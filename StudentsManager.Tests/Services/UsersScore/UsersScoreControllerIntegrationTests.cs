using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StudentsManager.Mvc.Controllers;
using StudentsManager.Mvc.Domain.Entities;
using StudentsManager.Mvc.Domain.Views.UsersScore;
using StudentsManager.Mvc.Persistence;
using StudentsManager.Tests.Providers;
using Xunit;

namespace StudentsManager.Tests.Services.UsersScore;

[Trait("Category", "Integration")]
public class UsersScoreControllerIntegrationTests : IAsyncLifetime
{
    // The scoreboard lists every row, so the test uses its own database: it always has the current schema
    // and never shares scores with other tests.
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
    public async Task GetAllReturnsEachUsersNamesWithTheirScores()
    {
        var first = await SeedAsync("Ivan Petrov", 30.50m, 20m, 5.25m);
        var second = await SeedAsync("Maria Ivanova", 10m, 0m, 2m);
        using var context = new ManagerDbContext(_options);
        var controller = new UsersScoreController(context);

        var response = await controller.GetAll();

        var scores = Assert.IsAssignableFrom<IEnumerable<UserScoreView>>(
            Assert.IsType<OkObjectResult>(response.Result).Value).ToList();
        Assert.Equal(2, scores.Count);
        var firstScore = Assert.Single(scores, score => score.UserId == first.Id);
        Assert.Equal(first.UserName, firstScore.UserName);
        Assert.Equal("Ivan Petrov", firstScore.FullName);
        Assert.Equal(30.50m, firstScore.FirstExaminationScore);
        Assert.Equal(20m, firstScore.SecondExaminationScore);
        Assert.Equal(5.25m, firstScore.Points);
        Assert.Equal(55.75m, firstScore.Total);
        var secondScore = Assert.Single(scores, score => score.UserId == second.Id);
        Assert.Equal(second.UserName, secondScore.UserName);
        Assert.Equal("Maria Ivanova", secondScore.FullName);
        Assert.Equal(12m, secondScore.Total);
    }

    private static DbContextOptions<ManagerDbContext> CreateOptions()
    {
        var connectionString = new SqlConnectionStringBuilder(ConnectionStringProvider.GetConnectionString())
        {
            InitialCatalog = $"UsersScoreTests_{Guid.NewGuid():N}"
        }.ConnectionString;
        return new DbContextOptionsBuilder<ManagerDbContext>().UseSqlServer(connectionString).Options;
    }

    private async Task<User> SeedAsync(string fullName, decimal firstScore, decimal secondScore, decimal points)
    {
        var userId = Guid.NewGuid();
        var user = new User
        {
            Id = userId,
            UserName = $"users-score-{userId:N}",
            NormalizedUserName = $"USERS-SCORE-{userId:N}",
            Email = $"{userId:N}@example.test",
            NormalizedEmail = $"{userId:N}@EXAMPLE.TEST",
            FullName = fullName,
            FacultyNumber = userId.ToString("N"),
            CreatedOn = DateTime.UtcNow
        };
        using var context = new ManagerDbContext(_options);
        context.Users.Add(user);
        context.UsersScore.Add(new UserScore
        {
            UserId = userId,
            FirstExaminationScore = firstScore,
            SecondExaminationScore = secondScore,
            Points = points
        });
        await context.SaveChangesAsync();
        return user;
    }
}
