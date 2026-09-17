Directory structure:
└── profjordanov-students-manager/
    ├── README.md
    ├── docker-compose.dcproj
    ├── docker-compose.integration.yml
    ├── docker-compose.override.yml
    ├── docker-compose.yml
    ├── LICENSE
    ├── package.json
    ├── push-app.sh
    ├── run-app.sh
    ├── run-down-app.sh
    ├── run-tests.sh
    ├── SECURITY.md
    ├── StudentsManager.sln
    ├── .dockerignore
    ├── docs/
    │   └── Exploring the Impact of an Innovative E-Learning Development Environment on Mobile and Web Technology Education.md
    ├── StudentsManager.Mvc/
    │   ├── appsettings.Development.json
    │   ├── appsettings.json
    │   ├── Dockerfile
    │   ├── Program.cs
    │   ├── StudentsManager.Mvc.csproj
    │   ├── Configurations/
    │   │   ├── DependenciesConfiguration.cs
    │   │   └── Extensions.cs
    │   ├── Controllers/
    │   │   ├── AuthController.cs
    │   │   ├── ChatbotController.cs
    │   │   ├── ClickController.cs
    │   │   ├── EventsController.cs
    │   │   ├── ExaminationSettingsController.cs
    │   │   ├── ForumController.cs
    │   │   ├── HomeController.cs
    │   │   ├── HomeworksController.cs
    │   │   ├── LoginController.cs
    │   │   ├── SandboxController.cs
    │   │   ├── SettingsController.cs
    │   │   ├── SlidoController.cs
    │   │   ├── StudentsController.cs
    │   │   ├── TestController.cs
    │   │   └── _Base/
    │   │       └── BaseController.cs
    │   ├── Domain/
    │   │   ├── CustomDataStructures/
    │   │   │   └── PaginatedList.cs
    │   │   ├── Entities/
    │   │   │   ├── Course.cs
    │   │   │   ├── CourseExamination.cs
    │   │   │   ├── CourseExaminationSetting.cs
    │   │   │   ├── Event.cs
    │   │   │   ├── ExaminationAnswer.cs
    │   │   │   ├── ForumComment.cs
    │   │   │   ├── ForumQuestion.cs
    │   │   │   ├── Homework.cs
    │   │   │   ├── QuestionOption.cs
    │   │   │   ├── Role.cs
    │   │   │   ├── ScreenSetting.cs
    │   │   │   ├── StudentCourseExamination.cs
    │   │   │   ├── StudentCourseExaminationUpload.cs
    │   │   │   ├── TestQuestion.cs
    │   │   │   ├── Topic.cs
    │   │   │   ├── User.cs
    │   │   │   ├── UserAnswer.cs
    │   │   │   ├── UserCourse.cs
    │   │   │   ├── UserCoursework.cs
    │   │   │   ├── UserTopicResult.cs
    │   │   │   └── _Base/
    │   │   │       ├── File.cs
    │   │   │       ├── IAuditInfo.cs
    │   │   │       └── IDeletableEntity.cs
    │   │   ├── Inputs/
    │   │   │   ├── ChosenOption.cs
    │   │   │   ├── EventInput.cs
    │   │   │   ├── TestAnswers.cs
    │   │   │   ├── TestBindingModel.cs
    │   │   │   ├── TestStats.cs
    │   │   │   ├── Auth/
    │   │   │   │   ├── Credentials.cs
    │   │   │   │   ├── Register.cs
    │   │   │   │   └── ResetPassword.cs
    │   │   │   ├── Click/
    │   │   │   │   ├── ClickResultsRootObject.cs
    │   │   │   │   ├── SaveResultsModel.cs
    │   │   │   │   └── UserAnswer.cs
    │   │   │   ├── Forum/
    │   │   │   │   └── Comment.cs
    │   │   │   ├── Messaging/
    │   │   │   │   └── MailRequest.cs
    │   │   │   └── Students/
    │   │   │       ├── StudentExaminationScore.cs
    │   │   │       └── UpdatePicture.cs
    │   │   └── Views/
    │   │       ├── Error.cs
    │   │       ├── QuestionOptionView.cs
    │   │       ├── TestQuestionView.cs
    │   │       ├── TestViewModel.cs
    │   │       ├── UserTopicResultView.cs
    │   │       ├── Cognitive/
    │   │       │   └── UserAnswerReportView.cs
    │   │       ├── Home/
    │   │       │   └── CourseExaminationSettingsView.cs
    │   │       ├── Homeworks/
    │   │       │   └── HomeworkView.cs
    │   │       ├── Statistics/
    │   │       │   └── StudentGrade.cs
    │   │       └── Students/
    │   │           └── StudentView.cs
    │   ├── Mappings/
    │   │   ├── AuthMapping.cs
    │   │   ├── QuestionOptionMapping.cs
    │   │   ├── TestQuestionMapping.cs
    │   │   └── UserTopicMapping.cs
    │   ├── Migrations/
    │   │   ├── 20210624120313_InitialCreate.cs
    │   │   ├── 20210624120313_InitialCreate.Designer.cs
    │   │   ├── 20210624123249_ExtendUser.cs
    │   │   ├── 20210624123249_ExtendUser.Designer.cs
    │   │   ├── 20210624125229_AddedExaminationAnswer.cs
    │   │   ├── 20210624125229_AddedExaminationAnswer.Designer.cs
    │   │   ├── 20210625120122_UserAuditable.cs
    │   │   ├── 20210625120122_UserAuditable.Designer.cs
    │   │   ├── 20210625123648_AddCourse.cs
    │   │   ├── 20210625123648_AddCourse.Designer.cs
    │   │   ├── 20210625125413_UpdateTopics.cs
    │   │   ├── 20210625125413_UpdateTopics.Designer.cs
    │   │   ├── 20210802081349_AddForum.cs
    │   │   ├── 20210802081349_AddForum.Designer.cs
    │   │   ├── 20210805085215_AddUserCourseworks.cs
    │   │   ├── 20210805085215_AddUserCourseworks.Designer.cs
    │   │   ├── 20210914080831_AddVideoLinkFromPreviousYearProp.cs
    │   │   ├── 20210914080831_AddVideoLinkFromPreviousYearProp.Designer.cs
    │   │   ├── 20210917064114_AddContentTypeToExaminationAnswers.cs
    │   │   ├── 20210917064114_AddContentTypeToExaminationAnswers.Designer.cs
    │   │   ├── 20210917105438_ProcessSpecificPropertiesToExaminationAnswer.cs
    │   │   ├── 20210917105438_ProcessSpecificPropertiesToExaminationAnswer.Designer.cs
    │   │   ├── 20220705073937_NullableMigration.cs
    │   │   ├── 20220705073937_NullableMigration.Designer.cs
    │   │   ├── 20221103083243_AddPointsToUserCoursework.cs
    │   │   ├── 20221103083243_AddPointsToUserCoursework.Designer.cs
    │   │   ├── 20230124120528_ExaminationAnswerNullables.cs
    │   │   ├── 20230124120528_ExaminationAnswerNullables.Designer.cs
    │   │   ├── 20260323150342_AddEvents.cs
    │   │   ├── 20260323150342_AddEvents.Designer.cs
    │   │   └── ManagerDbContextModelSnapshot.cs
    │   ├── Pages/
    │   │   ├── Admin.cshtml
    │   │   ├── Admin.cshtml.cs
    │   │   ├── Coursework.cshtml
    │   │   ├── Coursework.cshtml.cs
    │   │   ├── FirstExamination.cshtml
    │   │   ├── FirstExamination.cshtml.cs
    │   │   ├── Grading.cshtml
    │   │   ├── Grading.cshtml.cs
    │   │   ├── Homework.cshtml
    │   │   ├── Homework.cshtml.cs
    │   │   ├── Profile.cshtml
    │   │   ├── Profile.cshtml.cs
    │   │   ├── SecondExamination.cshtml
    │   │   ├── SecondExamination.cshtml.cs
    │   │   └── _Base/
    │   │       ├── ExaminationModel.cs
    │   │       └── IExaminationModel.cs
    │   ├── Persistence/
    │   │   ├── ManagerDbContext.cs
    │   │   └── OnModelCreatingConfiguration.cs
    │   ├── Properties/
    │   │   └── launchSettings.json
    │   ├── Resources/
    │   │   ├── basics.json
    │   │   └── resource.json
    │   ├── Services/
    │   │   ├── AI/
    │   │   │   ├── AgentFrameworkService.cs
    │   │   │   ├── FoundryAgent.cs
    │   │   │   ├── IAgentFrameworkService.cs
    │   │   │   └── IFoundryAgent.cs
    │   │   ├── AppSettings/
    │   │   │   ├── IScreenSettingService.cs
    │   │   │   ├── ScreenSettingConstants.cs
    │   │   │   ├── ScreenSettingSeeder.cs
    │   │   │   └── ScreenSettingService.cs
    │   │   ├── Auth/
    │   │   │   ├── AuthConstants.cs
    │   │   │   ├── AuthService.cs
    │   │   │   ├── IAuthService.cs
    │   │   │   ├── IPrincipalService.cs
    │   │   │   └── PrincipalService.cs
    │   │   ├── Clicks/
    │   │   │   ├── ClicksService.cs
    │   │   │   ├── ExaminationAnswersService.cs
    │   │   │   ├── IClicksService.cs
    │   │   │   └── IExaminationAnswersService.cs
    │   │   ├── Cognitive/
    │   │   │   ├── BasicKeyPhrases.cs
    │   │   │   ├── ITextAnalyticService.cs
    │   │   │   ├── KeyPhrases.cs
    │   │   │   ├── TextAnalyticService.cs
    │   │   │   └── UserAnswerProcessingService.cs
    │   │   ├── CourseExaminations/
    │   │   │   ├── CourseExaminationConstants.cs
    │   │   │   ├── ExaminationSeeder.cs
    │   │   │   ├── ExaminationSettingsService.cs
    │   │   │   ├── ExaminationUploadService.cs
    │   │   │   ├── IExaminationSettingsService.cs
    │   │   │   ├── IExaminationUploadService.cs
    │   │   │   ├── IStudentCourseExaminationService.cs
    │   │   │   └── StudentCourseExaminationService.cs
    │   │   ├── Courses/
    │   │   │   └── CourseSeeder.cs
    │   │   ├── Courseworks/
    │   │   │   ├── CourseworksService.cs
    │   │   │   └── ICourseworksService.cs
    │   │   ├── Forum/
    │   │   │   ├── ForumService.cs
    │   │   │   └── IForumService.cs
    │   │   ├── Homeworks/
    │   │   │   ├── HomeworksService.cs
    │   │   │   └── IHomeworksService.cs
    │   │   ├── Messaging/
    │   │   │   ├── AzureServiceBusSender.cs
    │   │   │   ├── IAzureServiceBusSender.cs
    │   │   │   ├── IMailService.cs
    │   │   │   └── MailService.cs
    │   │   ├── Options/
    │   │   │   ├── IUserAnswersService.cs
    │   │   │   ├── QuestionOptionSeeder.cs
    │   │   │   └── UserAnswersService.cs
    │   │   ├── Questions/
    │   │   │   ├── AsyncProgrammingTopicSeeder.cs
    │   │   │   ├── CordovaTopicSeeder.cs
    │   │   │   ├── JqueryTopicSeeder.cs
    │   │   │   ├── OnsenTopicSeeder.cs
    │   │   │   ├── ReactTopicSeeder.cs
    │   │   │   └── TestQuestionSeeder.cs
    │   │   ├── Statistics/
    │   │   │   ├── IStudentsGradingPersistenceService.cs
    │   │   │   ├── IStudentsGradingService.cs
    │   │   │   ├── StudentsGradingPersistenceService.cs
    │   │   │   └── StudentsGradingServiceV2.cs
    │   │   ├── Storage/
    │   │   │   ├── IStorageService.cs
    │   │   │   └── StorageService.cs
    │   │   ├── Students/
    │   │   │   ├── IStudentsService.cs
    │   │   │   └── StudentsService.cs
    │   │   ├── Tests/
    │   │   │   ├── DatabaseSeeder.cs
    │   │   │   ├── ITestsService.cs
    │   │   │   └── TestsService.cs
    │   │   └── Topics/
    │   │       ├── ITopicsService.cs
    │   │       ├── IUserTopicResultsService.cs
    │   │       ├── TopicsSeeder.cs
    │   │       ├── TopicsService.cs
    │   │       └── UserTopicResultsService.cs
    │   ├── Settings/
    │   │   ├── AgentFrameworkAppSettings.cs
    │   │   ├── CognitiveSettings.cs
    │   │   ├── MailSettings.cs
    │   │   ├── ServiceBusSettings.cs
    │   │   └── StorageSettings.cs
    │   ├── Views/
    │   │   ├── _ViewImports.cshtml
    │   │   ├── _ViewStart.cshtml
    │   │   ├── Auth/
    │   │   │   ├── Login.cshtml
    │   │   │   ├── Problem.cshtml
    │   │   │   ├── Register.cshtml
    │   │   │   └── ResetPassword.cshtml
    │   │   ├── Click/
    │   │   │   └── Index.cshtml
    │   │   ├── Forum/
    │   │   │   └── Index.cshtml
    │   │   ├── Home/
    │   │   │   ├── _HomeAdviceSectionPartial.cshtml
    │   │   │   └── Index.cshtml
    │   │   ├── Shared/
    │   │   │   ├── _FlexLayout.cshtml
    │   │   │   ├── _Layout.cshtml
    │   │   │   ├── _LoginPartial.cshtml
    │   │   │   ├── _ValidationScriptsPartial.cshtml
    │   │   │   └── Error.cshtml
    │   │   └── Test/
    │   │       ├── _ActiveQuestionSectionPartial.cshtml
    │   │       ├── _ImageQuestionSectionPartial.cshtml
    │   │       ├── _TextQuestionSectionPartial.cshtml
    │   │       ├── Done.cshtml
    │   │       ├── Index.cshtml
    │   │       ├── Result.cshtml
    │   │       └── Start.cshtml
    │   └── wwwroot/
    │       ├── css/
    │       │   ├── forum.css
    │       │   ├── style.css
    │       │   └── table.css
    │       ├── files/
    │       │   └── fonts/
    │       │       ├── raleway-medium-webfont.woff
    │       │       ├── raleway-medium-webfont.woff2
    │       │       ├── raleway-regular-webfont.woff
    │       │       ├── raleway-regular-webfont.woff2
    │       │       ├── raleway-semibold-webfont.woff
    │       │       └── raleway-semibold-webfont.woff2
    │       ├── js/
    │       │   ├── loader.js
    │       │   └── test.js
    │       └── lib/
    │           ├── bootstrap/
    │           │   └── LICENSE
    │           ├── jquery-validation/
    │           │   └── LICENSE.md
    │           └── jquery-validation-unobtrusive/
    │               ├── jquery.validate.unobtrusive.js
    │               └── LICENSE.txt
    ├── StudentsManager.Spa/
    │   ├── Dockerfile
    │   ├── eslint.config.js
    │   ├── index.html
    │   ├── package.json
    │   ├── src.txt
    │   ├── structure.txt
    │   ├── vite.config.js
    │   ├── .dockerignore
    │   ├── nginx/
    │   │   └── default.conf
    │   ├── public/
    │   │   └── files/
    │   │       └── fonts/
    │   │           ├── raleway-medium-webfont.woff
    │   │           ├── raleway-medium-webfont.woff2
    │   │           ├── raleway-regular-webfont.woff
    │   │           ├── raleway-regular-webfont.woff2
    │   │           ├── raleway-semibold-webfont.woff
    │   │           └── raleway-semibold-webfont.woff2
    │   ├── src/
    │   │   ├── App.jsx
    │   │   ├── main.jsx
    │   │   ├── components/
    │   │   │   ├── Chatbot/
    │   │   │   │   ├── Chatbot.jsx
    │   │   │   │   ├── ChatbotForm.jsx
    │   │   │   │   ├── ChatbotOptions.jsx
    │   │   │   │   ├── ChatbotQuestion.jsx
    │   │   │   │   ├── ChatbotResultCard.jsx
    │   │   │   │   ├── ChatbotResults.jsx
    │   │   │   │   └── ChatbotTextInput.jsx
    │   │   │   ├── Layout/
    │   │   │   │   ├── Footer.jsx
    │   │   │   │   ├── Header.jsx
    │   │   │   │   └── Layout.jsx
    │   │   │   └── Quiz/
    │   │   │       ├── Quiz.jsx
    │   │   │       ├── quizConstants.js
    │   │   │       ├── QuizProgress.jsx
    │   │   │       ├── QuizQuestion.jsx
    │   │   │       ├── QuizResults.jsx
    │   │   │       ├── QuizReview.jsx
    │   │   │       ├── QuizStart.jsx
    │   │   │       └── useQuiz.js
    │   │   ├── context/
    │   │   │   └── AuthContext.jsx
    │   │   ├── data/
    │   │   │   ├── questions.json
    │   │   │   └── quizQuestions.js
    │   │   ├── pages/
    │   │   │   ├── ChatbotPage.jsx
    │   │   │   ├── ChatbotResultsPage.jsx
    │   │   │   ├── ComradesPage.jsx
    │   │   │   ├── HomePage.css
    │   │   │   ├── HomePage.jsx
    │   │   │   ├── LoginPage.jsx
    │   │   │   ├── ProfilePage.jsx
    │   │   │   ├── QuizPage.jsx
    │   │   │   ├── SlidoPage.jsx
    │   │   │   ├── TimelinePage.jsx
    │   │   │   └── UserlessPage.jsx
    │   │   └── services/
    │   │       ├── apiConfig.js
    │   │       ├── chatbotService.js
    │   │       ├── eventsService.js
    │   │       └── userService.js
    │   └── .github/
    │       ├── copilot-instructions.md
    │       ├── agents/
    │       │   ├── chatbot-expert.agent.md
    │       │   └── quiz-expert.agent.md
    │       └── prompts/
    │           ├── plan-addAnimatedRobotToChatbotPage.prompt.md
    │           ├── plan-progressiveWebApp.prompt.md
    │           └── plan-quizRefactor.prompt.md
    ├── StudentsManager.Tests/
    │   ├── appsettings.json
    │   ├── Dockerfile
    │   ├── StudentsManager.Tests.csproj
    │   ├── UnitTest1.cs
    │   ├── Fixtures/
    │   │   └── UserAnswerFixture.cs
    │   ├── Models/
    │   │   ├── AppSettingsRoot.cs
    │   │   └── ConnectionStrings.cs
    │   ├── Properties/
    │   │   └── launchSettings.json
    │   ├── Providers/
    │   │   ├── ConnectionStringProvider.cs
    │   │   └── DbContextProvider.cs
    │   └── Services/
    │       └── TextAnalyticServiceTests.cs
    └── .github/
        └── workflows/
            ├── docker-build-spa.yml
            ├── docker-compose.yml
            └── dotnet-deploy.yml
