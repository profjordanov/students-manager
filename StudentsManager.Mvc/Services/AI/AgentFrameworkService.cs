using System.Runtime.CompilerServices;
using Azure.AI.Projects;
using Azure.Identity;
using Microsoft.Agents.AI;
using Microsoft.Extensions.Options;
using StudentsManager.Mvc.Settings;

namespace StudentsManager.Mvc.Services.AI;

public class AgentFrameworkService(IOptions<AgentFrameworkAppSettings> options) : IAgentFrameworkService
{
    private readonly AgentFrameworkAppSettings _settings = options.Value;
    private const string DeploymentName = "grok-4-1-smanager";
    private const string DefaultInstructions = "You are a university assistant. Keep your answers brief and helpful.";

    private AIProjectClient CreateClient() =>
        new(new Uri(_settings.TargetUri), new DefaultAzureCredential());

    #region Basic Chat

    public Task<string> AskAsync(string prompt) =>
        AskWithContextAsync(prompt, DefaultInstructions);

    public async Task<string> AskWithContextAsync(string prompt, string systemInstructions)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(prompt);
        ArgumentException.ThrowIfNullOrWhiteSpace(systemInstructions);

        AIAgent agent = CreateClient()
            .AsAIAgent(
                model: DeploymentName,
                instructions: systemInstructions,
                name: "UniversityAgent");

        var response = await agent.RunAsync(prompt);
        return response.Text;
    }

    public async IAsyncEnumerable<string> AskStreamingAsync(
        string prompt,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(prompt);

        AIAgent agent = CreateClient()
            .AsAIAgent(
                model: DeploymentName,
                instructions: DefaultInstructions,
                name: "UniversityAgent");

        await foreach (var chunk in agent.RunStreamingAsync(prompt).WithCancellation(cancellationToken))
        {
            yield return chunk.Text;
        }
    }

    #endregion
}