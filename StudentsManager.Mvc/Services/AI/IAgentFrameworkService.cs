namespace StudentsManager.Mvc.Services.AI;

public interface IAgentFrameworkService
{
    // Basic chat
    Task<string> AskAsync(string prompt);
    Task<string> AskWithContextAsync(string prompt, string systemInstructions);
    IAsyncEnumerable<string> AskStreamingAsync(string prompt, CancellationToken cancellationToken = default);
}