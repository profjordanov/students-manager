using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StudentsManager.Mvc.Domain.Entities;
using StudentsManager.Mvc.Domain.Inputs.UserGithubs;
using StudentsManager.Mvc.Persistence;
using StudentsManager.Mvc.Services.UserGithubs;
using StudentsManager.Tests.Providers;
using Xunit;

namespace StudentsManager.Tests.Services.UserGithubs;

[Trait("Category", "Integration")]
public class UserGithubsServiceIntegrationTests : IAsyncLifetime
{
    // Own database: it always has the current schema and never shares rows with other tests.
    private readonly DbContextOptions<ManagerDbContext> _options = CreateOptions();

    public async Task InitializeAsync()
    {
        using var context = new ManagerDbContext(_options);
        await context.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        using var context = new ManagerDbContext(_options);
        await context.Database.EnsureDeletedAsync();
    }

    [Fact]
    public async Task CreateStoresTrimmedValuesAndGetReturnsThem()
    {
        var user = await SeedUserAsync();

        var created = await RunAsync(service => service.CreateAsync(
            user.Id,
            Input("  https://github.com/octocat  ", "  My GitHub profile  ")));
        var loaded = await RunAsync(service => service.GetAsync(user.Id));

        Assert.Equal(UserGithubsServiceStatus.Success, created.Status);
        Assert.Equal(UserGithubsServiceStatus.Success, loaded.Status);
        foreach (var view in new[] { created.Value, loaded.Value })
        {
            Assert.Equal(user.Id, view.UserId);
            Assert.Equal("https://github.com/octocat", view.GithubLink);
            Assert.Equal("My GitHub profile", view.Description);
        }
    }

    [Fact]
    public async Task CreateRejectsASecondEntryForTheSameUserAndKeepsTheFirst()
    {
        var user = await SeedUserAsync();

        var first = await RunAsync(service => service.CreateAsync(user.Id, Input("https://github.com/first", "First")));
        var second = await RunAsync(service => service.CreateAsync(user.Id, Input("https://github.com/second", "Second")));
        var stored = await RunAsync(service => service.GetAsync(user.Id));

        Assert.Equal(UserGithubsServiceStatus.Success, first.Status);
        Assert.Equal(UserGithubsServiceStatus.AlreadyExists, second.Status);
        Assert.Null(second.Value);
        Assert.Equal("https://github.com/first", stored.Value.GithubLink);
        Assert.Equal("First", stored.Value.Description);
    }

    [Fact]
    public async Task ConcurrentCreatesForTheSameUserStoreExactlyOneEntry()
    {
        var user = await SeedUserAsync();

        var results = await Task.WhenAll(
            RunAsync(service => service.CreateAsync(user.Id, Input("https://github.com/one", "One"))),
            RunAsync(service => service.CreateAsync(user.Id, Input("https://github.com/two", "Two"))));

        Assert.Single(results, result => result.Status == UserGithubsServiceStatus.Success);
        Assert.Single(results, result => result.Status == UserGithubsServiceStatus.AlreadyExists);
        using var context = new ManagerDbContext(_options);
        Assert.Equal(1, await context.UserGithubs.CountAsync(github => github.UserId == user.Id));
    }

    [Fact]
    public async Task OperationsReportAnUnknownUserAndStoreNothing()
    {
        var userId = Guid.NewGuid();

        Assert.Equal(UserGithubsServiceStatus.UserNotFound, (await RunAsync(service => service.GetAsync(userId))).Status);
        Assert.Equal(UserGithubsServiceStatus.UserNotFound, (await RunAsync(service => service.CreateAsync(userId, Input()))).Status);
        Assert.Equal(UserGithubsServiceStatus.UserNotFound, (await RunAsync(service => service.UpdateAsync(userId, Input()))).Status);
        Assert.Equal(UserGithubsServiceStatus.UserNotFound, await RunAsync(service => service.DeleteAsync(userId)));

        using var context = new ManagerDbContext(_options);
        Assert.False(await context.UserGithubs.AnyAsync());
    }

    [Fact]
    public async Task OperationsTreatASoftDeletedUserAsMissingAndLeaveTheirEntryUntouched()
    {
        var user = await SeedUserAsync(isDeleted: true);
        await SeedGithubAsync(user.Id, "https://github.com/deleted", "Deleted user");

        Assert.Equal(UserGithubsServiceStatus.UserNotFound, (await RunAsync(service => service.GetAsync(user.Id))).Status);
        Assert.Equal(UserGithubsServiceStatus.UserNotFound, (await RunAsync(service => service.CreateAsync(user.Id, Input()))).Status);
        Assert.Equal(UserGithubsServiceStatus.UserNotFound, (await RunAsync(service => service.UpdateAsync(user.Id, Input()))).Status);
        Assert.Equal(UserGithubsServiceStatus.UserNotFound, await RunAsync(service => service.DeleteAsync(user.Id)));

        using var context = new ManagerDbContext(_options);
        var stored = await context.UserGithubs.SingleAsync(github => github.UserId == user.Id);
        Assert.Equal("https://github.com/deleted", stored.GithubLink);
        Assert.Equal("Deleted user", stored.Description);
    }

