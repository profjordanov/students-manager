using System;
using System.ClientModel.Primitives;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.AI.OpenAI;
using OpenAI.Chat;
using StudentsManager.Mvc.Services.RoboExaminations;
using Xunit;

namespace StudentsManager.Tests.Services.AI;

public class AzureOpenAiCompatibilityTests
{
    [Theory]
    [InlineData("grok-4-1-smanager", 1000)]
    [InlineData("gpt-4.1-nano", 13107)]
    public async Task CompleteChatAsyncSerializesMaxTokensAndParsesResponse(
        string deployment,
        int maxTokens)
    {
        using var handler = new ChatCompletionHandler();
        using var httpClient = new HttpClient(handler);
        var client = new AzureOpenAIClient(
            new Uri("https://example.openai.azure.com"),
            new AzureKeyCredential("test-key"),
            new AzureOpenAIClientOptions
            {
                Transport = new HttpClientPipelineTransport(httpClient)
            });
        var options = new ChatCompletionOptions
        {
            ResponseFormat = ChatResponseFormat.CreateJsonObjectFormat(),
            Temperature = 0.2f,
            MaxOutputTokenCount = maxTokens
        };

        var response = await client.GetChatClient(deployment).CompleteChatAsync(
            [new SystemChatMessage("Return an evaluation as JSON."), new UserChatMessage("An answer.")],
            options,
            CancellationToken.None);

        Assert.Equal(HttpMethod.Post, handler.RequestMethod);
        Assert.Equal($"/openai/deployments/{deployment}/chat/completions", handler.RequestUri.AbsolutePath);
        using var request = JsonDocument.Parse(handler.RequestBody);
        Assert.Equal(maxTokens, request.RootElement.GetProperty("max_tokens").GetInt32());
        Assert.False(request.RootElement.TryGetProperty("max_completion_tokens", out _));
        Assert.Equal("json_object", request.RootElement.GetProperty("response_format").GetProperty("type").GetString());
        Assert.Equal(0.2f, request.RootElement.GetProperty("temperature").GetSingle());

        var evaluation = RoboExaminationEvaluationParser.Parse(Assert.Single(response.Value.Content).Text);
        Assert.Equal(5, evaluation.Grade);
        Assert.Equal("Good work.", evaluation.Feedback);
    }

    private sealed class ChatCompletionHandler : HttpMessageHandler
    {
        public HttpMethod RequestMethod { get; private set; }
        public Uri RequestUri { get; private set; }
        public string RequestBody { get; private set; }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            RequestMethod = request.Method;
            RequestUri = request.RequestUri;
            RequestBody = await request.Content.ReadAsStringAsync(cancellationToken);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(
                    """
                    {
                      "id": "chatcmpl-test",
                      "object": "chat.completion",
                      "created": 1700000000,
                      "model": "test-model",
                      "choices": [{
                        "index": 0,
                        "message": {
                          "role": "assistant",
                          "content": "{\"grade\":5,\"overallFeedback\":\"Good work.\"}"
                        },
                        "finish_reason": "stop"
                      }],
                      "usage": {"prompt_tokens":10,"completion_tokens":10,"total_tokens":20}
                    }
                    """,
                    Encoding.UTF8,
                    "application/json")
            };
        }
    }
}
