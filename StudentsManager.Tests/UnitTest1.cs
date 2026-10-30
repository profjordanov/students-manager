using StudentsManager.Mvc.Persistence;
using StudentsManager.Mvc.Services.Tests;
using StudentsManager.Tests.Providers;
using Xunit;

namespace StudentsManager.Tests;

public class UnitTest1
{
    [Fact(Skip = "Skipping this test for now")]
    public void Test1()
    {
        // Arrange
        using var dbContext = new ManagerDbContext(DbContextProvider.GetDbContextOptions());
        dbContext.Database.EnsureCreated();
        var seeder = new DatabaseSeeder(dbContext);
        seeder.SeedDatabase();
        // Act
        var canConnect = dbContext.Database.CanConnect();
        // Assert
        Assert.True(canConnect);
    }
}