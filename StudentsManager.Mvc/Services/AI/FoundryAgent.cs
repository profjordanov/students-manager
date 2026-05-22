using Azure.Identity;
using Microsoft.Extensions.Options;
using ModelContextProtocol.Client;
using OpenAI;
using OpenAI.Chat;
using StudentsManager.Mvc.Settings;
using System.ClientModel;
using System.Text;
using System.Text.Json;

namespace StudentsManager.Mvc.Services.AI;

public class FoundryAgent(IOptions<ServiceBusSettings> options) : IFoundryAgent
{
    private readonly ServiceBusSettings _settings = options.Value;
    private const string DeploymentName = "grok-4-1-smanager";

    private ChatClient CreateClient() => new(
        credential: new ApiKeyCredential(_settings.QueueName),
        model: DeploymentName,
        options: new OpenAIClientOptions()
        {
            Endpoint = new($"{_settings.AzureConnectionString}"),
        });

    public async Task<string> Test()
    {
        var client = CreateClient();

        ChatCompletion completion = await client.CompleteChatAsync(
        [
            new SystemChatMessage("You are a helpful assistant for a student management system. Be concise and informative."),
            new UserChatMessage("What are the top 3 best practices for managing student records in a university?")
        ]);

        var sb = new StringBuilder();
        sb.AppendLine($"Model: {completion.Model}");
        sb.AppendLine($"Finish Reason: {completion.FinishReason}");
        sb.AppendLine($"Token Usage - Input: {completion.Usage.InputTokenCount}, Output: {completion.Usage.OutputTokenCount}, Total: {completion.Usage.TotalTokenCount}");
        sb.AppendLine();

        foreach (var contentPart in completion.Content)
        {
            sb.AppendLine($"[{completion.Role}]: {contentPart.Text}");
        }

        return sb.ToString();
    }

    public async Task<string> ChatWithSystemPrompt(string systemPrompt, string userMessage)
    {
        var client = CreateClient();

        ChatCompletion completion = await client.CompleteChatAsync(
        [
            new SystemChatMessage(systemPrompt),
            new UserChatMessage(userMessage)
        ]);

        return completion.Content[0].Text;
    }

    public async Task<string> ChatWithOptions(string userMessage, float temperature = 0.7f, int maxTokens = 500)
    {
        var client = CreateClient();

        var chatOptions = new ChatCompletionOptions
        {
            Temperature = temperature,
            MaxOutputTokenCount = maxTokens,
            TopP = 0.9f,
            FrequencyPenalty = 0.5f,
            PresencePenalty = 0.3f,
        };

        ChatCompletion completion = await client.CompleteChatAsync(
        [
            new SystemChatMessage("You are a concise assistant. Answer in bullet points when possible."),
            new UserChatMessage(userMessage)
        ], chatOptions);

        var sb = new StringBuilder();
        sb.AppendLine($"[Temperature={temperature}, MaxTokens={maxTokens}]");
        sb.AppendLine($"Finish Reason: {completion.FinishReason}");
        sb.AppendLine($"Tokens Used: {completion.Usage.TotalTokenCount}");
        sb.AppendLine();
        sb.AppendLine(completion.Content[0].Text);

        return sb.ToString();
    }

    public async IAsyncEnumerable<string> StreamChat(string userMessage)
    {
        var client = CreateClient();

        AsyncCollectionResult<StreamingChatCompletionUpdate> updates = client.CompleteChatStreamingAsync(
        [
            new SystemChatMessage("You are a helpful assistant. Provide detailed responses."),
            new UserChatMessage(userMessage)
        ]);

        await foreach (var update in updates)
        {
            foreach (var part in update.ContentUpdate)
            {
                yield return part.Text;
            }
        }
    }

    public async Task<string> MultiTurnConversation(List<(string Role, string Content)> messages)
    {
        var client = CreateClient();

        var chatMessages = new List<ChatMessage>();

        foreach (var (role, content) in messages)
        {
            chatMessages.Add(role.ToLowerInvariant() switch
            {
                "system" => new SystemChatMessage(content),
                "assistant" => new AssistantChatMessage(content),
                _ => new UserChatMessage(content)
            });
        }

        ChatCompletion completion = await client.CompleteChatAsync(chatMessages);

        var sb = new StringBuilder();
        sb.AppendLine($"Conversation turns: {messages.Count}");
        sb.AppendLine($"Tokens: {completion.Usage.TotalTokenCount}");
        sb.AppendLine();
        sb.AppendLine(completion.Content[0].Text);

        return sb.ToString();
    }

