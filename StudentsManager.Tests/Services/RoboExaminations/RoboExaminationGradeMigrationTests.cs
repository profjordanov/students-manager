using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using StudentsManager.Mvc.Migrations;
using StudentsManager.Mvc.Persistence;
using StudentsManager.Tests.Providers;
using Xunit;

namespace StudentsManager.Tests.Services.RoboExaminations;

[Trait("Category", "Integration")]
public class RoboExaminationGradeMigrationTests
{
    [Fact]
    public async Task MigrationAllowsZeroAndOneWithoutChangingHistoricalGrades()
    {
        var connection = new SqlConnectionStringBuilder(ConnectionStringProvider.GetConnectionString())
        {
            InitialCatalog = $"RoboGradeMigration_{Guid.NewGuid():N}"
        };
        using var context = new ManagerDbContext(new DbContextOptionsBuilder<ManagerDbContext>()
            .UseSqlServer(connection.ConnectionString).Options);
        try
        {
            await context.GetService<IRelationalDatabaseCreator>().CreateAsync();
            await context.Database.ExecuteSqlRawAsync("""
                CREATE TABLE [RoboExaminationAttempts] (
                    [Id] int IDENTITY PRIMARY KEY,
                    [Grade] int NULL,
                    CONSTRAINT [CK_RoboExaminationAttempts_Grade]
                        CHECK ([Grade] IS NULL OR [Grade] BETWEEN 2 AND 6));
                INSERT INTO [RoboExaminationAttempts] ([Grade]) VALUES (NULL), (2), (6);
                """);
            foreach (var grade in new[] { 0, 1 })
            {
                var exception = await Assert.ThrowsAsync<SqlException>(() =>
                    context.Database.ExecuteSqlInterpolatedAsync(
                        $"INSERT INTO [RoboExaminationAttempts] ([Grade]) VALUES ({grade})"));
                Assert.Equal(547, exception.Number);
            }

            var migration = new AllowZeroBasedRoboExaminationGrades();
            var commands = context.GetService<IMigrationsSqlGenerator>().Generate(migration.UpOperations);
            foreach (var command in commands)
                await context.Database.ExecuteSqlRawAsync(command.CommandText);

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO [RoboExaminationAttempts] ([Grade]) VALUES (0), (1)");
            var grades = await context.Database
                .SqlQueryRaw<int?>("SELECT [Grade] AS [Value] FROM [RoboExaminationAttempts] ORDER BY [Id]")
                .ToListAsync();
            Assert.Equal(new int?[] { null, 2, 6, 0, 1 }, grades);
            foreach (var grade in new[] { -1, 7 })
            {
                var exception = await Assert.ThrowsAsync<SqlException>(() =>
                    context.Database.ExecuteSqlInterpolatedAsync(
                        $"INSERT INTO [RoboExaminationAttempts] ([Grade]) VALUES ({grade})"));
                Assert.Equal(547, exception.Number);
            }
        }
        finally
        {
            await context.Database.EnsureDeletedAsync();
        }
    }
}
