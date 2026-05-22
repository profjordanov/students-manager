namespace StudentsManager.Mvc.Services.AI;

public interface IFoundryAgent
{
    Task<string> Test();
    Task<string> ChatWithSystemPrompt(string systemPrompt, string userMessage);
    Task<string> ChatWithOptions(string userMessage, float temperature = 0.7f, int maxTokens = 500);
    IAsyncEnumerable<string> StreamChat(string userMessage);
    Task<string> MultiTurnConversation(List<(string Role, string Content)> messages);
    Task<string> ChatWithJsonOutput(string userMessage);
    Task<string> ChatWithToolCalling(string userMessage);
    Task<string> SummarizeWithStopSequences(string textToSummarize);
    Task<string> AnalyzeImage(Uri imageUri, string question);
    Task<string> ReproducibleChat(string userMessage, long seed);
    Task<string> ChatWithLogprobs(string userMessage);
    Task<string> GenerateWithNucleusSampling(string userMessage, float topP = 0.1f);
    Task<string> ChatWithToolbox(string userMessage);
}