    public async Task<string> ChatWithJsonOutput(string userMessage)
    {
        var client = CreateClient();

        var chatOptions = new ChatCompletionOptions
        {
            ResponseFormat = ChatResponseFormat.CreateJsonSchemaFormat(
                jsonSchemaFormatName: "student_analysis",
                jsonSchema: BinaryData.FromString("""
                {
                    "type": "object",
                    "properties": {
                        "summary": { "type": "string" },
                        "keyPoints": { "type": "array", "items": { "type": "string" } },
                        "sentiment": { "type": "string", "enum": ["positive", "neutral", "negative"] },
                        "confidence": { "type": "number" }
                    },
                    "required": ["summary", "keyPoints", "sentiment", "confidence"],
                    "additionalProperties": false
                }
                """),
                jsonSchemaIsStrict: true)
        };

        ChatCompletion completion = await client.CompleteChatAsync(
        [
            new SystemChatMessage("You are an assistant that analyzes student feedback. Always respond in the requested JSON format."),
            new UserChatMessage(userMessage)
        ], chatOptions);

        var rawJson = completion.Content[0].Text;
        var formatted = JsonSerializer.Serialize(
            JsonSerializer.Deserialize<JsonElement>(rawJson),
            new JsonSerializerOptions { WriteIndented = true });

        var sb = new StringBuilder();
        sb.AppendLine("=== Structured JSON Output ===");
        sb.AppendLine($"Tokens: {completion.Usage.TotalTokenCount}");
        sb.AppendLine();
        sb.AppendLine(formatted);

        return sb.ToString();
    }

    public async Task<string> ChatWithToolCalling(string userMessage)
    {
        var client = CreateClient();

        var tools = new List<ChatTool>
        {
            ChatTool.CreateFunctionTool(
                functionName: "get_student_grade",
                functionDescription: "Gets the current grade for a student in a specific course",
                functionParameters: BinaryData.FromString("""
                {
                    "type": "object",
                    "properties": {
                        "studentId": { "type": "string", "description": "The student's ID" },
                        "courseId": { "type": "string", "description": "The course ID" }
                    },
                    "required": ["studentId", "courseId"]
                }
                """)),
            ChatTool.CreateFunctionTool(
                functionName: "get_course_schedule",
                functionDescription: "Gets the schedule for a specific course",
                functionParameters: BinaryData.FromString("""
                {
                    "type": "object",
                    "properties": {
                        "courseId": { "type": "string", "description": "The course ID" }
                    },
                    "required": ["courseId"]
                }
                """))
        };

        var chatOptions = new ChatCompletionOptions();
        foreach (var tool in tools)
        {
            chatOptions.Tools.Add(tool);
        }

        ChatCompletion completion = await client.CompleteChatAsync(
        [
            new SystemChatMessage("You are a student management assistant. Use the available tools to help answer questions."),
            new UserChatMessage(userMessage)
        ], chatOptions);

        var sb = new StringBuilder();
        sb.AppendLine("=== Tool Calling Demo ===");
        sb.AppendLine($"Finish Reason: {completion.FinishReason}");

        if (completion.FinishReason == ChatFinishReason.ToolCalls)
        {
            foreach (var toolCall in completion.ToolCalls)
            {
                sb.AppendLine($"Tool Called: {toolCall.FunctionName}");
                sb.AppendLine($"Arguments: {toolCall.FunctionArguments}");
                sb.AppendLine();
            }

            // Simulate tool response and continue conversation
            var followUp = new List<ChatMessage>
            {
                new SystemChatMessage("You are a student management assistant."),
                new UserChatMessage(userMessage),
                new AssistantChatMessage(completion),
            };

            foreach (var toolCall in completion.ToolCalls)
            {
                followUp.Add(new ToolChatMessage(toolCall.Id, """{"result": "Sample data - Grade: A, Schedule: MWF 10:00 AM"}"""));
            }

            ChatCompletion followUpCompletion = await client.CompleteChatAsync(followUp);
            sb.AppendLine("=== Final Response After Tool Use ===");
            sb.AppendLine(followUpCompletion.Content[0].Text);
        }
        else
        {
            sb.AppendLine(completion.Content[0].Text);
        }

        return sb.ToString();
    }