    [Fact]
    public async Task GetUpdateAndDeleteReportAMissingEntryWithoutCreatingOne()
    {
        var user = await SeedUserAsync();

        Assert.Equal(UserGithubsServiceStatus.NotFound, (await RunAsync(service => service.GetAsync(user.Id))).Status);
        Assert.Equal(UserGithubsServiceStatus.NotFound, (await RunAsync(service => service.UpdateAsync(user.Id, Input()))).Status);
        Assert.Equal(UserGithubsServiceStatus.NotFound, await RunAsync(service => service.DeleteAsync(user.Id)));

        using var context = new ManagerDbContext(_options);
        Assert.False(await context.UserGithubs.AnyAsync());
    }

    [Fact]
    public async Task UpdateReplacesBothFieldsOfOnlyThatUsersEntry()
    {
        var first = await SeedUserAsync();
        var second = await SeedUserAsync();
        await SeedGithubAsync(first.Id, "https://github.com/first", "First profile");
        await SeedGithubAsync(second.Id, "https://github.com/second", "Second profile");

        var updated = await RunAsync(service => service.UpdateAsync(
            first.Id,
            Input(" https://github.com/first-new ", " New description ")));
        var firstStored = await RunAsync(service => service.GetAsync(first.Id));
        var secondStored = await RunAsync(service => service.GetAsync(second.Id));

        Assert.Equal(UserGithubsServiceStatus.Success, updated.Status);
        Assert.Equal(first.Id, updated.Value.UserId);
        Assert.Equal("https://github.com/first-new", updated.Value.GithubLink);
        Assert.Equal("New description", updated.Value.Description);
        Assert.Equal("https://github.com/first-new", firstStored.Value.GithubLink);
        Assert.Equal("New description", firstStored.Value.Description);
        Assert.Equal("https://github.com/second", secondStored.Value.GithubLink);
        Assert.Equal("Second profile", secondStored.Value.Description);
    }

    [Fact]
    public async Task DeleteRemovesOnlyThatUsersEntry()
    {
        var first = await SeedUserAsync();
        var second = await SeedUserAsync();
        await SeedGithubAsync(first.Id, "https://github.com/first", "First profile");
        await SeedGithubAsync(second.Id, "https://github.com/second", "Second profile");

        var deleted = await RunAsync(service => service.DeleteAsync(first.Id));
        var firstStored = await RunAsync(service => service.GetAsync(first.Id));
        var secondStored = await RunAsync(service => service.GetAsync(second.Id));

        Assert.Equal(UserGithubsServiceStatus.Success, deleted);
        Assert.Equal(UserGithubsServiceStatus.NotFound, firstStored.Status);
        Assert.Equal(UserGithubsServiceStatus.Success, secondStored.Status);
    }

    [Fact]
    public async Task DeletingAUserDeletesTheirEntry()
    {
        var user = await SeedUserAsync();
        await SeedGithubAsync(user.Id, "https://github.com/octocat", "Profile");

        using (var context = new ManagerDbContext(_options))
            await context.Users.Where(item => item.Id == user.Id).ExecuteDeleteAsync();

        using (var context = new ManagerDbContext(_options))
            Assert.False(await context.UserGithubs.AnyAsync(github => github.UserId == user.Id));
    }

    private static DbContextOptions<ManagerDbContext> CreateOptions()
    {
        var connectionString = new SqlConnectionStringBuilder(ConnectionStringProvider.GetConnectionString())
        {
            InitialCatalog = $"UserGithubsTests_{Guid.NewGuid():N}"
        }.ConnectionString;
        return new DbContextOptionsBuilder<ManagerDbContext>().UseSqlServer(connectionString).Options;
    }

    // Each call gets its own context, like a separate request would.
    private async Task<T> RunAsync<T>(Func<UserGithubsService, Task<T>> action)
    {
        using var context = new ManagerDbContext(_options);
        return await action(new UserGithubsService(context));
    }

    private static UserGithubInput Input(
        string githubLink = "https://github.com/octocat",
        string description = "My GitHub profile") =>
        new() { GithubLink = githubLink, Description = description };

    private async Task<User> SeedUserAsync(bool isDeleted = false)
    {
        var userId = Guid.NewGuid();
        var user = new User
        {
            Id = userId,
            UserName = $"user-githubs-{userId:N}",
            NormalizedUserName = $"USER-GITHUBS-{userId:N}",
            Email = $"{userId:N}@example.test",
            NormalizedEmail = $"{userId:N}@EXAMPLE.TEST",
            FullName = "Test Student",
            FacultyNumber = userId.ToString("N"),
            CreatedOn = DateTime.UtcNow,
            IsDeleted = isDeleted,
            DeletedOn = isDeleted ? DateTime.UtcNow : null
        };
        using var context = new ManagerDbContext(_options);
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return user;
    }

    private async Task SeedGithubAsync(Guid userId, string githubLink, string description)
    {
        using var context = new ManagerDbContext(_options);
        context.UserGithubs.Add(new UserGithub { UserId = userId, GithubLink = githubLink, Description = description });
        await context.SaveChangesAsync();
    }
}
