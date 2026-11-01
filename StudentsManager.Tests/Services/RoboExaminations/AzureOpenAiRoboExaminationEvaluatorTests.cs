using System;
using System.ClientModel.Primitives;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Azure.AI.OpenAI;
using Microsoft.Extensions.Options;
using StudentsManager.Mvc.Services.RoboExaminations;
using StudentsManager.Mvc.Settings;
using Xunit;

namespace StudentsManager.Tests.Services.RoboExaminations;

public class AzureOpenAiRoboExaminationEvaluatorTests
{
    private static readonly RoboExaminationEvaluationItem[] Items =
        [new("What is a closure?", "A function that remembers its outer scope.")];

    [Fact]
    public async Task SendsRequestCompatibleWithGpt5Nano()
    {
        using var handler = new StubHandler(Completion("""{"grade":5,"overallFeedback":"Good."}""", "stop"));

        var result = await CreateEvaluator(handler).EvaluateAsync(Items, CancellationToken.None);

        Assert.Equal(5, result.Grade);
        Assert.Equal("Good.", result.Feedback);
        Assert.Equal("/openai/deployments/gpt-5-nano/chat/completions", handler.RequestPath);
        using var body = JsonDocument.Parse(handler.RequestBody!);
        var root = body.RootElement;
        Assert.False(root.TryGetProperty("temperature", out _));
        Assert.Equal("low", root.GetProperty("reasoning_effort").GetString());
        Assert.Equal("json_object", root.GetProperty("response_format").GetProperty("type").GetString());
        var cap = root.TryGetProperty("max_completion_tokens", out var completionCap)
            ? completionCap
            : root.GetProperty("max_tokens");
        Assert.True(cap.GetInt32() >= 4000, "Token cap must leave room for reasoning tokens.");
    }

    [Fact]
    public async Task SendsQuestionsAndAnswersAsJsonDataAndSystemPrompt()
    {
        using var handler = new StubHandler(Completion("""{"grade":4,"overallFeedback":"Ok."}""", "stop"));

        await CreateEvaluator(handler).EvaluateAsync(Items, CancellationToken.None);

        using var body = JsonDocument.Parse(handler.RequestBody!);
        var messages = body.RootElement.GetProperty("messages");
        Assert.Equal(2, messages.GetArrayLength());
        Assert.Equal("system", messages[0].GetProperty("role").GetString());
        Assert.Contains("between 2 and 6", messages[0].GetProperty("content").GetString());
        Assert.Equal("user", messages[1].GetProperty("role").GetString());
        using var data = JsonDocument.Parse(messages[1].GetProperty("content").GetString()!);
        Assert.Equal("What is a closure?", data.RootElement[0].GetProperty("question").GetString());
        Assert.Equal(
            "A function that remembers its outer scope.",
            data.RootElement[0].GetProperty("answer").GetString());
    }

    [Fact]
    public async Task ReasoningExhaustingTokenCapFailsWithEvaluationException()
    {
        using var handler = new StubHandler(Completion("", "length"));

        var exception = await Assert.ThrowsAsync<RoboExaminationEvaluationException>(
            () => CreateEvaluator(handler).EvaluateAsync(Items, CancellationToken.None));

        Assert.Contains("output tokens", exception.Message);
    }

    [Fact]
    public async Task RefusalFailsWithEvaluationException()
    {
        using var handler = new StubHandler(
            """
            {"id":"c","object":"chat.completion","created":1,"model":"gpt-5-nano",
             "choices":[{"index":0,"message":{"role":"assistant","content":null,"refusal":"Cannot help."},"finish_reason":"stop"}]}
            """);

        var exception = await Assert.ThrowsAsync<RoboExaminationEvaluationException>(
            () => CreateEvaluator(handler).EvaluateAsync(Items, CancellationToken.None));

        Assert.Contains("Cannot help.", exception.Message);
    }

    [Fact]
    public async Task InvalidModelOutputPreservesRaw()
    {
        using var handler = new StubHandler(Completion("not json", "stop"));

        var exception = await Assert.ThrowsAsync<RoboExaminationEvaluationException>(
            () => CreateEvaluator(handler).EvaluateAsync(Items, CancellationToken.None));

        Assert.Equal("not json", exception.Raw);
    }

    [Theory]
    [InlineData(HttpStatusCode.BadRequest)]
    [InlineData(HttpStatusCode.TooManyRequests)]
    [InlineData(HttpStatusCode.InternalServerError)]
    public async Task HttpErrorsAreWrappedInEvaluationException(HttpStatusCode status)
    {
        using var handler = new StubHandler("""{"error":{"message":"boom"}}""", status);

        var exception = await Assert.ThrowsAsync<RoboExaminationEvaluationException>(
            () => CreateEvaluator(handler).EvaluateAsync(Items, CancellationToken.None));

        Assert.NotNull(exception.InnerException);
    }

    [Theory]
    [InlineData("", "key")]
    [InlineData("not-a-uri", "key")]
    [InlineData("https://example.openai.azure.com", "")]
    public async Task MissingConfigurationFailsBeforeAnyRequest(string endpoint, string key)
    {
        using var handler = new StubHandler(Completion("{}", "stop"));

        await Assert.ThrowsAsync<RoboExaminationEvaluationException>(
            () => CreateEvaluator(handler, endpoint, key).EvaluateAsync(Items, CancellationToken.None));

        Assert.Null(handler.RequestBody);
    }

    [Fact]
    public async Task CancellationIsNotWrapped()
    {
        using var handler = new StubHandler(Completion("{}", "stop"));
        using var cts = new CancellationTokenSource();
        cts.Cancel();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(
            () => CreateEvaluator(handler).EvaluateAsync(Items, cts.Token));
    }

    private static AzureOpenAiRoboExaminationEvaluator CreateEvaluator(
        StubHandler handler,
        string endpoint = "https://example.openai.azure.com",
        string key = "test-key")
    {
        var settings = Options.Create(new ServiceBusSettings { AzureConnectionString = endpoint, QueueName = key });
        var clientOptions = new AzureOpenAIClientOptions
        {
            Transport = new HttpClientPipelineTransport(new HttpClient(handler)),
            RetryPolicy = new ClientRetryPolicy(0)
        };
        return new AzureOpenAiRoboExaminationEvaluator(settings, clientOptions);
    }

    private static string Completion(string content, string finishReason) =>
        JsonSerializer.Serialize(new
        {
            id = "chatcmpl-test",
            @object = "chat.completion",
            created = 1700000000,
            model = "gpt-5-nano",
            choices = new[]
            {
                new
                {
                    index = 0,
                    message = new { role = "assistant", content },
                    finish_reason = finishReason
                }
            }
        });

    private sealed class StubHandler(string responseBody, HttpStatusCode status = HttpStatusCode.OK)
        : HttpMessageHandler
    {
        public string? RequestPath { get; private set; }
        public string? RequestBody { get; private set; }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            RequestPath = request.RequestUri!.AbsolutePath;
            RequestBody = await request.Content!.ReadAsStringAsync(cancellationToken);
            return new HttpResponseMessage(status)
            {
                Content = new StringContent(responseBody, Encoding.UTF8, "application/json")
            };
        }
    }
}
