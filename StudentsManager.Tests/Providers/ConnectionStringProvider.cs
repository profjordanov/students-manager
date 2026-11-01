using System;
using System.IO;
using Newtonsoft.Json;
using StudentsManager.Tests.Models;

namespace StudentsManager.Tests.Providers;

internal static class ConnectionStringProvider
{
    internal static string GetConnectionString()
    {
        var connectionString = Environment.GetEnvironmentVariable("STUDENTS_MANAGER_TEST_CONNECTION_STRING");
        if (!string.IsNullOrWhiteSpace(connectionString))
            return connectionString;

        var file = File.ReadAllText("appsettings.json");
        var appSettings = JsonConvert.DeserializeObject<AppSettingsRoot>(file);
        return appSettings?.ConnectionStrings.DefaultConnection;
    }
}