    public async Task<string> SummarizeWithStopSequences(string textToSummarize)
    {
        var client = CreateClient();

        var chatOptions = new ChatCompletionOptions
        {
            Temperature = 0.3f,
            MaxOutputTokenCount = 300,
        };
        chatOptions.StopSequences.Add("END");
        chatOptions.StopSequences.Add("---");

        ChatCompletion completion = await client.CompleteChatAsync(
        [
            new SystemChatMessage("Summarize the following text concisely. End your summary with 'END' on a new line."),
            new UserChatMessage(textToSummarize)
        ], chatOptions);

        var sb = new StringBuilder();
        sb.AppendLine("=== Summary with Stop Sequences ===");
        sb.AppendLine($"Finish Reason: {completion.FinishReason}");
        sb.AppendLine($"Tokens - Input: {completion.Usage.InputTokenCount}, Output: {completion.Usage.OutputTokenCount}");
        sb.AppendLine();
        sb.AppendLine(completion.Content[0].Text);

        return sb.ToString();
    }

    public async Task<string> AnalyzeImage(Uri imageUri, string question)
    {
        ChatClient client = CreateClient();

        ChatCompletion completion = await client.CompleteChatAsync(
        [
            new SystemChatMessage("You are a vision assistant that analyzes images related to education and student management."),
            new UserChatMessage(
                ChatMessageContentPart.CreateTextPart(question),
                ChatMessageContentPart.CreateImagePart(imageUri))
        ]);

        var sb = new StringBuilder();
        sb.AppendLine("=== Vision / Image Analysis ===");
        sb.AppendLine($"Tokens - Input: {completion.Usage.InputTokenCount}, Output: {completion.Usage.OutputTokenCount}");
        sb.AppendLine();
        sb.AppendLine(completion.Content[0].Text);

        return sb.ToString();
    }

    public async Task<string> ReproducibleChat(string userMessage, long seed)
    {
        ChatClient client = CreateClient();

        var chatOptions = new ChatCompletionOptions
        {
#pragma warning disable OPENAI001
            Seed = seed,
#pragma warning restore OPENAI001
            Temperature = 0f,
        };

        ChatCompletion completion = await client.CompleteChatAsync(
        [
            new SystemChatMessage("You are a deterministic assistant. Given the same input and seed, produce identical output."),
            new UserChatMessage(userMessage)
        ], chatOptions);

        var sb = new StringBuilder();
        sb.AppendLine("=== Reproducible Output (Seed) ===");
        sb.AppendLine($"Seed: {seed}");
        sb.AppendLine($"System Fingerprint: {completion.SystemFingerprint}");
        sb.AppendLine($"Tokens: {completion.Usage.TotalTokenCount}");
        sb.AppendLine();
        sb.AppendLine(completion.Content[0].Text);

        return sb.ToString();
    }

    public async Task<string> ChatWithLogprobs(string userMessage)
    {
        ChatClient client = CreateClient();

        var chatOptions = new ChatCompletionOptions
        {
            IncludeLogProbabilities = true,
            TopLogProbabilityCount = 3,
        };

        ChatCompletion completion = await client.CompleteChatAsync(
        [
            new SystemChatMessage("You are a helpful assistant."),
            new UserChatMessage(userMessage)
        ], chatOptions);

        var sb = new StringBuilder();
        sb.AppendLine("=== Log Probabilities ===");
        sb.AppendLine($"Response: {completion.Content[0].Text}");
        sb.AppendLine();
        sb.AppendLine("Token-level confidence (first 10 tokens):");

        if (completion.ContentTokenLogProbabilities is { Count: > 0 })
        {
            foreach (var tokenLogprob in completion.ContentTokenLogProbabilities.Take(10))
            {
                sb.AppendLine($"  Token: \"{tokenLogprob.Token}\" | LogProb: {tokenLogprob.LogProbability:F4} | Prob: {Math.Exp(tokenLogprob.LogProbability):P2}");

                foreach (var alt in tokenLogprob.TopLogProbabilities)
                {
                    sb.AppendLine($"    Alt: \"{alt.Token}\" | LogProb: {alt.LogProbability:F4}");
                }
            }
        }

        return sb.ToString();
    }

