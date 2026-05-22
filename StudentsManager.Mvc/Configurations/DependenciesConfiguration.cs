using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using StudentsManager.Mvc.Domain.Entities;
using StudentsManager.Mvc.Persistence;
using StudentsManager.Mvc.Services.AI;
using StudentsManager.Mvc.Services.AppSettings;
using StudentsManager.Mvc.Services.Auth;
using StudentsManager.Mvc.Services.Clicks;
using StudentsManager.Mvc.Services.Cognitive;
using StudentsManager.Mvc.Services.CourseExaminations;
using StudentsManager.Mvc.Services.Courseworks;
using StudentsManager.Mvc.Services.Forum;
using StudentsManager.Mvc.Services.Homeworks;
using StudentsManager.Mvc.Services.Messaging;
using StudentsManager.Mvc.Services.Options;
using StudentsManager.Mvc.Services.Statistics;
using StudentsManager.Mvc.Services.Storage;
using StudentsManager.Mvc.Services.Students;
using StudentsManager.Mvc.Services.Tests;
using StudentsManager.Mvc.Services.Topics;
using StudentsManager.Mvc.Settings;

namespace StudentsManager.Mvc.Configurations
{
    public static class DependenciesConfiguration
    {
        public static IServiceCollection AddSettings(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.Configure<MailSettings>(configuration.GetSection(nameof(MailSettings)));
            services.Configure<ServiceBusSettings>(configuration.GetSection(nameof(ServiceBusSettings)));
            services.Configure<StorageSettings>(configuration.GetSection(nameof(StorageSettings)));
            services.Configure<AgentFrameworkAppSettings>(configuration.GetSection(nameof(AgentFrameworkAppSettings)));

            return services;
        }

        public static IServiceCollection AddManagerDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ManagerDbContext>(options => options.UseSqlServer(connectionString));
            return services;
        }

        public static IServiceCollection AddManagerIdentity(this IServiceCollection services)
        {
            services
                .AddIdentity<User, Role>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 0;
                    options.Password.RequiredUniqueChars = 0;

                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<ManagerDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme);
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/auth/login");
                options.LogoutPath = new PathString("/auth/logout");
            });

            return services;
        }

        public static IServiceCollection AddAzureClients(this IServiceCollection services, ConfigurationManager configuration)
        {
            var azureServiceBusConnectionString =
                configuration.GetSection(nameof(ServiceBusSettings))[nameof(ServiceBusSettings.AzureConnectionString)];

            var azureStorageConnectionString =
                configuration.GetSection(nameof(StorageSettings))[nameof(StorageSettings.AzureConnectionString)];

            services.AddAzureClients(builder =>
            {
                builder
                    .AddServiceBusClient(azureServiceBusConnectionString)
                    .WithName("DefaultServiceBus");
                builder
                    .AddBlobServiceClient(azureStorageConnectionString)
                    .WithName("DefaultBlobService");
            });

            services.AddScoped<IAzureServiceBusSender, AzureServiceBusSender>();
            return services;
        }

        public static IServiceCollection AddAzureTextAnalytic(this IServiceCollection services)
        {
            services.AddSingleton<ITextAnalyticService, TextAnalyticService>();
            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IStorageService, StorageService>();
            services.AddSingleton<IMailService, MailService>();

            services.AddScoped<ITopicsService, TopicsService>();
            services.AddScoped<ITestsService, TestsService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPrincipalService, PrincipalService>();
            services.AddScoped<IUserAnswersService, UserAnswersService>();
            services.AddScoped<IUserTopicResultsService, UserTopicResultsService>();
            services.AddScoped<IHomeworksService, HomeworksService>();
            services.AddScoped<IStudentsGradingService, StudentsGradingServiceV2>();

            services.AddScoped<IExaminationSettingsService, ExaminationSettingsService>();
            services.AddScoped<IStudentCourseExaminationService, StudentCourseExaminationService>();
            services.AddScoped<IExaminationUploadService, ExaminationUploadService>();

            services.AddScoped<IStudentsService, StudentsService>();
            services.AddScoped<IScreenSettingService, ScreenSettingService>();

            services.AddScoped<IClicksService, ClicksService>();

            services.AddScoped<IStudentsGradingPersistenceService, StudentsGradingPersistenceService>();

            services.AddScoped<IForumService, ForumService>();

            services.AddScoped<ICourseworksService, CourseworksService>();

            services.AddScoped<IExaminationAnswersService, ExaminationAnswersService>();

            services.AddScoped<IFoundryAgent, FoundryAgent>();

            services.AddScoped<IAgentFrameworkService, AgentFrameworkService>();

            return services;
        }

        public static IServiceCollection AddHostedServices(this IServiceCollection services)
        {
            services.AddHostedService<UserAnswerProcessingService>();

            return services;
        }

        public static IServiceCollection AddCrossOriginResourceSharing(this IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
            return services;
        }
    }
}