    public async Task<string> GenerateWithNucleusSampling(string userMessage, float topP = 0.1f)
    {
        ChatClient client = CreateClient();

        var chatOptions = new ChatCompletionOptions
        {
            TopP = topP,
            Temperature = 1.0f,
            MaxOutputTokenCount = 300,
        };

        ChatCompletion completion = await client.CompleteChatAsync(
        [
            new SystemChatMessage("You are a creative writing assistant for student essays."),
            new UserChatMessage(userMessage)
        ], chatOptions);

        var sb = new StringBuilder();
        sb.AppendLine($"=== Nucleus Sampling (TopP={topP}) ===");
        sb.AppendLine($"Tokens: {completion.Usage.TotalTokenCount}");
        sb.AppendLine();
        sb.AppendLine(completion.Content[0].Text);

        return sb.ToString();
    }

    public async Task<string> ChatWithToolbox(string userMessage)
    {
        const string toolboxEndpoint = "";

        var credential = new DefaultAzureCredential();
        var token = await credential.GetTokenAsync(
            new Azure.Core.TokenRequestContext(["https://ai.azure.com/.default"]));

        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Token);
        httpClient.DefaultRequestHeaders.Add("Foundry-Features", "Toolboxes=V1Preview");

        var transportOptions = new HttpClientTransportOptions
        {
            Endpoint = new Uri(toolboxEndpoint),
            TransportMode = HttpTransportMode.StreamableHttp,
        };

        await using var transport = new HttpClientTransport(transportOptions, httpClient, loggerFactory: null, ownsHttpClient: false);
        await using var mcpClient = await McpClient.CreateAsync(transport);

        // List available tools from the toolbox
        var mcpTools = await mcpClient.ListToolsAsync();

        // Convert MCP tools to OpenAI ChatTools
        var chatTools = new List<ChatTool>();
        foreach (var tool in mcpTools)
        {
            chatTools.Add(ChatTool.CreateFunctionTool(
                functionName: tool.Name,
                functionDescription: tool.Description,
                functionParameters: tool.JsonSchema is { } schema
                    ? BinaryData.FromString(JsonSerializer.Serialize(schema))
                    : null));
        }

        var chatOptions = new ChatCompletionOptions();
        foreach (var tool in chatTools)
        {
            chatOptions.Tools.Add(tool);
        }

        var client = CreateClient();
        var messages = new List<ChatMessage>
        {
            new SystemChatMessage("You are a helpful assistant with access to Azure AI Foundry toolbox tools. Use the available tools to help answer questions."),
            new UserChatMessage(userMessage)
        };

        ChatCompletion completion = await client.CompleteChatAsync(messages, chatOptions);

        var sb = new StringBuilder();
        sb.AppendLine("=== Foundry Toolbox MCP Demo ===");
        sb.AppendLine($"Available Tools: {mcpTools.Count}");
        foreach (var tool in mcpTools)
        {
            sb.AppendLine($"  - {tool.Name}: {tool.Description}");
        }
        sb.AppendLine();
        sb.AppendLine($"Finish Reason: {completion.FinishReason}");

        // Handle tool calls if the model wants to use tools
        if (completion.FinishReason == ChatFinishReason.ToolCalls)
        {
            foreach (var toolCall in completion.ToolCalls)
            {
                sb.AppendLine($"Tool Called: {toolCall.FunctionName}");
                sb.AppendLine($"Arguments: {toolCall.FunctionArguments}");

                // Call the tool via MCP
                var arguments = JsonSerializer.Deserialize<Dictionary<string, object>>(toolCall.FunctionArguments.ToString());
                string resultText;
                try
                {
                    var toolResult = await mcpClient.CallToolAsync(toolCall.FunctionName, arguments);
                    resultText = string.Join("\n", toolResult.Content
                        .OfType<ModelContextProtocol.Protocol.TextContentBlock>()
                        .Select(c => c.Text));
                }
                catch (Exception ex)
                {
                    resultText = $"Tool call failed: {ex.Message}";
                    sb.AppendLine($"Tool Error: {ex.Message}");
                }

                sb.AppendLine($"Tool Result: {resultText}");
                sb.AppendLine();

                // Continue conversation with tool results
                messages.Add(new AssistantChatMessage(completion));
                messages.Add(new ToolChatMessage(toolCall.Id, resultText));
            }

            // Get final response after tool use
            ChatCompletion followUp = await client.CompleteChatAsync(messages, chatOptions);
            sb.AppendLine("=== Final Response ===");
            sb.AppendLine(followUp.Content[0].Text);
        }
        else
        {
            sb.AppendLine(completion.Content[0].Text);
        }

        return sb.ToString();
    }
}