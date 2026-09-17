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


Files Content:

(Files content cropped to 300k characters, download full ingest to see more)
================================================
FILE: README.md
================================================
# StudentsManager

A web application built on .NET, React, MSSQL, Azure, and OpenAI, designed to manage course-related data at the University of Economics – Varna. Students actively use the platform while also developing new functionalities for it.

[![Docker Compose Build Check](https://github.com/profjordanov/students-manager/actions/workflows/docker-compose.yml/badge.svg)](https://github.com/profjordanov/students-manager/actions/workflows/docker-compose.yml)
[![Deploy Manager to Azure App Service](https://github.com/profjordanov/students-manager/actions/workflows/dotnet-deploy.yml/badge.svg)](https://github.com/profjordanov/students-manager/actions/workflows/dotnet-deploy.yml)
[![CodeQL](https://github.com/profjordanov/students-manager/actions/workflows/github-code-scanning/codeql/badge.svg)](https://github.com/profjordanov/students-manager/actions/workflows/github-code-scanning/codeql)
[![CodeFactor](https://www.codefactor.io/repository/github/profjordanov/students-manager/badge)](https://www.codefactor.io/repository/github/profjordanov/students-manager)
[![SonarQube Cloud](https://sonarcloud.io/images/project_badges/sonarcloud-light.svg)](https://sonarcloud.io/summary/new_code?id=profjordanov_students-manager)

---

## Table of Contents

- [Environments](#environments)
- [API Endpoints](#api-endpoints)
- [MVC Frontend](#mvc-frontend-mainjs--chatbot)
- [Technologies](#-technologies)
- [Project Structure](#-project-structure)
- [Getting Started](#️-getting-started)
- [Running Tests](#-running-tests)
- [Docker Commands](#-docker-commands)
- [License](#-license)
- [Security](#-security)

---

## Environments

| Environment | URL | Notes |
|---|---|---|
| Production | https://students-manager.site/ | Public production deployment |
| Development | https://students-manager-dev.azurewebsites.net/ | Dev backend / API deployment |
| React (SPA) | https://students-manager-spa.azurewebsites.net/ | React version of the platform |

## API Endpoints

### 1. Forum

**GET forum posts**

```bash
curl --location 'https://students-manager-dev.azurewebsites.net/api/slido?limit=20&skip=0'
```

**POST question**

```bash
curl --location 'https://students-manager-dev.azurewebsites.net/api/slido/question' \
  --header 'Content-Type: application/json' \
  --data '{
    "question": "api post get"
  }'
```

**POST comment**

```bash
curl --location 'https://students-manager-dev.azurewebsites.net/api/slido/comment' \
  --header 'Content-Type: application/json' \
  --data '{
    "forumQuestionId": 4,
    "description": "api post comment"
  }'
```

**GET questions**

```bash
curl --location 'https://students-manager-dev.azurewebsites.net/api/slido/questions?limit=20&skip=0'
```

Example response:

```json
["api post get", "lower api/slido/question", "because", "why?", "question", ".net 10"]
```

### 2. User Profile / Login

**POST login**

```bash
curl --request POST \
  --url https://students-manager-dev.azurewebsites.net/api/login \
  --header 'Content-Type: application/json' \
  --data '{
    "email": "jordan@abv.bg",
    "password": "password"
  }'
```

Responses:

```json
// 200
{
  "userId": "1eac9820-5e6e-4d10-6e94-08de36f40f78"
}
```

```json
// 401
{
  "message": "Invalid email or password."
}
```

**GET profile**

```bash
curl --location 'https://students-manager-dev.azurewebsites.net/api/students/profile/022a6007-f33c-47c3-b811-08de88b121f2'
```

Example response:

```json
// 200
{
  "id": "022a6007-f33c-47c3-b811-08de88b121f2",
  "fullName": "Dr J",
  "base64EncodePicture": null,
  "facultyNumber": "987987",
  "testQuestions": [
    {
      "testQuestionDescription": "Какъв е правилният синтаксис за препратка към външен скрипт, наречен xxx.js?",
      "questionOptionDescription": "<script src=xxx.js>",
      "wasCorrect": true
    }
  ]
}
```

```json
// 400
{
  "message": "Invalid user."
}
```

**PUT picture**

```bash
curl --location --request PUT 'https://students-manager-dev.azurewebsites.net/api/students/picture' \
  --header 'Content-Type: application/json' \
  --data '{
    "FacultyNumber": "123123123",
    "Password": "password",
    "Picture": "data:image/jpeg;base64,/9j/k="
  }'
```

### 3. Events

**POST event**

```bash
curl --location 'https://students-manager-dev.azurewebsites.net/api/events' \
  --header 'Content-Type: application/json' \
  --data '{
    "userId": "123123",
    "type": "test",
    "data": "temp"
  }'
```

**GET events by user ID**

```bash
curl --location 'https://students-manager-dev.azurewebsites.net/api/events/022a6007-f33c-47c3-b811-08de88b121f2'
```

Example response:

```json
[
  {
    "id": "fddea52a-5702-46c8-86f6-00ed51641660",
    "userId": "022a6007-f33c-47c3-b811-08de88b121f2",
    "datetimeUtc": "2026-03-24T14:33:42.3137213",
    "type": "geolocation-position-drj",
    "data": "{\"latitude\":53.513211150189285,\"longitude\":-80.50332253634296}"
  }
]
```

### 4. Chatbot

**POST save results**

```bash
curl "https://students-manager-dev.azurewebsites.net/api/chatbot/examination-answers" \
  -H "Content-Type: application/json" \
  --data-raw '{
    "userId": "022a6007-f33c-47c3-b811-08de88b121f2",
    "answers": [
      {
        "questionId": "q123123",
        "questionText": "What is programming?",
        "answer": "programming is .."
      }
    ]
  }'
```

**GET examination answers**

```bash
curl --location 'https://students-manager-dev.azurewebsites.net/api/chatbot/examination-answers/522a6007-fkkc-47c3-b811-08de88b121f2'
```

---

## MVC Frontend (main.js + chatbot)

### main.js

`main.js` is a single bundled and minified file that mixes third-party libraries with a custom global `App` namespace (site logic).

#### Third-party libraries embedded in the bundle

| Library | Version | Purpose |
|---------|---------|---------|
| jQuery | 3.1.1 | DOM manipulation |
| GSAP TweenMax | 1.19.x | Animations (ScrollToPlugin, CSSPlugin, etc.) |
| ScrollMagic | 2.0.5 | Scroll-triggered animations (plus GSAP plugin) |
| Blazy | — | Lazy-loading images |
| fullPage.js | — | One-page scrolling sections |
| Swiper | — | Carousel / slider |
| Plyr | — | Video / audio player |
| jQBrowser | — | User-agent detection helper |

#### Custom application logic

The bundle defines a global `App` object and initializes multiple modules via `App.init()`.

On window load it calls:

| Module | Description |
|--------|-------------|
| `App.resize()` | Sets `App.viewport_height` / `App.viewport_width` and `App.mobile` based on UA / width |
| `App.bind()` | Attaches UI handlers (menu, category tabs, video popup, job popup, chatbot start, etc.) |
| `App.UI.init()` | Lazy-loading + fullPage initialization |
| `App.Test.init()` | Course tests behavior |
| `App.Scroll.init()` | Parallax + header scroll states + scroll-to |
| `App.sliderSwipper.init()` | Initializes Swiper sliders |
| `App.Animations`, `App.Login`, `App.Profile` | Additional UI modules |

### Course Tests Page

The key module is `App.Test`. It binds click handlers on the active question only:

- `#test` click on `.question.active .answer input` → `animateAfterClick`
- `#test` click on `.question.active .answer input` → `countStats`

**`countStats()` behavior:**

- Reads the value of the clicked radio (`action` / `process` / `people` / `idea`)
- Reads `data-answer` (`1` or `2`) and `data-question` (`1..40`)
- Resets / recomputes category totals
- Records the chosen answer for that question

**`animateAfterClick()` behavior:**

- Hides answers for non-active questions initially (`setOpacityToAllUnactiveQuestions`)
- After selecting an answer, animates the transition to the next `.question` (via TweenMax)
- Uses a guard (`#test.animating`) to prevent double-clicks during transitions

### Chatbot

#### External dependencies

| Library | Bundled? | Purpose |
|---------|----------|---------|
| Lodash (`_`) | Yes | Utility functions (`_.trim`, `_.map`, `_.filter`, etc.) |
| Typed.js | Yes | Typing animation effect (`new Typed(...)`) |
| jQuery (`$`) | No | Expected to be available globally |

---

## 🚀 Technologies

- **Backend:** ASP.NET Core MVC
- **Frontend:** React (SPA), Razor Views
- **Database:** Microsoft SQL Server
- **Cloud:** Azure App Service
- **AI:** OpenAI
- **Containerization:** Docker & Docker Compose
- **Testing:** xUnit (StudentsManager.Tests)

## 📁 Project Structure

```
├── StudentsManager.Mvc/          # Main MVC application
│   ├── Controllers/              # MVC Controllers
│   ├── Domain/                   # Domain models
│   ├── Mappings/                 # Object mappings
│   ├── Migrations/               # Database migrations
│   ├── Persistence/              # Data access layer
│   ├── Services/                 # Business logic services
│   ├── Settings/                 # Configuration settings
│   ├── Views/                    # Razor views
│   └── wwwroot/                  # Static files
├── StudentsManager.Tests/        # Unit tests
└── docker-compose.yml            # Docker orchestration
```

## 🛠️ Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/get-started) (optional)

### Running Locally

```bash
cd StudentsManager.Mvc
dotnet run
```

### Running with Docker

```bash
./run-app.sh
```

Or using Docker Compose directly:

```bash
docker-compose up
```

### Stopping the Application

```bash
./run-down-app.sh
```

## 🧪 Running Tests

```bash
./run-tests.sh
```

Or manually:

```bash
dotnet test StudentsManager.Tests/
```

## 🐳 Docker Commands

| Script | Description |
|--------|-------------|
| `run-app.sh` | Start the application |
| `run-down-app.sh` | Stop the application |
| `push-app.sh` | Push Docker images |
| `run-tests.sh` | Run test suite |

---

## 📄 License

See [LICENSE](LICENSE) for details.

## 🔒 Security

See [SECURITY.md](SECURITY.md) for security policies.



================================================
FILE: docker-compose.dcproj
================================================
<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="15.0" Sdk="Microsoft.Docker.Sdk">
  <PropertyGroup Label="Globals">
    <ProjectVersion>2.1</ProjectVersion>
    <DockerTargetOS>Linux</DockerTargetOS>
    <ProjectGuid>9dca3501-fca3-4416-acfe-1c21a2816380</ProjectGuid>
    <DockerLaunchAction>LaunchBrowser</DockerLaunchAction>
    <DockerServiceUrl>{Scheme}://localhost:{ServicePort}</DockerServiceUrl>
    <DockerServiceName>studentsmanager.mvc</DockerServiceName>
  </PropertyGroup>
  <ItemGroup>
    <None Include="docker-compose.override.yml">
      <DependentUpon>docker-compose.yml</DependentUpon>
    </None>
    <None Include="docker-compose.yml" />
    <None Include=".dockerignore" />
  </ItemGroup>
</Project>


================================================
FILE: docker-compose.integration.yml
================================================
version: "3.3"

services:

    data:
        container_name: sqlserver
        image: mcr.microsoft.com/mssql/server:2019-latest
        ports: 
            - "1433:1433"
        environment:
            - ACCEPT_EULA=Y
            - SA_PASSWORD=yourStrongPassword12!@
        volumes: 
            - sqldata:/var/opt/mssql 
        networks: 
            - tests-network
            
    studentsmanager.tests:
        container_name: studentsmanagertests
        build:
            context: .
            dockerfile: StudentsManager.Tests/Dockerfile    
        networks: 
            - tests-network
        depends_on:
            - data    

networks:
    tests-network:

volumes:
    sqldata:            



================================================
FILE: docker-compose.override.yml
================================================
version: '3.4'

services:
  azurite:
    hostname: azurite
    command: "azurite-blob --loose --blobHost 0.0.0.0 --skipApiVersionCheck"
    ports: 
      - "10000:10000"
    volumes: 
      - ./cert/azurite:/data
    networks: 
      - studentsmanager-network

  data:
    environment:
      - ACCEPT_EULA=Y
      - SA_PASSWORD=strongPassword12!@
    ports: 
      - "1433:1433"
    volumes: 
      - sqldata:/var/opt/mssql 
    networks: 
      - studentsmanager-network
      
  studentsmanager.mvc:
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ASPNETCORE_URLS=https://+:443;http://+:80
      - ConnectionStrings__DefaultConnection=Server=sqlserver;Database=StudentsManagerDb;User Id=sa;Password=strongPassword12!@;Encrypt=false;
      - StorageSettings__AzureConnectionString=UseDevelopmentStorage=true;DevelopmentStorageProxyUri=http://azurite
      - StorageSettings__PathBasis=http://azurite:10000/
      - ServiceBusSettings__AzureConnectionString=Endpoint=sb://bus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=123-Q
      - ServiceBusSettings__QueueName=ExaminationQueue
    ports:
      - "80"
      - "443"
    volumes:
      - ${APPDATA}/Microsoft/UserSecrets:/root/.microsoft/usersecrets:ro
      - ${APPDATA}/ASP.NET/Https:/root/.aspnet/https:ro
    networks: 
      - studentsmanager-network
    depends_on:
      - data
      - azurite

  studentsmanager.spa:
    ports:
      - "3000:80"
    networks:
      - studentsmanager-network
    depends_on:
      - studentsmanager.mvc
      
networks:
    studentsmanager-network:

volumes:
    sqldata:      



================================================
FILE: docker-compose.yml
================================================
version: '3.4'

services:
  azurite:
    container_name: azurite
    image: mcr.microsoft.com/azure-storage/azurite

  data:
    container_name: sqlserver
    image: mcr.microsoft.com/mssql/server:2019-latest
    
  studentsmanager.mvc:
    container_name: studentsmanagermvc
    image: profjordanov/studentsmanagermvc:10.0
    build:
      context: .
      dockerfile: StudentsManager.Mvc/Dockerfile

  studentsmanager.spa:
    container_name: studentsmanagerspa
    image: profjordanov/studentsmanagerspa:1.0
    build:
      context: ./StudentsManager.Spa
      dockerfile: Dockerfile



================================================
FILE: LICENSE
================================================
MIT License

Copyright (c) 2026 Assistant Professor Dr. Yordan Yordanov

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.



================================================
FILE: package.json
================================================
{
  "dependencies": {
    "vite": "^8.0.8"
  }
}



================================================
FILE: push-app.sh
================================================
#!/bin/bash
docker-compose push


================================================
FILE: run-app.sh
================================================
#!/bin/bash
docker compose -f docker-compose.yml -f docker-compose.override.yml -p studentsmanager up -d



================================================
FILE: run-down-app.sh
================================================
#!/bin/bash
docker-compose down


================================================
FILE: run-tests.sh
================================================
#!/bin/bash
docker-compose -f docker-compose.integration.yml up --exit-code-from studentsmanager.tests --build


================================================
FILE: SECURITY.md
================================================
# Security Policy

## Supported Versions

| Version | Supported          |
| ------- | ------------------ |
| 5.1.x   | :white_check_mark: |
| 5.0.x   | :x:                |
| 4.0.x   | :white_check_mark: |
| < 4.0   | :x:                |

## Reporting a Vulnerability
This section is being used to tell people how to report a vulnerability.
Plese, go and update the vulnerability report, the vulnerability will be either accepted or declined, etc.


================================================
FILE: StudentsManager.sln
================================================
﻿
Microsoft Visual Studio Solution File, Format Version 12.00
# Visual Studio Version 16
VisualStudioVersion = 16.0.30711.63
MinimumVisualStudioVersion = 10.0.40219.1
Project("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}") = "StudentsManager.Mvc", "StudentsManager.Mvc\StudentsManager.Mvc.csproj", "{96257DCC-5D90-4A2F-B0F4-3B78A2160DE7}"
EndProject
Project("{E53339B2-1760-4266-BCC7-CA923CBCF16C}") = "docker-compose", "docker-compose.dcproj", "{9DCA3501-FCA3-4416-ACFE-1C21A2816380}"
EndProject
Project("{9A19103F-16F7-4668-BE54-9A1E7A4F7556}") = "StudentsManager.Tests", "StudentsManager.Tests\StudentsManager.Tests.csproj", "{E4A50137-A3BD-4F5D-8541-75DBEABF18F0}"
EndProject
Global
	GlobalSection(SolutionConfigurationPlatforms) = preSolution
		Debug|Any CPU = Debug|Any CPU
		Release|Any CPU = Release|Any CPU
	EndGlobalSection
	GlobalSection(ProjectConfigurationPlatforms) = postSolution
		{96257DCC-5D90-4A2F-B0F4-3B78A2160DE7}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{96257DCC-5D90-4A2F-B0F4-3B78A2160DE7}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{96257DCC-5D90-4A2F-B0F4-3B78A2160DE7}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{96257DCC-5D90-4A2F-B0F4-3B78A2160DE7}.Release|Any CPU.Build.0 = Release|Any CPU
		{9DCA3501-FCA3-4416-ACFE-1C21A2816380}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{9DCA3501-FCA3-4416-ACFE-1C21A2816380}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{9DCA3501-FCA3-4416-ACFE-1C21A2816380}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{9DCA3501-FCA3-4416-ACFE-1C21A2816380}.Release|Any CPU.Build.0 = Release|Any CPU
		{E4A50137-A3BD-4F5D-8541-75DBEABF18F0}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
		{E4A50137-A3BD-4F5D-8541-75DBEABF18F0}.Debug|Any CPU.Build.0 = Debug|Any CPU
		{E4A50137-A3BD-4F5D-8541-75DBEABF18F0}.Release|Any CPU.ActiveCfg = Release|Any CPU
		{E4A50137-A3BD-4F5D-8541-75DBEABF18F0}.Release|Any CPU.Build.0 = Release|Any CPU
	EndGlobalSection
	GlobalSection(SolutionProperties) = preSolution
		HideSolutionNode = FALSE
	EndGlobalSection
	GlobalSection(ExtensibilityGlobals) = postSolution
		SolutionGuid = {B1868AE0-61C2-4EAE-8C23-A2DB984EA383}
	EndGlobalSection
EndGlobal



================================================
FILE: .dockerignore
================================================
**/.classpath
**/.dockerignore
**/.env
**/.git
**/.gitignore
**/.project
**/.settings
**/.toolstarget
**/.vs
**/.vscode
**/*.*proj.user
**/*.dbmdl
**/*.jfm
**/azds.yaml
**/bin
**/charts
**/docker-compose*
**/Dockerfile*
**/node_modules
**/npm-debug.log
**/obj
**/secrets.dev.yaml
**/values.dev.yaml
LICENSE
README.md


================================================
FILE: docs/Exploring the Impact of an Innovative E-Learning Development Environment on Mobile and Web Technology Education.md
================================================
# Exploring the Impact of an Innovative E-Learning Development Environment on Mobile and Web Technology Education

## Abstract

Education in mobile and web technology is continuously evolving, driven by advancements in software frameworks, cloud services, development workflows, and the integration of human-AI collaboration. 
In this context, traditional teaching methodologies often face challenges in delivering adequate opportunities for authentic practice, timely feedback, and sustained learner engagement.
This article examines the educational role of an open-source e-learning development environment designed to support mobile and web technology education through the integration of structured digital pedagogy and hands-on programming activity. Building on the theoretical foundations of educational technology and learning management systems, the study conceptualizes the platform not merely as a content delivery system but as a development-oriented learning environment that combines microlearning, gamified progression, adaptive assessment, collaborative work, and authentic software contribution practices. Methodologically, the article proposes a mixed-methods embedded case study with a quasi-experimental component involving more than 200 learners. The analysis focuses on learning outcomes, practical programming performance, engagement, feedback latency, and learner self-efficacy. The article argues that e-learning environments are most effective in technical education when pedagogical strategies are translated into concrete platform mechanisms, such as short coding tasks, adaptive quizzes, AI-assisted examination, peer collaboration, and repository-based project work. The findings are expected to contribute to scholarship on digital pedagogy, mobile and web technology education, and the design of next-generation e-learning systems that bridge theoretical instruction and authentic development practice.



================================================
FILE: StudentsManager.Mvc/appsettings.Development.json
================================================
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft": "Debug",
      "Microsoft.Hosting.Lifetime": "Debug"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=.\\SQLEXPRESS;Database=StudentsManagerDb;Integrated Security=true;TrustServerCertificate=True"
  },
  "MailSettings": {
    "Mail": "",
    "DisplayName": "",
    "Password": "",
    "Host": "smtp.gmail.com",
    "Port": 587
  },
  "ServiceBusSettings": {
    "AzureConnectionString": "",
    "QueueName": "ExaminationQueue"
  },
  "StorageSettings": {
    "AzureConnectionString": "",
    "PathBasis": "https://abc.blob.core.windows.net/"
  },
  "AgentFrameworkAppSettings": {
    "TargetUri": ""
  }
}



================================================
FILE: StudentsManager.Mvc/appsettings.json
================================================
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft": "Debug",
      "Microsoft.Hosting.Lifetime": "Debug"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": ""
  },
  "MailSettings": {
    "Mail": "<fromemail>",
    "DisplayName": "<displayname>",
    "Password": "<yourpasswordhere>",
    "Host": "smtp.gmail.com",
    "Port": 587
  },
  "ServiceBusSettings": {
    "AzureConnectionString": ""
  }
}



================================================
FILE: StudentsManager.Mvc/Dockerfile
================================================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["StudentsManager.Mvc/StudentsManager.Mvc.csproj", "StudentsManager.Mvc/"]
RUN dotnet restore "StudentsManager.Mvc/StudentsManager.Mvc.csproj"
COPY . .
WORKDIR "/src/StudentsManager.Mvc"
RUN dotnet build "StudentsManager.Mvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StudentsManager.Mvc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StudentsManager.Mvc.dll"]



================================================
FILE: StudentsManager.Mvc/Program.cs
================================================
using Microsoft.EntityFrameworkCore;
using StudentsManager.Mvc.Configurations;
using StudentsManager.Mvc.Persistence;
using StudentsManager.Mvc.Services.Tests;

var builder = WebApplication.CreateBuilder(args);
var configurationManager = builder.Configuration;

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSettings(configurationManager);
builder.Services.AddManagerDbContext(configurationManager.GetConnectionString("DefaultConnection"));
builder.Services.AddManagerIdentity();
builder.Services.AddAzureClients(configurationManager);
builder.Services.AddAzureTextAnalytic();
builder.Services.AddApplicationServices();
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews().AddNewtonsoftJson();
builder.Services.AddCrossOriginResourceSharing();
//builder.Services.AddHostedServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ManagerDbContext>();
    dbContext.Database.Migrate();
    var seeder = new DatabaseSeeder(dbContext);
    seeder.SeedDatabase();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("MyPolicy");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        "default",
        "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});

app.Run();


================================================
FILE: StudentsManager.Mvc/StudentsManager.Mvc.csproj
================================================
﻿<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <UserSecretsId>419f07b3-5d3b-4b48-a1a4-c02f01d27ed3</UserSecretsId>
    <DockerDefaultTargetOS>Linux</DockerDefaultTargetOS>
    <DockerComposeProjectPath>..\docker-compose.dcproj</DockerComposeProjectPath>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

  <ItemGroup>
    <Content Remove="Resources\basics.json" />
    <Content Remove="Resources\resource.json" />
  </ItemGroup>

  <ItemGroup>
    <EmbeddedResource Include="Resources\basics.json">
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </EmbeddedResource>
    <EmbeddedResource Include="Resources\resource.json">
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </EmbeddedResource>
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Azure.AI.OpenAI" Version="2.1.0" />
    <PackageReference Include="Azure.AI.Projects" Version="2.0.1" />
    <PackageReference Include="Azure.AI.TextAnalytics" Version="5.3.0" />
    <PackageReference Include="Azure.Core" Version="1.57.0" />
    <PackageReference Include="Azure.Identity" Version="1.21.0" />
    <PackageReference Include="Azure.Messaging.ServiceBus" Version="7.20.1" />
    <PackageReference Include="Azure.Storage.Blobs" Version="12.28.0" />
    <PackageReference Include="MailKit" Version="4.16.0" />
    <PackageReference Include="Marvin.StreamExtensions" Version="1.2.0" />
    <PackageReference Include="Microsoft.Agents.AI.Foundry" Version="1.5.0" />
    <PackageReference Include="Microsoft.AspNetCore.Identity.EntityFrameworkCore" Version="10.0.8" />
    <PackageReference Include="Microsoft.AspNetCore.Mvc.NewtonsoftJson" Version="10.0.8" />
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="10.0.8" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="10.0.8" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="10.0.8">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
    <PackageReference Include="Microsoft.Extensions.Azure" Version="1.14.0" />
    <PackageReference Include="Microsoft.VisualStudio.Azure.Containers.Tools.Targets" Version="1.23.0" />
    <PackageReference Include="Microsoft.VisualStudio.Web.CodeGeneration.Design" Version="10.0.2" />
    <PackageReference Include="MimeKit" Version="4.16.0" />
    <PackageReference Include="ModelContextProtocol" Version="1.3.0" />
  </ItemGroup>

  <ItemGroup>
    <Folder Include="Domain\Views\Students\" />
    <Folder Include="wwwroot\images\login\" />
    <Folder Include="wwwroot\images\home\" />
  </ItemGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.Build" Version="18.6.3" />
    <PackageReference Include="NuGet.Packaging" Version="7.6.0" />
    <PackageReference Include="NuGet.Protocol" Version="6.12.5" />
    <Content Update="wwwroot\css\forum.css">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
    <Content Update="wwwroot\css\loader.min.css">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
    <Content Update="wwwroot\css\site.css">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
    <Content Update="wwwroot\css\style.css">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
    <Content Update="wwwroot\css\table.css">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
    <Content Update="wwwroot\favicon.ico">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
    <Content Update="wwwroot\files\fonts\raleway-medium-webfont.woff">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
    <Content Update="wwwroot\files\fonts\raleway-medium-webfont.woff2">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
    <Content Update="wwwroot\files\fonts\raleway-regular-webfont.woff">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
    <Content Update="wwwroot\files\fonts\raleway-regular-webfont.woff2">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
    <Content Update="wwwroot\files\fonts\raleway-semibold-webfont.woff">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
    <Content Update="wwwroot\files\fonts\raleway-semibold-webfont.woff2">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
    <Content Update="wwwroot\images\coursework\coursework-pic.png">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
    <Content Update="wwwroot\images\home\4.jpg">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
    <Content Update="wwwroot\images\home\ex4.jpg">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
    <Content Update="wwwroot\images\home\slider-bg.jpg">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
    <Content Update="wwwroot\images\home\wave-bg-gray.svg">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
    <Content Update="wwwroot\images\home\wave-bg.svg">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
    <Content Update="wwwroot\images\icons\checkbox-white.svg">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
    <Content Update="wwwroot\images\icons\checkbox.svg">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
    <Content Update="wwwroot\images\icons\checked-color.svg">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
    <Content Update="wwwroot\images\icons\checked-white.svg">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
    <Content Update="wwwroot\images\icons\checked.svg">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
    <Content Update="wwwroot\images\icons\scroll-down.svg">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
    <Content Update="wwwroot\images\login\login-step1.jpg">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
    <Content Update="wwwroot\images\login\login-step2.jpg">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
    <Content Update="wwwroot\images\login\login-step3.png">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
    <Content Update="wwwroot\images\test\study.jpg">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
    <Content Update="wwwroot\images\test\test-result-image.jpg">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
    <Content Update="wwwroot\js\loader.js">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </Content>
  </ItemGroup>

</Project>



================================================
FILE: StudentsManager.Mvc/Configurations/DependenciesConfiguration.cs
================================================
﻿using Microsoft.AspNetCore.Authentication.Cookies;
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


================================================
FILE: StudentsManager.Mvc/Configurations/Extensions.cs
================================================
﻿using StudentsManager.Mvc.Domain.Entities;
using StudentsManager.Mvc.Domain.Views.Students;
using System.Text;

namespace StudentsManager.Mvc.Configurations;

public static class Extensions
{
    public static StudentView? ToView(this User? user)
    {
        if (user == null)
        {
            return null;
        }
        return new StudentView(user.UserName, user.Email.Encode(), user.FullName, user.FacultyNumber, user.Base64EncodePicture);
    }

    public static string? Encode(this string? value)
    {
        return string.IsNullOrEmpty(value) ? value : Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
    }
}


================================================
FILE: StudentsManager.Mvc/Controllers/AuthController.cs
================================================
﻿using Microsoft.AspNetCore.Mvc;
using StudentsManager.Mvc.Controllers._Base;
using StudentsManager.Mvc.Domain.Inputs.Auth;
using StudentsManager.Mvc.Domain.Views;
using StudentsManager.Mvc.Services.AppSettings;
using StudentsManager.Mvc.Services.Auth;
using static StudentsManager.Mvc.Services.Auth.AuthConstants;

namespace StudentsManager.Mvc.Controllers
{
    public class AuthController(IAuthService service, IScreenSettingService screenSettingService) : BaseController
    {
        [HttpGet]
        public IActionResult Login(string? returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                TempData["returnUrl"] = returnUrl;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Credentials model)
        {
            var result = await service.LoginAsync(model);
            if (result == null)
            {
                return RedirectToAction("Problem", new { errorMessage = LoginErrorMessage });
            }

            var returnUrl = TempData["returnUrl"]?.ToString();
            return string.IsNullOrEmpty(returnUrl)
                ? RedirectToHome()
                : RedirectToReturnUrl(returnUrl);
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var screenSetting = await screenSettingService.RegistrationScreenSettingAsync;
            if (screenSetting == null)
            {
                return View();
            }
            return screenSetting.Enabled ? View() : RedirectToHome();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Register model)
        {
            var result = await service.RegisterAsync(model);
            return result == null
                ? RedirectToAction("Problem", new { errorMessage = RegisterErrorMessage })
                : RedirectToHome();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await service.LogoutAsync();
            return RedirectToHome();
        }

        [HttpGet]
        public IActionResult Problem(string errorMessage)
        {
            return View(new Error(errorMessage));
        }

        [HttpGet]
        public async Task<IActionResult> ResetPassword()
        {
            var screenSetting = await screenSettingService.RegistrationScreenSettingAsync;
            if (screenSetting == null)
            {
                return View();
            }
            return screenSetting.Enabled ? View() : RedirectToHome();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPassword model)
        {
            var result = await service.ResetPasswordAsync(model);
            return result == null
                ? RedirectToAction("Problem", new { errorMessage = UnexpectedErrorMessage })
                : RedirectToHome();
        }
    }
}


================================================
FILE: StudentsManager.Mvc/Controllers/ChatbotController.cs
================================================
using System.Text;
using System.Text.Json;
using Azure;
using Azure.AI.OpenAI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OpenAI.Chat;
using StudentsManager.Mvc.Domain.Entities;
using StudentsManager.Mvc.Persistence;
using StudentsManager.Mvc.Settings;

namespace StudentsManager.Mvc.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChatbotController(
    ManagerDbContext managerDbContext,
    IOptions<ServiceBusSettings> options)
    : ControllerBase
{
    private const string SystemPrompt = """
                                        You are a senior JavaScript quiz evaluator. Your task is to analyze the given quiz answers and provide:
                                        1. A grade between 2 and 6.
                                        2. Overall feedback summarizing the performance.
                                        
                                        Respond in JSON format using the structure below:
                                        {
                                          "grade": <2-6>,
                                          "overallFeedback": "<summary>"
                                        }
                                        """;

    [HttpGet("examination-answers/{studentId:guid}")]
    public async Task<IActionResult> GetByStudent([FromRoute] Guid studentId, CancellationToken cancellationToken)
    {
        var result = await managerDbContext.ExaminationAnswers
            .AsNoTracking()
            .Where(answer => answer.UserId == studentId)
            .ToListAsync(cancellationToken);

        return Ok(result);
    }

    [HttpPost("examination-answers")]
    public async Task<IActionResult> EvaluateAnswers([FromBody] ChatbotExaminationInput input, CancellationToken cancellationToken)
    {
        if (input.Answers.Length == 0)
            return BadRequest("Answers cannot be empty.");

        var settings = options.Value;
        var azureClient = new AzureOpenAIClient(new Uri(settings.AzureConnectionString), new AzureKeyCredential(settings.QueueName));
        var chatClient = azureClient.GetChatClient("gpt-4.1-nano");

        var serializedAnswers = JsonSerializer.Serialize(input.Answers);
        var userPrompt = $"Evaluate these JavaScript quiz answers:\n{serializedAnswers}";
        var responseText = string.Empty;
        var errorMessage = string.Empty;

        try
        {
            var requestOptions = new ChatCompletionOptions
            {
                Temperature = 1.0f,
                TopP = 1.0f,
                FrequencyPenalty = 0.0f,
                PresencePenalty = 0.0f,
                MaxOutputTokenCount = 13107
            };

            var response = await chatClient.CompleteChatAsync(
            [
                new SystemChatMessage(SystemPrompt),
                new UserChatMessage(userPrompt)
            ],
            requestOptions, cancellationToken);

            var sb = new StringBuilder();
            foreach (var part in response.Value.Content)
            {
                sb.Append(part.Text);
            }

            responseText = sb.ToString();
        }
        catch (Exception ex)
        {
            errorMessage = ex.Message;
        }

        var entity = new ExaminationAnswer
        {
            Id = Guid.NewGuid(),
            UserId = input.UserId,
            CreatedOn = DateTime.UtcNow,
            Result = serializedAnswers,
            ContentType = "chatbot",
            WasSuccessfullyProcessed = !string.IsNullOrEmpty(responseText),
            Form = responseText,
            ErrorMessage = errorMessage
        };

        await managerDbContext.AddAsync(entity, cancellationToken);
        await managerDbContext.SaveChangesAsync(cancellationToken);

        return Ok(entity);
    }
}

public record ChatbotAnswerItem(string QuestionId, string QuestionText, string Answer);

public record ChatbotExaminationInput(Guid UserId, ChatbotAnswerItem[] Answers);



================================================
FILE: StudentsManager.Mvc/Controllers/ClickController.cs
================================================
﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentsManager.Mvc.Domain.Inputs.Click;
using StudentsManager.Mvc.Services.Auth;
using StudentsManager.Mvc.Services.Clicks;

namespace StudentsManager.Mvc.Controllers
{
    [Authorize]
    public class ClickController : Controller
    {
        private readonly IPrincipalService _principalService;
        private readonly IClicksService _service;

        public ClickController(IClicksService service, IPrincipalService principalService)
        {
            _service = service;
            _principalService = principalService;
        }

        // GET: Click
        public ActionResult Index()
        {
            return View();
        }

        //GET: Click/Details?type=professional
        public ActionResult Details([FromQuery] string type)
        {
            TempData["QuestionsContentType"] = type;
            var result = _service.GetQuestionsContent(type);
            return Json(result);
        }

        // POST: Click/SaveResults
        [HttpPost]
        public async Task<IActionResult> SaveResults([FromForm] ClickResultsRootObject rootObject)
        {
            var questionsContentType = TempData["QuestionsContentType"].ToString() ?? throw new ArgumentNullException();
            await _service.SaveAndPublishAnswersAsync(rootObject, _principalService.GetUserIdByClaimsPrincipal(User),
                questionsContentType);
            return Ok(new SaveResultsModel(true));
        }
    }
}


================================================
FILE: StudentsManager.Mvc/Controllers/EventsController.cs
================================================
﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentsManager.Mvc.Domain.Entities;
using StudentsManager.Mvc.Domain.Inputs;
using StudentsManager.Mvc.Persistence;

namespace StudentsManager.Mvc.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly ManagerDbContext _dbContext;

    public EventsController(ManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetByUser(string userId)
    {
        var result = await _dbContext
            .Events
            .Where(@event => @event.UserId == userId)
            .ToListAsync();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(EventInput input)
    {
        var entity = new Event
        {
            Id = Guid.NewGuid(),
            UserId = input.UserId,
            DatetimeUtc = DateTime.UtcNow,
            Type = input.Type,
            Data = input.Data
        };
        _dbContext.Events.Add(entity);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetByUser), new { userId = entity.UserId }, entity);
    }

}



================================================
FILE: StudentsManager.Mvc/Controllers/ExaminationSettingsController.cs
================================================
﻿using Microsoft.AspNetCore.Mvc;
using StudentsManager.Mvc.Services.CourseExaminations;

namespace StudentsManager.Mvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExaminationSettingsController : ControllerBase
    {
        private readonly IExaminationSettingsService _service;

        public ExaminationSettingsController(IExaminationSettingsService service)
        {
            _service = service;
        }

        [HttpGet("enable/first")]
        public async Task<IActionResult> EnableFirst()
        {
            await _service.MakeFirstAvailableAsync();
            return Ok("First test enabled!");
        }

        [HttpGet("disable/first")]
        public async Task<IActionResult> DisableFirst()
        {
            await _service.MakeFirstUnavailableAsync();
            return Ok("First test disabled!");
        }

        [HttpGet("enable/second")]
        public async Task<IActionResult> EnableSecond()
        {
            await _service.MakeSecondAvailableAsync();
            return Ok("Second tests enabled!");
        }

        [HttpGet("disable/second")]
        public async Task<IActionResult> DisableSecond()
        {
            await _service.MakeSecondUnavailableAsync();
            return Ok("Second tests disabled!");
        }
    }
}


================================================
FILE: StudentsManager.Mvc/Controllers/ForumController.cs
================================================
﻿using Microsoft.AspNetCore.Mvc;
using StudentsManager.Mvc.Domain.Inputs.Forum;
using StudentsManager.Mvc.Services.Auth;
using StudentsManager.Mvc.Services.Forum;

namespace StudentsManager.Mvc.Controllers
{
    public class ForumController : Controller
    {
        private readonly IPrincipalService _principalService;
        private readonly IForumService _service;

        public ForumController(IForumService service, IPrincipalService principalService)
        {
            _service = service;
            _principalService = principalService;
        }

        // GET: /Forum
        public async Task<IActionResult> Index([FromQuery] int pageNumber = 1)
        {
            var result = await _service.GetPaginatedListAsync(pageNumber);
            return View(result);
        }

        // POST: /Forum
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IFormCollection collection)
        {
            var tryQuestionValue = collection.TryGetValue("forumQuestion", out var questionValue);
            if (!tryQuestionValue || string.IsNullOrEmpty(questionValue)) return RedirectToAction(nameof(Index));

            await _service.SaveQuestionAsync(
                questionValue.ToString(),
                _principalService.GetUserIdByClaimsPrincipal(User));

            return RedirectToAction(nameof(Index));
        }

        // POST: Forum/Comment
        [HttpPost]
        [Route("comment")]
        public async Task<IActionResult> PostComment([FromBody] Comment input)
        {
            var result = await _service.SaveCommentAsync(input, _principalService.GetUserIdByClaimsPrincipal(User));
            return Json(result);
        }
    }
}


================================================
FILE: StudentsManager.Mvc/Controllers/HomeController.cs
================================================
﻿using Microsoft.AspNetCore.Mvc;
using StudentsManager.Mvc.Services.CourseExaminations;

namespace StudentsManager.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IExaminationSettingsService _settingsService;

        public HomeController(IExaminationSettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public async Task<IActionResult> Index()
        {
            var examinationSettingsView = await _settingsService.GetViewAsync();
            return View(examinationSettingsView);
        }
    }
}


================================================
FILE: StudentsManager.Mvc/Controllers/HomeworksController.cs
================================================
using Microsoft.AspNetCore.Mvc;
using StudentsManager.Mvc.Services.Homeworks;

namespace StudentsManager.Mvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeworksController : ControllerBase
    {
        private readonly IHomeworksService _homeworksService;

        public HomeworksController(IHomeworksService homeworksService)
        {
            _homeworksService = homeworksService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUser(Guid userId)
        {
            var result = await _homeworksService.GetByUserAsync(userId);
            return Ok(result);
        }
    }
}


================================================
FILE: StudentsManager.Mvc/Controllers/LoginController.cs
================================================
using Microsoft.AspNetCore.Mvc;
using StudentsManager.Mvc.Controllers._Base;
using StudentsManager.Mvc.Domain.Inputs.Auth;
using StudentsManager.Mvc.Services.Auth;

namespace StudentsManager.Mvc.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoginController(IAuthService service) : BaseController
{
    /// <summary>
    /// API endpoint for user login.
    /// </summary>
    /// <param name="credentials"></param>
    /// <returns>
    /// Ok (200) with the user ID if the login is successful.
    /// </returns>
    /// <returns>
    /// Unauthorized (401) if the email or password is invalid.
    /// </returns>
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] Credentials credentials)
    {
        var result = await service.LoginAsync(credentials);
        if (result == null)
        {
            return Unauthorized(new { message = "Invalid email or password." });
        }
        return Ok(new { userId = result.Id });
    }
}



================================================
FILE: StudentsManager.Mvc/Controllers/SandboxController.cs
================================================
﻿using Microsoft.AspNetCore.Mvc;
using StudentsManager.Mvc.Domain.Inputs.Messaging;
using StudentsManager.Mvc.Services.AI;
using StudentsManager.Mvc.Services.Messaging;

namespace StudentsManager.Mvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SandboxController(
        IMailService mailService,
        IAzureServiceBusSender serviceBusSender,
        IFoundryAgent foundryAgent,
        IAgentFrameworkService agentFrameworkService)
        : ControllerBase
    {
        private readonly IMailService _mailService = mailService;
        private readonly IAzureServiceBusSender _serviceBusSender = serviceBusSender;
        private readonly IFoundryAgent _foundryAgent = foundryAgent;
        private readonly IAgentFrameworkService _agentFrameworkService = agentFrameworkService;

        [HttpGet("agent")]
        public async Task<IActionResult> TestAgentFrameworkService()
        {
            var result = await _agentFrameworkService.AskAsync("Tell me a one-sentence Harvard fact.");
            return Ok(result);
        }

        [HttpGet("mail")]
        public async Task<IActionResult> TestMailService()
        {
            var mailRequest = new MailRequest
            {
                ToEmail = "d_yugioh@abv.bg",
                Subject = $"Test #{Guid.NewGuid()}",
                Body = $"Dear, {Guid.NewGuid()}, We are Testing {Guid.NewGuid()}. Bye {Guid.NewGuid()}"
            };
            await _mailService.SendEmailAsync(mailRequest);
            return Ok();
        }

        [HttpGet("bus")]
        public async Task<IActionResult> TestAzureServiceBusSender()
        {
            try
            {
                await _serviceBusSender.SendAsync(Guid.NewGuid().ToString());
                return Ok("READY!");
            }
            catch (Exception exception)
            {
                return Ok(exception);
            }
        }

        [HttpGet("foundry")]
        public async Task<IActionResult> TestFoundryAgent([FromQuery] string message = "Hello, what can you do?")
        {
            try
            {
                var result = await _foundryAgent.Test();
                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(exception.Message);
            }
        }

        [HttpGet("foundry/chat")]
        public async Task<IActionResult> TestChatWithSystemPrompt(
            [FromQuery] string systemPrompt = "You are a helpful assistant.",
            [FromQuery] string userMessage = "Hello, what can you do?")
        {
            try
            {
                var result = await _foundryAgent.ChatWithSystemPrompt(systemPrompt, userMessage);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(exception.Message);
            }
        }

        [HttpGet("foundry/chat-options")]
        public async Task<IActionResult> TestChatWithOptions(
            [FromQuery] string userMessage = "Hello, what can you do?",
            [FromQuery] float temperature = 0.7f,
            [FromQuery] int maxTokens = 500)
        {
            try
            {
                var result = await _foundryAgent.ChatWithOptions(userMessage, temperature, maxTokens);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(exception.Message);
            }
        }

        [HttpGet("foundry/stream")]
        public async Task TestStreamChat([FromQuery] string userMessage = "Hello, what can you do?")
        {
            Response.ContentType = "text/plain";
            try
            {
                await foreach (var chunk in _foundryAgent.StreamChat(userMessage))
                {
                    await Response.WriteAsync(chunk);
                    await Response.Body.FlushAsync();
                }
            }
            catch (Exception exception)
            {
                await Response.WriteAsync($"Error: {exception.Message}");
            }
        }

        [HttpGet("foundry/multi-turn")]
        public async Task<IActionResult> TestMultiTurnConversation(
            [FromQuery] string userMessage = "What are best practices for student management?")
        {
            try
            {
                var messages = new List<(string Role, string Content)>
                {
                    ("system", "You are a helpful assistant for a student management system."),
                    ("user", userMessage)
                };
                var result = await _foundryAgent.MultiTurnConversation(messages);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(exception.Message);
            }
        }

        [HttpGet("foundry/json-output")]
        public async Task<IActionResult> TestChatWithJsonOutput(
            [FromQuery] string userMessage = "Analyze this student feedback: The course was excellent but the schedule was too tight.")
        {
            try
            {
                var result = await _foundryAgent.ChatWithJsonOutput(userMessage);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(exception.Message);
            }
        }

        [HttpGet("foundry/tool-calling")]
        public async Task<IActionResult> TestChatWithToolCalling(
            [FromQuery] string userMessage = "What is the grade for student S001 in course CS101?")
        {
            try
            {
                var result = await _foundryAgent.ChatWithToolCalling(userMessage);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(exception.Message);
            }
        }

        [HttpGet("foundry/summarize")]
        public async Task<IActionResult> TestSummarizeWithStopSequences(
            [FromQuery] string text = "The student management system provides tools for tracking grades, attendance, and course enrollment.")
        {
            try
            {
                var result = await _foundryAgent.SummarizeWithStopSequences(text);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(exception.Message);
            }
        }

        [HttpGet("foundry/analyze-image")]
        public async Task<IActionResult> TestAnalyzeImage(
            [FromQuery] string imageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/47/PNG_transparency_demonstration_1.png/300px-PNG_transparency_demonstration_1.png",
            [FromQuery] string question = "What do you see in this image?")
        {
            try
            {
                var result = await _foundryAgent.AnalyzeImage(new Uri(imageUrl), question);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(exception.Message);
            }
        }

        [HttpGet("foundry/reproducible")]
        public async Task<IActionResult> TestReproducibleChat(
            [FromQuery] string userMessage = "List 3 benefits of online learning.",
            [FromQuery] long seed = 42)
        {
            try
            {
                var result = await _foundryAgent.ReproducibleChat(userMessage, seed);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(exception.Message);
            }
        }

        [HttpGet("foundry/logprobs")]
        public async Task<IActionResult> TestChatWithLogprobs(
            [FromQuery] string userMessage = "What is the capital of France?")
        {
            try
            {
                var result = await _foundryAgent.ChatWithLogprobs(userMessage);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(exception.Message);
            }
        }

        [HttpGet("foundry/nucleus-sampling")]
        public async Task<IActionResult> TestGenerateWithNucleusSampling(
            [FromQuery] string userMessage = "Write a short creative paragraph about a student's first day.",
            [FromQuery] float topP = 0.1f)
        {
            try
            {
                var result = await _foundryAgent.GenerateWithNucleusSampling(userMessage, topP);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(exception.Message);
            }
        }

        [HttpGet("toolboxes")]
        public async Task<IActionResult> TestToolboxes([FromQuery] string message = "Review https://github.com/profjordanov/students-manager")
        {
            try
            {
                var result = await _foundryAgent.ChatWithToolbox(message);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return Ok(exception.Message);
            }
        }
    }
}


================================================
FILE: StudentsManager.Mvc/Controllers/SettingsController.cs
================================================
﻿using Microsoft.AspNetCore.Mvc;
using StudentsManager.Mvc.Services.AppSettings;

namespace StudentsManager.Mvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly IScreenSettingService _settingService;

        public SettingsController(IScreenSettingService settingService)
        {
            _settingService = settingService;
        }

        [HttpGet("enable/reg")]
        public async Task<IActionResult> EnableRegistrationScreen()
        {
            await _settingService.UpdateRegistrationScreenSettingAsync(true);
            return Ok();
        }

        [HttpGet("disable/reg")]
        public async Task<IActionResult> DisableRegistrationScreen()
        {
            await _settingService.UpdateRegistrationScreenSettingAsync(false);
            return Ok();
        }
    }
}


================================================
FILE: StudentsManager.Mvc/Controllers/SlidoController.cs
================================================
using Microsoft.AspNetCore.Mvc;
using StudentsManager.Mvc.Domain.Inputs.Forum;
using StudentsManager.Mvc.Services.Forum;

namespace StudentsManager.Mvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlidoController(IForumService service) : ControllerBase
    {
        // GET: api/slido?limit=20&skip=0
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int limit, [FromQuery] int skip)
        {
            var forumQuestions = await service.GetAsync(limit, skip);
            var result = forumQuestions.Select(question => new QuestionCommentsView(
                question.Id,
                question.Description,
                question.Comments?.Select(comment => comment.Description).ToArray()
            )).ToList();
            return Ok(result);
        }

        // POST: api/slido/comment
        [HttpPost("comment")]
        public async Task<IActionResult> PostComment([FromBody] Comment input)
        {
            if (string.IsNullOrWhiteSpace(input.Description) || input.ForumQuestionId == 0)
                return BadRequest("Comment cannot be empty");

            await service.SaveCommentAsync(input,Guid.Parse("1eac9820-5e6e-4d10-6e94-08de36f40f78"));

            return Ok("Comment posted successfully");
        }
        
        // GET: api/slido/questions?limit=20&skip=0
        [HttpGet("questions")]
        public async Task<IActionResult> GetQuestions([FromQuery] int limit, [FromQuery] int skip)
        {
            var result = await service.GetSlidoQuestionsAsync(limit, skip);
            return Ok(result);
        }

        // POST: api/slido/question
        [HttpPost("question")]
        public async Task<IActionResult> PostQuestion([FromBody] QuestionInput input)
        {
            if (string.IsNullOrWhiteSpace(input.Question))
                return BadRequest("Question cannot be empty");

            await service.SaveQuestionAsync(
                input.Question,
                Guid.Parse("1eac9820-5e6e-4d10-6e94-08de36f40f78"));

            return Ok("Question posted successfully");
        }
    }

    public record struct QuestionInput(string Question);
    public record struct QuestionCommentsView(int ForumQuestionId, string ForumQuestionDescription, string[]? Comments);
}



================================================
FILE: StudentsManager.Mvc/Controllers/StudentsController.cs
================================================
using Microsoft.AspNetCore.Mvc;
using StudentsManager.Mvc.Domain.Inputs.Students;
using StudentsManager.Mvc.Services.CourseExaminations;
using StudentsManager.Mvc.Services.Students;

namespace StudentsManager.Mvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController(IStudentsService service, IStudentCourseExaminationService examinationService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var isAuthorized = Request.Headers.TryGetValue("Authorization", out var authorizationHeader);
            if (!isAuthorized) return Unauthorized();
            var authBytes = "guest:guest"u8.ToArray();
            var base64Auth = Convert.ToBase64String(authBytes);

            var authorizationValue = authorizationHeader.ToString().Split(' ')[1];
            if (authorizationValue != base64Auth) return Unauthorized();

            return Ok(await service.AllAsync());
        }

        [HttpGet("{facultyNumber}")]
        public async Task<IActionResult> GetByFacultyNumber([FromRoute] string facultyNumber)
        {
            var result = await service.GetByFacultyNumberAsync(facultyNumber);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPut("picture")]
        public async Task<IActionResult> UpdatePicture([FromBody] UpdatePicture input)
        {
            var result = await service.UpdatePictureAsync(input);
            if (result == null) return BadRequest();
            return Ok();
        }

        [HttpPatch("examination")]
        public async Task<IActionResult> SetStudentExaminationScore([FromBody] StudentExaminationScore input)
        {
            var isAuthorized = Request.Headers.TryGetValue("classified", out var authorizationHeader);
            if (!isAuthorized || authorizationHeader != "s3cR37") return Unauthorized();
            await examinationService.SetScoreAsync(input.UserId, input.Type, input.Score);
            return Ok();
        }

        [HttpGet("profile/{studentId}")]
        public async Task<IActionResult> GetStudentProfileView([FromRoute] Guid studentId)
        {
            var result = await service.GetStudentProfileViewAsync(studentId);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}



================================================
FILE: StudentsManager.Mvc/Controllers/TestController.cs
================================================
﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentsManager.Mvc.Domain.Inputs;
using StudentsManager.Mvc.Services.Auth;
using StudentsManager.Mvc.Services.Options;
using StudentsManager.Mvc.Services.Tests;
using StudentsManager.Mvc.Services.Topics;

namespace StudentsManager.Mvc.Controllers
{
    [Authorize]
    public class TestController : Controller
    {
        private readonly IPrincipalService _principalService;
        private readonly ITestsService _testsService;
        private readonly ITopicsService _topicsService;
        private readonly IUserAnswersService _userAnswersService;
        private readonly IUserTopicResultsService _userTopicResultsService;

        public TestController(
            ITopicsService topicsService,
            ITestsService testsService,
            IPrincipalService principalService,
            IUserAnswersService userAnswersService,
            IUserTopicResultsService userTopicResultsService)
        {
            _topicsService = topicsService;
            _testsService = testsService;
            _principalService = principalService;
            _userAnswersService = userAnswersService;
            _userTopicResultsService = userTopicResultsService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _principalService.GetUserIdByClaimsPrincipal(User);
            var result = await _topicsService.GetUserTopicResultsAsync(userId);
            return View(result);
        }

        [HttpGet]
        [Route("start/{topicId}")]
        public async Task<IActionResult> Start([FromRoute] Guid topicId)
        {
            var userId = _principalService.GetUserIdByClaimsPrincipal(User);
            if (await _topicsService.HasUserTopicResultAsync(userId, topicId))
                return RedirectToAction("Result", new { topicId });
            var result = await _testsService.GetTestAsync(topicId, userId);

            TempData["topicId"] = topicId;
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUserAnswer([FromBody] ChosenOption optionModel)
        {
            var userId = _principalService.GetUserIdByClaimsPrincipal(User);
            await _userAnswersService.AddAsync(userId, optionModel.ChosenOptionId);
            return Ok();
        }

        [HttpPost]
        [Route("start/SubmitAnswers")]
        public async Task<IActionResult> SubmitAnswers([FromQuery] string v, [FromBody] TestBindingModel model)
        {
            var topicId = TempData["topicId"].ToString() ?? throw new ArgumentNullException();
            var userId = _principalService.GetUserIdByClaimsPrincipal(User);

            await _userTopicResultsService.ProcessAsync(Guid.Parse(topicId), userId);

            var result = new
            {
                redirect = $"{Url.Action("Done", "Test")}"
            };

            return Json(result);
        }

        [HttpGet]
        public IActionResult Done()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Result(Guid topicId)
        {
            var userId = _principalService.GetUserIdByClaimsPrincipal(User);
            var result = await _userTopicResultsService.GetSingleAsync(topicId, userId);
            return View(result);
        }
    }
}


================================================
FILE: StudentsManager.Mvc/Controllers/_Base/BaseController.cs
================================================
﻿using Microsoft.AspNetCore.Mvc;

namespace StudentsManager.Mvc.Controllers._Base
{
    public class BaseController : Controller
    {
        protected IActionResult RedirectToHome()
        {
            return RedirectToAction("Index", "Home");
        }

        protected IActionResult RedirectToReturnUrl(string returnUrl)
        {
            return Url.IsLocalUrl(returnUrl) ? Redirect(returnUrl) : RedirectToHome();
        }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/CustomDataStructures/PaginatedList.cs
================================================
﻿using Microsoft.EntityFrameworkCore;

namespace StudentsManager.Mvc.Domain.CustomDataStructures
{
    public class PaginatedList<T> : List<T>
        where T : class
    {
        public PaginatedList(IEnumerable<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            AddRange(items);
        }

        public int PageIndex { get; }
        public int TotalPages { get; }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Entities/Course.cs
================================================
﻿namespace StudentsManager.Mvc.Domain.Entities
{
    public class Course
    {
        public Guid Id { get; set; }
        
        public string Description { get; set; }

        public IEnumerable<Topic>? Topics { get; set; }
        public IEnumerable<UserCourse>? UserCourses { get; set; }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Entities/CourseExamination.cs
================================================
﻿namespace StudentsManager.Mvc.Domain.Entities
{
    public class CourseExamination
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string ResourceUrl { get; set; }

        public string? ExtraResourceUrl { get; set; }

        public ICollection<StudentCourseExamination>? StudentExaminations { get; set; }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Entities/CourseExaminationSetting.cs
================================================
﻿namespace StudentsManager.Mvc.Domain.Entities
{
    public class CourseExaminationSetting
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public bool Enabled { get; set; }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Entities/Event.cs
================================================
﻿namespace StudentsManager.Mvc.Domain.Entities;

public class Event
{
    public Guid Id { get; set; }
    public string? UserId { get; set; }
    public DateTime DatetimeUtc { get; set; }

    public string? Type { get; set; }

    public string? Data { get; set; }
}


================================================
FILE: StudentsManager.Mvc/Domain/Entities/ExaminationAnswer.cs
================================================
﻿using StudentsManager.Mvc.Domain.Entities._Base;

namespace StudentsManager.Mvc.Domain.Entities
{
    public class ExaminationAnswer : IAuditInfo
    {
        public Guid Id { get; set; }

        public string? Result { get; set; }
        public string? Form { get; set; }
        public string? ContentType { get; set; }

        public bool WasSuccessfullyProcessed { get; set; }
        public string? ErrorMessage { get; set; }

        // Reference
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Entities/ForumComment.cs
================================================
﻿namespace StudentsManager.Mvc.Domain.Entities
{
    public class ForumComment
    {
        public int Id { get; set; }

        public string Description { get; set; }

        // Reference
        public int ForumQuestionId { get; set; }
        public ForumQuestion? ForumQuestion { get; set; }

        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Entities/ForumQuestion.cs
================================================
﻿namespace StudentsManager.Mvc.Domain.Entities
{
    public class ForumQuestion
    {
        public int Id { get; set; }

        public string Description { get; set; }

        // Reference
        public Guid UserId { get; set; }
        public User? User { get; set; }

        public ICollection<ForumComment>? Comments { get; set; }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Entities/Homework.cs
================================================
﻿using System.ComponentModel.DataAnnotations.Schema;
using File = StudentsManager.Mvc.Domain.Entities._Base.File;

namespace StudentsManager.Mvc.Domain.Entities
{
    public class Homework : File
    {
        public int Id { get; set; }

        public string? RepositoryLink { get; set; }

        [NotMapped] 
        public string? Link => string.IsNullOrEmpty(Path) ? RepositoryLink : Path;

        public Guid UserId { get; set; }
        public User? User { get; set; }

        public Guid TopicId { get; set; }
        public Topic? Topic { get; set; }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Entities/QuestionOption.cs
================================================
﻿namespace StudentsManager.Mvc.Domain.Entities
{
    public class QuestionOption
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public bool IsTextOnly { get; set; }

        public bool IsCorrect { get; set; }

        public string? ImageAddress { get; set; }

        // Relations
        public Guid TestQuestionId { get; set; }

        public TestQuestion? TestQuestion { get; set; }

        public ICollection<UserAnswer>? UserAnswers { get; set; }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Entities/Role.cs
================================================
﻿using Microsoft.AspNetCore.Identity;

namespace StudentsManager.Mvc.Domain.Entities
{
    public class Role : IdentityRole<Guid>
    {
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Entities/ScreenSetting.cs
================================================
﻿namespace StudentsManager.Mvc.Domain.Entities
{
    public class ScreenSetting
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public bool Enabled { get; set; }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Entities/StudentCourseExamination.cs
================================================
﻿namespace StudentsManager.Mvc.Domain.Entities
{
    public class StudentCourseExamination
    {
        public Guid Id { get; set; }

        public int Score { get; set; }

        public Guid UserId { get; set; }
        public User? User { get; set; }

        public int CourseExaminationId { get; set; }
        public CourseExamination? CourseExamination { get; set; }

        public ICollection<StudentCourseExaminationUpload>? Uploads { get; set; }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Entities/StudentCourseExaminationUpload.cs
================================================
﻿using File = StudentsManager.Mvc.Domain.Entities._Base.File;

namespace StudentsManager.Mvc.Domain.Entities
{
    public class StudentCourseExaminationUpload : File
    {
        public int Id { get; set; }

        public Guid StudentCourseExaminationId { get; set; }

        public StudentCourseExamination? StudentCourseExamination { get; set; }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Entities/TestQuestion.cs
================================================
﻿namespace StudentsManager.Mvc.Domain.Entities
{
    public class TestQuestion
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        // Relations
        public Guid TopicId { get; set; }

        public Topic? Topic { get; set; }

        public ICollection<QuestionOption>? Options { get; set; }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Entities/Topic.cs
================================================
﻿namespace StudentsManager.Mvc.Domain.Entities
{
    public class Topic
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
        public int SequenceNumber { get; set; }
        public string ExerciseFileUrl { get; set; }
        public string ResourcesUrl { get; set; }
        public DateTimeOffset CanBeSeenAfter { get; set; }
        public string? VideoLinkFromPreviousYear { get; set; }

        public Guid? CourseId { get; set; }
        public Course? Course { get; set; }

        public ICollection<TestQuestion>? TestQuestions { get; set; }
        public ICollection<UserTopicResult>? UserTopics { get; set; }
        public ICollection<Homework>? Homeworks { get; set; }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Entities/User.cs
================================================
﻿using Microsoft.AspNetCore.Identity;
using StudentsManager.Mvc.Domain.Entities._Base;

namespace StudentsManager.Mvc.Domain.Entities
{
    public class User : IdentityUser<Guid>, IDeletableEntity, IAuditInfo
    {
        public string FullName { get; set; }
        public string FacultyNumber { get; set; }
        public string? Base64EncodePicture { get; set; }

        public ICollection<UserAnswer>? UserAnswers { get; set; }
        public ICollection<UserTopicResult>? UserTopicResults { get; set; }
        public ICollection<Homework>? Homeworks { get; set; }
        public ICollection<StudentCourseExamination>? StudentExaminations { get; set; }
        public ICollection<ExaminationAnswer>? ExaminationAnswers { get; set; }
        public IEnumerable<UserCourse>? UserCourses { get; set; }
        public ICollection<ForumQuestion>? ForumQuestions { get; set; }
        public ICollection<ForumComment>? ForumComments { get; set; }

        public ICollection<UserCoursework>? Courseworks { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Entities/UserAnswer.cs
================================================
﻿namespace StudentsManager.Mvc.Domain.Entities
{
    public class UserAnswer
    {
        public int Id { get; set; }

        // Reference
        public Guid UserId { get; set; }
        public User? User { get; set; }

        public int QuestionOptionId { get; set; }
        public QuestionOption? QuestionOption { get; set; }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Entities/UserCourse.cs
================================================
﻿namespace StudentsManager.Mvc.Domain.Entities
{
    public class UserCourse
    {
        public int Id { get; set; }

        public Guid CourseId { get; set; }
        public Course? Course { get; set; }

        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Entities/UserCoursework.cs
================================================
﻿namespace StudentsManager.Mvc.Domain.Entities
{
    public class UserCoursework
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public int Points { get; set; }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Entities/UserTopicResult.cs
================================================
﻿using System.ComponentModel.DataAnnotations;

namespace StudentsManager.Mvc.Domain.Entities
{
    public class UserTopicResult
    {
        public int Id { get; set; }

        [Range(0, 10)] 
        public int Score { get; set; }

        public Guid UserId { get; set; }
        public User? User { get; set; }

        public Guid TopicId { get; set; }
        public Topic? Topic { get; set; }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Entities/_Base/File.cs
================================================
﻿namespace StudentsManager.Mvc.Domain.Entities._Base
{
    public abstract class File
    {
        public string? FileName { get; set; }

        public DateTimeOffset CreatedAtUtc { get; set; }

        public string? Path { get; set; }

        public string? Extension { get; set; }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Entities/_Base/IAuditInfo.cs
================================================
﻿namespace StudentsManager.Mvc.Domain.Entities._Base
{
    public interface IAuditInfo
    {
        DateTime CreatedOn { get; set; }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Entities/_Base/IDeletableEntity.cs
================================================
﻿namespace StudentsManager.Mvc.Domain.Entities._Base
{
    public interface IDeletableEntity
    {
        bool IsDeleted { get; set; }

        DateTime? DeletedOn { get; set; }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Inputs/ChosenOption.cs
================================================
﻿namespace StudentsManager.Mvc.Domain.Inputs
{
    public class ChosenOption
    {
        public int ChosenOptionId { get; set; }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Inputs/EventInput.cs
================================================
﻿namespace StudentsManager.Mvc.Domain.Inputs
{
    public class EventInput
    {
        public string UserId { get; set; }
        public string Type { get; set; }
        public string Data { get; set; }
    }
}



================================================
FILE: StudentsManager.Mvc/Domain/Inputs/TestAnswers.cs
================================================
﻿using Newtonsoft.Json;

namespace StudentsManager.Mvc.Domain.Inputs
{
    public class TestAnswers
    {
        [JsonProperty("1")] public int First { get; set; }

        [JsonProperty("2")] public int Second { get; set; }

        [JsonProperty("3")] public int Third { get; set; }

        [JsonProperty("4")] public int Fourth { get; set; }

        [JsonProperty("5")] public int Fifth { get; set; }

        [JsonProperty("6")] public int Sixth { get; set; }

        [JsonProperty("7")] public int Seventh { get; set; }

        [JsonProperty("8")] public int Eighth { get; set; }

        [JsonProperty("9")] public int Ninth { get; set; }

        [JsonProperty("10")] public int Tenth { get; set; }

        [JsonProperty("11")] public int Eleventh { get; set; }

        [JsonProperty("12")] public int Twelfth { get; set; }

        [JsonProperty("13")] public int Thirteenth { get; set; }

        [JsonProperty("14")] public int Fourteenth { get; set; }

        [JsonProperty("15")] public int Fifteenth { get; set; }

        [JsonProperty("16")] public int Sixteenth { get; set; }

        [JsonProperty("17")] public int Seventeenth { get; set; }

        [JsonProperty("18")] public int Eighteenth { get; set; }

        [JsonProperty("19")] public int Nineteenth { get; set; }

        [JsonProperty("20")] public int Twentieth { get; set; }

        [JsonProperty("21")] public int TwentyFirst { get; set; }

        [JsonProperty("22")] public int TwentySecond { get; set; }

        [JsonProperty("23")] public int TwentyThird { get; set; }

        [JsonProperty("24")] public int TwentyFourth { get; set; }

        [JsonProperty("25")] public int TwentyFifth { get; set; }

        [JsonProperty("26")] public int TwentySixth { get; set; }

        [JsonProperty("27")] public int TwentySeventh { get; set; }

        [JsonProperty("28")] public int TwentyEighth { get; set; }

        [JsonProperty("29")] public int TwentyNinth { get; set; }

        [JsonProperty("30")] public int Thirtieth { get; set; }

        [JsonProperty("31")] public int ThirtyFirst { get; set; }

        [JsonProperty("32")] public int ThirtySecond { get; set; }

        [JsonProperty("33")] public int ThirtyThird { get; set; }

        [JsonProperty("34")] public int ThirtyFourth { get; set; }

        [JsonProperty("35")] public int ThirtyFifth { get; set; }

        [JsonProperty("36")] public int ThirtySixth { get; set; }

        [JsonProperty("37")] public int ThirtySeventh { get; set; }

        [JsonProperty("38")] public int ThirtyEighth { get; set; }

        [JsonProperty("39")] public int ThirtyNinth { get; set; }

        [JsonProperty("40")] public int Question40 { get; set; }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Inputs/TestBindingModel.cs
================================================
﻿using Newtonsoft.Json;

namespace StudentsManager.Mvc.Domain.Inputs
{
    public class TestBindingModel
    {
        [JsonProperty("testStats")] public TestStats TestStats { get; set; }

        [JsonProperty("testAnswers")] public TestAnswers TestAnswers { get; set; }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Inputs/TestStats.cs
================================================
﻿namespace StudentsManager.Mvc.Domain.Inputs
{
    public class TestStats
    {
        public int Action { get; set; }

        public int Process { get; set; }

        public int People { get; set; }

        public int Idea { get; set; }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Inputs/Auth/Credentials.cs
================================================
﻿using System.ComponentModel.DataAnnotations;

namespace StudentsManager.Mvc.Domain.Inputs.Auth
{
    public class Credentials
    {
        [Required] [EmailAddress] public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Inputs/Auth/Register.cs
================================================
﻿using System.ComponentModel.DataAnnotations;

namespace StudentsManager.Mvc.Domain.Inputs.Auth
{
    public class Register
    {
        [Required] [EmailAddress] public string Email { get; set; }

        [Required] public string FullName { get; set; }

        [Required] public string FacultyNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Inputs/Auth/ResetPassword.cs
================================================
﻿using System.ComponentModel.DataAnnotations;

namespace StudentsManager.Mvc.Domain.Inputs.Auth;

public class ResetPassword
{
    [Required]
    [EmailAddress] 
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
}


================================================
FILE: StudentsManager.Mvc/Domain/Inputs/Click/ClickResultsRootObject.cs
================================================
﻿namespace StudentsManager.Mvc.Domain.Inputs.Click
{
    public class ClickResultsRootObject
    {
        public string Res { get; set; }

        public string Form { get; set; }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Inputs/Click/SaveResultsModel.cs
================================================
﻿namespace StudentsManager.Mvc.Domain.Inputs.Click
{
    public class SaveResultsModel
    {
        public SaveResultsModel()
        {
        }

        public SaveResultsModel(bool status)
        {
            Status = status;
            Errors = Array.Empty<string>();
        }

        public bool Status { get; set; }

        public string[] Errors { get; set; }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Inputs/Click/UserAnswer.cs
================================================
﻿namespace StudentsManager.Mvc.Domain.Inputs.Click
{
    public class UserAnswer
    {
        public string Question { get; set; }

        public object Answer { get; set; }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Inputs/Forum/Comment.cs
================================================
﻿namespace StudentsManager.Mvc.Domain.Inputs.Forum
{
    public class Comment
    {
        public string Description { get; set; }

        public int ForumQuestionId { get; set; }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Inputs/Messaging/MailRequest.cs
================================================
﻿namespace StudentsManager.Mvc.Domain.Inputs.Messaging
{
    public class MailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Inputs/Students/StudentExaminationScore.cs
================================================
﻿namespace StudentsManager.Mvc.Domain.Inputs.Students
{
    public class StudentExaminationScore
    {
        public Guid UserId { get; set; }

        public string Type { get; set; }

        public int Score { get; set; }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Inputs/Students/UpdatePicture.cs
================================================
﻿namespace StudentsManager.Mvc.Domain.Inputs.Students
{
    public class UpdatePicture
    {
        public string FacultyNumber { get; set; }
        public string Password { get; set; }
        public string? Picture { get; set; }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Views/Error.cs
================================================
﻿namespace StudentsManager.Mvc.Domain.Views
{
    public class Error
    {
        public Error(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Views/QuestionOptionView.cs
================================================
﻿namespace StudentsManager.Mvc.Domain.Views
{
    public class QuestionOptionView
    {
        /// <summary>
        ///     Question Option ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Question Option Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Test Question Number
        /// </summary>
        public int QuestionNumber { get; set; }

        /// <summary>
        ///     Test Result Value
        /// </summary>
        public string ResultValue { get; set; }

        /// <summary>
        ///     Is Text Question Option
        /// </summary>
        public bool IsTextOnly { get; set; }

        /// <summary>
        ///     Option Image Binary Data (Base64 string)
        /// </summary>
        /// <remarks>
        ///     If <see cref="IsTextOnly" /> is equal to true,
        ///     this must be NULL.
        /// </remarks>
        public string? Base64Image { get; set; }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Views/TestQuestionView.cs
================================================
﻿namespace StudentsManager.Mvc.Domain.Views
{
    public class TestQuestionView
    {
        /// <summary>
        ///     Test Question ID.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     Test Question Description.
        /// </summary>
        public string Description { get; set; }


        /// <summary>
        ///     Test Question Options.
        /// </summary>
        public List<QuestionOptionView> Options { get; set; }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Views/TestViewModel.cs
================================================
﻿namespace StudentsManager.Mvc.Domain.Views
{
    public class TestViewModel
    {
        public TestViewModel(IReadOnlyCollection<TestQuestionView> testQuestions)
        {
            ActiveQuestion = testQuestions.First();
            FollowQuestions = testQuestions.Skip(1).ToList();
        }

        public TestQuestionView ActiveQuestion { get; }

        public List<TestQuestionView> FollowQuestions { get; }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Views/UserTopicResultView.cs
================================================
﻿namespace StudentsManager.Mvc.Domain.Views
{
    public class UserTopicResultView
    {
        public Guid UserId { get; set; }

        public Guid TopicId { get; set; }

        public string TopicDescription { get; set; }

        public bool Passed { get; set; }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Views/Cognitive/UserAnswerReportView.cs
================================================
﻿namespace StudentsManager.Mvc.Domain.Views.Cognitive
{
    public class UserAnswerReportView
    {
        public string Report { get; set; }

        public string UserMail { get; set; }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Views/Home/CourseExaminationSettingsView.cs
================================================
﻿namespace StudentsManager.Mvc.Domain.Views.Home
{
    public readonly struct CourseExaminationSettingsView
    {
        public CourseExaminationSettingsView(bool firstAvailable, bool secondAvailable)
        {
            FirstAvailable = firstAvailable;
            SecondAvailable = secondAvailable;
        }

        public bool FirstAvailable { get; }
        public bool SecondAvailable { get; }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Views/Homeworks/HomeworkView.cs
================================================
﻿namespace StudentsManager.Mvc.Domain.Views.Homeworks
{
    public class HomeworkView
    {
        public Guid TopicId { get; set; }
        public string? TopicDescription { get; set; }
        public string? VideoLinkFromPreviousYear { get; set; }
        public string? TopicTag { get; set; }
        public string? ExerciseFileUrl { get; set; }
        public string? ResourcesUrl { get; set; }
        public bool HasHanded { get; set; }
        public string? HomeWorkPath { get; set; }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Views/Statistics/StudentGrade.cs
================================================
﻿namespace StudentsManager.Mvc.Domain.Views.Statistics
{
    public class StudentGrade
    {
        public Guid StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentFacultyNumber { get; set; }

        public float IntroScore { get; set; }
        public string? IntroHomework { get; set; }
        public int IntroHomeworkScore => string.IsNullOrEmpty(IntroHomework) ? 0 : 1;


        public float AdvanceScore { get; set; }
        public string? AdvanceHomework { get; set; }
        public int AdvanceHomeworkScore => string.IsNullOrEmpty(AdvanceHomework) ? 0 : 1;

        public float AsyncScore { get; set; }
        public string? AsyncHomework { get; set; }
        public int AsyncHomeworkScore => string.IsNullOrEmpty(AsyncHomework) ? 0 : 1;

        public float CordovaScore { get; set; }
        public string? CordovaHomework { get; set; }
        public int CordovaHomeworkScore => string.IsNullOrEmpty(CordovaHomework) ? 0 : 1;

        public float ReactScore { get; set; }
        public string? ReactHomework { get; set; }
        public int ReactHomeworkScore => string.IsNullOrEmpty(ReactHomework) ? 0 : 1;

        public float NativeScore { get; set; }
        public string? NativeHomework { get; set; }
        public int NativeHomeworkScore => string.IsNullOrEmpty(NativeHomework) ? 0 : 1;

        public float ArchitectureScore { get; set; }
        public string? ArchitectureHomework { get; set; }
        public int ArchitectureHomeworkScore => string.IsNullOrEmpty(ArchitectureHomework) ? 0 : 1;


        public float FirstExaminationScore { get; set; }
        public float SecondExaminationScore { get; set; }

        public bool HasExaminationAnswer { get; set; }
        public bool HasCoursework { get; set; }
        public float CourseworkScore { get; set; }

        public float Total
        {
            get
            {
                var result = IntroScore + IntroHomeworkScore +
                             AdvanceScore + AdvanceHomeworkScore +
                             AsyncScore + AsyncHomeworkScore +
                             ArchitectureScore + ArchitectureHomeworkScore +
                             CordovaScore + CordovaHomeworkScore +
                             NativeScore + NativeHomeworkScore +
                             ReactScore + ReactHomeworkScore +
                             FirstExaminationScore + SecondExaminationScore;

                if (HasExaminationAnswer)
                {
                    result += 10;
                }

                if (HasCoursework)
                {
                    result += CourseworkScore;
                }

                return result;
            }
        }
    }
}


================================================
FILE: StudentsManager.Mvc/Domain/Views/Students/StudentView.cs
================================================
﻿namespace StudentsManager.Mvc.Domain.Views.Students
{
    public readonly record struct StudentView(string? UserName, string? Email, string FullName, string FacultyNumber, string? Base64EncodePicture);
}


================================================
FILE: StudentsManager.Mvc/Mappings/AuthMapping.cs
================================================
﻿using System.Net.Mail;
using StudentsManager.Mvc.Domain.Entities;
using StudentsManager.Mvc.Domain.Inputs.Auth;

namespace StudentsManager.Mvc.Mappings
{
    public static class AuthMapping
    {
        public static User ToEntity(this Register model)
        {
            var rnd = new Random();
            return new User
            {
                Email = model.Email,
                UserName = $"{new MailAddress(model.Email).User}{rnd.Next(0, 100)}",
                FullName = model.FullName,
                FacultyNumber = model.FacultyNumber,
                EmailConfirmed = true,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };
        }
    }
}


================================================
FILE: StudentsManager.Mvc/Mappings/QuestionOptionMapping.cs
================================================
﻿using StudentsManager.Mvc.Domain.Entities;
using StudentsManager.Mvc.Domain.Views;

namespace StudentsManager.Mvc.Mappings
{
    public static class QuestionOptionMapping
    {
        public static QuestionOptionView ToView(this QuestionOption entity)
        {
            return new QuestionOptionView
            {
                Id = entity.Id,
                Description = entity.Description,
                IsTextOnly = entity.IsTextOnly,
                QuestionNumber = entity.Id,
                ResultValue = entity.Description,
                Base64Image = entity.ImageAddress
            };
        }
    }
}


================================================
FILE: StudentsManager.Mvc/Mappings/TestQuestionMapping.cs
================================================
﻿using StudentsManager.Mvc.Domain.Entities;
using StudentsManager.Mvc.Domain.Views;

namespace StudentsManager.Mvc.Mappings
{
    public static class TestQuestionMapping
    {
        public static TestQuestionView ToView(this TestQuestion entity)
        {
            return new TestQuestionView
            {
                Id = entity.Id,
                Description = entity.Description,
                Options = entity
                    .Options
                    .Select(option => option.ToView())
                    .ToList()
            };
        }
    }
}


================================================
FILE: StudentsManager.Mvc/Mappings/UserTopicMapping.cs
================================================
﻿using StudentsManager.Mvc.Domain.Entities;
using StudentsManager.Mvc.Domain.Views;

namespace StudentsManager.Mvc.Mappings
{
    public static class UserTopicMapping
    {
        public static UserTopicResultView ToUserResult(this Topic entity, Guid userId, bool passed)
        {
            return new UserTopicResultView
            {
                TopicId = entity.Id,
                UserId = userId,
                TopicDescription = entity.Description,
                Passed = passed
            };
        }
    }
}


================================================
FILE: StudentsManager.Mvc/Migrations/20210624120313_InitialCreate.cs
================================================
﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentsManager.Mvc.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacultyNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Base64EncodePicture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseExaminations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResourceUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraResourceUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseExaminations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseExaminationSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Enabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseExaminationSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScreenSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Enabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreenSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SequenceNumber = table.Column<int>(type: "int", nullable: false),
                    ExerciseFileUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResourcesUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CanBeSeenAfter = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentCourseExaminations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseExaminationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCourseExaminations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentCourseExaminations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentCourseExaminations_CourseExaminations_CourseExaminationId",
                        column: x => x.CourseExaminationId,
                        principalTable: "CourseExaminations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Homeworks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RepositoryLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TopicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Homeworks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Homeworks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Homeworks_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestQuestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TopicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestQuestions_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTopicResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Score = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TopicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTopicResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTopicResults_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTopicResults_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentExaminationUploads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentCourseExaminationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAtUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentExaminationUploads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentExaminationUploads_StudentCourseExaminations_StudentCourseExaminationId",
                        column: x => x.StudentCourseExaminationId,
                        principalTable: "StudentCourseExaminations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsTextOnly = table.Column<bool>(type: "bit", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    ImageAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionOptions_TestQuestions_TestQuestionId",
                        column: x => x.TestQuestionId,
                        principalTable: "TestQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionOptionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAnswers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAnswers_QuestionOptions_QuestionOptionId",
                        column: x => x.QuestionOptionId,
                        principalTable: "QuestionOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Homeworks_TopicId",
                table: "Homeworks",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Homeworks_UserId",
                table: "Homeworks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionOptions_TestQuestionId",
                table: "QuestionOptions",
                column: "TestQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourseExaminations_CourseExaminationId",
                table: "StudentCourseExaminations",
                column: "CourseExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCourseExaminations_UserId",
                table: "StudentCourseExaminations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentExaminationUploads_StudentCourseExaminationId",
                table: "StudentExaminationUploads",
                column: "StudentCourseExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_TestQuestions_TopicId",
                table: "TestQuestions",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_QuestionOptionId",
                table: "UserAnswers",
                column: "QuestionOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_UserId",
                table: "UserAnswers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTopicResults_TopicId",
                table: "UserTopicResults",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTopicResults_UserId",
                table: "UserTopicResults",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CourseExaminationSettings");

            migrationBuilder.DropTable(
                name: "Homeworks");

            migrationBuilder.DropTable(
                name: "ScreenSettings");

            migrationBuilder.DropTable(
                name: "StudentExaminationUploads");

            migrationBuilder.DropTable(
                name: "UserAnswers");

            migrationBuilder.DropTable(
                name: "UserTopicResults");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "StudentCourseExaminations");

            migrationBuilder.DropTable(
                name: "QuestionOptions");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "CourseExaminations");

            migrationBuilder.DropTable(
                name: "TestQuestions");

            migrationBuilder.DropTable(
                name: "Topics");
        }
    }
}



================================================
FILE: StudentsManager.Mvc/Migrations/20210624120313_InitialCreate.Designer.cs
================================================
﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentsManager.Mvc.Persistence;

namespace StudentsManager.Mvc.Migrations
{
    [DbContext(typeof(ManagerDbContext))]
    [Migration("20210624120313_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.CourseExamination", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ExtraResourceUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResourceUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CourseExaminations");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.CourseExaminationSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CourseExaminationSettings");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.Homework", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTimeOffset>("CreatedAtUtc")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Extension")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RepositoryLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TopicId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.HasIndex("UserId");

                    b.ToTable("Homeworks");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.QuestionOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTextOnly")
                        .HasColumnType("bit");

                    b.Property<Guid>("TestQuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TestQuestionId");

                    b.ToTable("QuestionOptions");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.ScreenSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ScreenSettings");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.StudentCourseExamination", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CourseExaminationId")
                        .HasColumnType("int");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CourseExaminationId");

                    b.HasIndex("UserId");

                    b.ToTable("StudentCourseExaminations");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.StudentCourseExaminationUpload", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTimeOffset>("CreatedAtUtc")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Extension")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StudentCourseExaminationId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StudentCourseExaminationId");

                    b.ToTable("StudentExaminationUploads");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.TestQuestion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TopicId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.ToTable("TestQuestions");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.Topic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CanBeSeenAfter")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExerciseFileUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResourcesUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SequenceNumber")
                        .HasColumnType("int");

                    b.Property<string>("Tag")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Base64EncodePicture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FacultyNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.UserAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("QuestionOptionId")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("QuestionOptionId");

                    b.HasIndex("UserId");

                    b.ToTable("UserAnswers");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.UserTopicResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<Guid>("TopicId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.HasIndex("UserId");

                    b.ToTable("UserTopicResults");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.Homework", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.Topic", "Topic")
                        .WithMany("Homeworks")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", "User")
                        .WithMany("Homeworks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Topic");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.QuestionOption", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.TestQuestion", "TestQuestion")
                        .WithMany("Options")
                        .HasForeignKey("TestQuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TestQuestion");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.StudentCourseExamination", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.CourseExamination", "CourseExamination")
                        .WithMany("StudentExaminations")
                        .HasForeignKey("CourseExaminationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", "User")
                        .WithMany("StudentExaminations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CourseExamination");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.StudentCourseExaminationUpload", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.StudentCourseExamination", "StudentCourseExamination")
                        .WithMany("Uploads")
                        .HasForeignKey("StudentCourseExaminationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StudentCourseExamination");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.TestQuestion", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.Topic", "Topic")
                        .WithMany("TestQuestions")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Topic");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.UserAnswer", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.QuestionOption", "QuestionOption")
                        .WithMany("UserAnswers")
                        .HasForeignKey("QuestionOptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", "User")
                        .WithMany("UserAnswers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("QuestionOption");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.UserTopicResult", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.Topic", "Topic")
                        .WithMany("UserTopics")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", "User")
                        .WithMany("UserTopicResults")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Topic");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.CourseExamination", b =>
                {
                    b.Navigation("StudentExaminations");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.QuestionOption", b =>
                {
                    b.Navigation("UserAnswers");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.StudentCourseExamination", b =>
                {
                    b.Navigation("Uploads");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.TestQuestion", b =>
                {
                    b.Navigation("Options");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.Topic", b =>
                {
                    b.Navigation("Homeworks");

                    b.Navigation("TestQuestions");

                    b.Navigation("UserTopics");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.User", b =>
                {
                    b.Navigation("Homeworks");

                    b.Navigation("StudentExaminations");

                    b.Navigation("UserAnswers");

                    b.Navigation("UserTopicResults");
                });
#pragma warning restore 612, 618
        }
    }
}



================================================
FILE: StudentsManager.Mvc/Migrations/20210624123249_ExtendUser.cs
================================================
﻿using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace StudentsManager.Mvc.Migrations
{
    public partial class ExtendUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");
        }
    }
}



================================================
FILE: StudentsManager.Mvc/Migrations/20210624123249_ExtendUser.Designer.cs
================================================
﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentsManager.Mvc.Persistence;

namespace StudentsManager.Mvc.Migrations
{
    [DbContext(typeof(ManagerDbContext))]
    [Migration("20210624123249_ExtendUser")]
    partial class ExtendUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.CourseExamination", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ExtraResourceUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResourceUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CourseExaminations");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.CourseExaminationSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CourseExaminationSettings");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.Homework", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTimeOffset>("CreatedAtUtc")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Extension")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RepositoryLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TopicId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.HasIndex("UserId");

                    b.ToTable("Homeworks");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.QuestionOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTextOnly")
                        .HasColumnType("bit");

                    b.Property<Guid>("TestQuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TestQuestionId");

                    b.ToTable("QuestionOptions");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.ScreenSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ScreenSettings");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.StudentCourseExamination", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CourseExaminationId")
                        .HasColumnType("int");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CourseExaminationId");

                    b.HasIndex("UserId");

                    b.ToTable("StudentCourseExaminations");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.StudentCourseExaminationUpload", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTimeOffset>("CreatedAtUtc")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Extension")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StudentCourseExaminationId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StudentCourseExaminationId");

                    b.ToTable("StudentExaminationUploads");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.TestQuestion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TopicId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.ToTable("TestQuestions");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.Topic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CanBeSeenAfter")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExerciseFileUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResourcesUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SequenceNumber")
                        .HasColumnType("int");

                    b.Property<string>("Tag")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Base64EncodePicture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FacultyNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.UserAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("QuestionOptionId")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("QuestionOptionId");

                    b.HasIndex("UserId");

                    b.ToTable("UserAnswers");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.UserTopicResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<Guid>("TopicId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.HasIndex("UserId");

                    b.ToTable("UserTopicResults");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.Homework", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.Topic", "Topic")
                        .WithMany("Homeworks")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", "User")
                        .WithMany("Homeworks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Topic");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.QuestionOption", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.TestQuestion", "TestQuestion")
                        .WithMany("Options")
                        .HasForeignKey("TestQuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TestQuestion");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.StudentCourseExamination", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.CourseExamination", "CourseExamination")
                        .WithMany("StudentExaminations")
                        .HasForeignKey("CourseExaminationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", "User")
                        .WithMany("StudentExaminations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CourseExamination");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.StudentCourseExaminationUpload", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.StudentCourseExamination", "StudentCourseExamination")
                        .WithMany("Uploads")
                        .HasForeignKey("StudentCourseExaminationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StudentCourseExamination");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.TestQuestion", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.Topic", "Topic")
                        .WithMany("TestQuestions")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Topic");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.UserAnswer", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.QuestionOption", "QuestionOption")
                        .WithMany("UserAnswers")
                        .HasForeignKey("QuestionOptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", "User")
                        .WithMany("UserAnswers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("QuestionOption");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.UserTopicResult", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.Topic", "Topic")
                        .WithMany("UserTopics")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", "User")
                        .WithMany("UserTopicResults")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Topic");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.CourseExamination", b =>
                {
                    b.Navigation("StudentExaminations");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.QuestionOption", b =>
                {
                    b.Navigation("UserAnswers");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.StudentCourseExamination", b =>
                {
                    b.Navigation("Uploads");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.TestQuestion", b =>
                {
                    b.Navigation("Options");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.Topic", b =>
                {
                    b.Navigation("Homeworks");

                    b.Navigation("TestQuestions");

                    b.Navigation("UserTopics");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.User", b =>
                {
                    b.Navigation("Homeworks");

                    b.Navigation("StudentExaminations");

                    b.Navigation("UserAnswers");

                    b.Navigation("UserTopicResults");
                });
#pragma warning restore 612, 618
        }
    }
}



================================================
FILE: StudentsManager.Mvc/Migrations/20210624125229_AddedExaminationAnswer.cs
================================================
﻿using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace StudentsManager.Mvc.Migrations
{
    public partial class AddedExaminationAnswer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExaminationAnswers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Form = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExaminationAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExaminationAnswers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExaminationAnswers_UserId",
                table: "ExaminationAnswers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExaminationAnswers");
        }
    }
}



================================================
FILE: StudentsManager.Mvc/Migrations/20210624125229_AddedExaminationAnswer.Designer.cs
================================================
﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentsManager.Mvc.Persistence;

namespace StudentsManager.Mvc.Migrations
{
    [DbContext(typeof(ManagerDbContext))]
    [Migration("20210624125229_AddedExaminationAnswer")]
    partial class AddedExaminationAnswer
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.CourseExamination", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ExtraResourceUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResourceUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CourseExaminations");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.CourseExaminationSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CourseExaminationSettings");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.ExaminationAnswer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Form")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Result")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ExaminationAnswers");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.Homework", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTimeOffset>("CreatedAtUtc")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Extension")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RepositoryLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TopicId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.HasIndex("UserId");

                    b.ToTable("Homeworks");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.QuestionOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTextOnly")
                        .HasColumnType("bit");

                    b.Property<Guid>("TestQuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TestQuestionId");

                    b.ToTable("QuestionOptions");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.ScreenSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ScreenSettings");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.StudentCourseExamination", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CourseExaminationId")
                        .HasColumnType("int");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CourseExaminationId");

                    b.HasIndex("UserId");

                    b.ToTable("StudentCourseExaminations");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.StudentCourseExaminationUpload", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTimeOffset>("CreatedAtUtc")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Extension")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StudentCourseExaminationId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StudentCourseExaminationId");

                    b.ToTable("StudentExaminationUploads");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.TestQuestion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TopicId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.ToTable("TestQuestions");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.Topic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CanBeSeenAfter")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExerciseFileUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResourcesUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SequenceNumber")
                        .HasColumnType("int");

                    b.Property<string>("Tag")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Base64EncodePicture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FacultyNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.UserAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("QuestionOptionId")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("QuestionOptionId");

                    b.HasIndex("UserId");

                    b.ToTable("UserAnswers");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.UserTopicResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<Guid>("TopicId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.HasIndex("UserId");

                    b.ToTable("UserTopicResults");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.ExaminationAnswer", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", "User")
                        .WithMany("ExaminationAnswers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.Homework", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.Topic", "Topic")
                        .WithMany("Homeworks")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", "User")
                        .WithMany("Homeworks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Topic");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.QuestionOption", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.TestQuestion", "TestQuestion")
                        .WithMany("Options")
                        .HasForeignKey("TestQuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TestQuestion");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.StudentCourseExamination", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.CourseExamination", "CourseExamination")
                        .WithMany("StudentExaminations")
                        .HasForeignKey("CourseExaminationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", "User")
                        .WithMany("StudentExaminations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CourseExamination");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.StudentCourseExaminationUpload", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.StudentCourseExamination", "StudentCourseExamination")
                        .WithMany("Uploads")
                        .HasForeignKey("StudentCourseExaminationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StudentCourseExamination");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.TestQuestion", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.Topic", "Topic")
                        .WithMany("TestQuestions")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Topic");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.UserAnswer", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.QuestionOption", "QuestionOption")
                        .WithMany("UserAnswers")
                        .HasForeignKey("QuestionOptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", "User")
                        .WithMany("UserAnswers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("QuestionOption");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.UserTopicResult", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.Topic", "Topic")
                        .WithMany("UserTopics")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", "User")
                        .WithMany("UserTopicResults")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Topic");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.CourseExamination", b =>
                {
                    b.Navigation("StudentExaminations");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.QuestionOption", b =>
                {
                    b.Navigation("UserAnswers");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.StudentCourseExamination", b =>
                {
                    b.Navigation("Uploads");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.TestQuestion", b =>
                {
                    b.Navigation("Options");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.Topic", b =>
                {
                    b.Navigation("Homeworks");

                    b.Navigation("TestQuestions");

                    b.Navigation("UserTopics");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.User", b =>
                {
                    b.Navigation("ExaminationAnswers");

                    b.Navigation("Homeworks");

                    b.Navigation("StudentExaminations");

                    b.Navigation("UserAnswers");

                    b.Navigation("UserTopicResults");
                });
#pragma warning restore 612, 618
        }
    }
}



================================================
FILE: StudentsManager.Mvc/Migrations/20210625120122_UserAuditable.cs
================================================
﻿using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace StudentsManager.Mvc.Migrations
{
    public partial class UserAuditable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "AspNetUsers");
        }
    }
}



================================================
FILE: StudentsManager.Mvc/Migrations/20210625120122_UserAuditable.Designer.cs
================================================
﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentsManager.Mvc.Persistence;

namespace StudentsManager.Mvc.Migrations
{
    [DbContext(typeof(ManagerDbContext))]
    [Migration("20210625120122_UserAuditable")]
    partial class UserAuditable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.CourseExamination", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ExtraResourceUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResourceUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CourseExaminations");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.CourseExaminationSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CourseExaminationSettings");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.ExaminationAnswer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Form")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Result")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ExaminationAnswers");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.Homework", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTimeOffset>("CreatedAtUtc")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Extension")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RepositoryLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TopicId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.HasIndex("UserId");

                    b.ToTable("Homeworks");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.QuestionOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTextOnly")
                        .HasColumnType("bit");

                    b.Property<Guid>("TestQuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TestQuestionId");

                    b.ToTable("QuestionOptions");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.ScreenSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ScreenSettings");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.StudentCourseExamination", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CourseExaminationId")
                        .HasColumnType("int");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CourseExaminationId");

                    b.HasIndex("UserId");

                    b.ToTable("StudentCourseExaminations");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.StudentCourseExaminationUpload", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTimeOffset>("CreatedAtUtc")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Extension")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StudentCourseExaminationId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StudentCourseExaminationId");

                    b.ToTable("StudentExaminationUploads");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.TestQuestion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TopicId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.ToTable("TestQuestions");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.Topic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CanBeSeenAfter")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExerciseFileUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResourcesUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SequenceNumber")
                        .HasColumnType("int");

                    b.Property<string>("Tag")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Base64EncodePicture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FacultyNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.UserAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("QuestionOptionId")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("QuestionOptionId");

                    b.HasIndex("UserId");

                    b.ToTable("UserAnswers");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.UserTopicResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<Guid>("TopicId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.HasIndex("UserId");

                    b.ToTable("UserTopicResults");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.ExaminationAnswer", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", "User")
                        .WithMany("ExaminationAnswers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.Homework", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.Topic", "Topic")
                        .WithMany("Homeworks")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", "User")
                        .WithMany("Homeworks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Topic");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.QuestionOption", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.TestQuestion", "TestQuestion")
                        .WithMany("Options")
                        .HasForeignKey("TestQuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TestQuestion");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.StudentCourseExamination", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.CourseExamination", "CourseExamination")
                        .WithMany("StudentExaminations")
                        .HasForeignKey("CourseExaminationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", "User")
                        .WithMany("StudentExaminations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CourseExamination");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.StudentCourseExaminationUpload", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.StudentCourseExamination", "StudentCourseExamination")
                        .WithMany("Uploads")
                        .HasForeignKey("StudentCourseExaminationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StudentCourseExamination");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.TestQuestion", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.Topic", "Topic")
                        .WithMany("TestQuestions")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Topic");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.UserAnswer", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.QuestionOption", "QuestionOption")
                        .WithMany("UserAnswers")
                        .HasForeignKey("QuestionOptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", "User")
                        .WithMany("UserAnswers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("QuestionOption");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.UserTopicResult", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.Topic", "Topic")
                        .WithMany("UserTopics")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", "User")
                        .WithMany("UserTopicResults")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Topic");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.CourseExamination", b =>
                {
                    b.Navigation("StudentExaminations");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.QuestionOption", b =>
                {
                    b.Navigation("UserAnswers");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.StudentCourseExamination", b =>
                {
                    b.Navigation("Uploads");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.TestQuestion", b =>
                {
                    b.Navigation("Options");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.Topic", b =>
                {
                    b.Navigation("Homeworks");

                    b.Navigation("TestQuestions");

                    b.Navigation("UserTopics");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.User", b =>
                {
                    b.Navigation("ExaminationAnswers");

                    b.Navigation("Homeworks");

                    b.Navigation("StudentExaminations");

                    b.Navigation("UserAnswers");

                    b.Navigation("UserTopicResults");
                });
#pragma warning restore 612, 618
        }
    }
}



================================================
FILE: StudentsManager.Mvc/Migrations/20210625123648_AddCourse.cs
================================================
﻿using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace StudentsManager.Mvc.Migrations
{
    public partial class AddCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CourseId",
                table: "Topics",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserCourses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCourses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Topics_CourseId",
                table: "Topics",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCourses_CourseId",
                table: "UserCourses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCourses_UserId",
                table: "UserCourses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_Courses_CourseId",
                table: "Topics",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Topics_Courses_CourseId",
                table: "Topics");

            migrationBuilder.DropTable(
                name: "UserCourses");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Topics_CourseId",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Topics");
        }
    }
}



================================================
FILE: StudentsManager.Mvc/Migrations/20210625123648_AddCourse.Designer.cs
================================================
﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentsManager.Mvc.Persistence;

namespace StudentsManager.Mvc.Migrations
{
    [DbContext(typeof(ManagerDbContext))]
    [Migration("20210625123648_AddCourse")]
    partial class AddCourse
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.CourseExamination", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ExtraResourceUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResourceUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CourseExaminations");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.CourseExaminationSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CourseExaminationSettings");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.ExaminationAnswer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Form")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Result")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ExaminationAnswers");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.Homework", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTimeOffset>("CreatedAtUtc")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Extension")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RepositoryLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TopicId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.HasIndex("UserId");

                    b.ToTable("Homeworks");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.QuestionOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTextOnly")
                        .HasColumnType("bit");

                    b.Property<Guid>("TestQuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TestQuestionId");

                    b.ToTable("QuestionOptions");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.ScreenSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ScreenSettings");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.StudentCourseExamination", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CourseExaminationId")
                        .HasColumnType("int");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CourseExaminationId");

                    b.HasIndex("UserId");

                    b.ToTable("StudentCourseExaminations");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.StudentCourseExaminationUpload", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTimeOffset>("CreatedAtUtc")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Extension")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StudentCourseExaminationId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StudentCourseExaminationId");

                    b.ToTable("StudentExaminationUploads");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.TestQuestion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TopicId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.ToTable("TestQuestions");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.Topic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CanBeSeenAfter")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("CourseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExerciseFileUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResourcesUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SequenceNumber")
                        .HasColumnType("int");

                    b.Property<string>("Tag")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Base64EncodePicture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FacultyNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.UserAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("QuestionOptionId")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("QuestionOptionId");

                    b.HasIndex("UserId");

                    b.ToTable("UserAnswers");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.UserCourse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("UserId");

                    b.ToTable("UserCourses");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.UserTopicResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<Guid>("TopicId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.HasIndex("UserId");

                    b.ToTable("UserTopicResults");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.ExaminationAnswer", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", "User")
                        .WithMany("ExaminationAnswers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.Homework", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.Topic", "Topic")
                        .WithMany("Homeworks")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", "User")
                        .WithMany("Homeworks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Topic");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.QuestionOption", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.TestQuestion", "TestQuestion")
                        .WithMany("Options")
                        .HasForeignKey("TestQuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TestQuestion");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.StudentCourseExamination", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.CourseExamination", "CourseExamination")
                        .WithMany("StudentExaminations")
                        .HasForeignKey("CourseExaminationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", "User")
                        .WithMany("StudentExaminations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CourseExamination");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.StudentCourseExaminationUpload", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.StudentCourseExamination", "StudentCourseExamination")
                        .WithMany("Uploads")
                        .HasForeignKey("StudentCourseExaminationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StudentCourseExamination");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.TestQuestion", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.Topic", "Topic")
                        .WithMany("TestQuestions")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Topic");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.Topic", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.Course", "Course")
                        .WithMany("Topics")
                        .HasForeignKey("CourseId");

                    b.Navigation("Course");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.UserAnswer", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.QuestionOption", "QuestionOption")
                        .WithMany("UserAnswers")
                        .HasForeignKey("QuestionOptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", "User")
                        .WithMany("UserAnswers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("QuestionOption");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.UserCourse", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.Course", "Course")
                        .WithMany("UserCourses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", "User")
                        .WithMany("UserCourses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.UserTopicResult", b =>
                {
                    b.HasOne("StudentsManager.Mvc.Domain.Entities.Topic", "Topic")
                        .WithMany("UserTopics")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StudentsManager.Mvc.Domain.Entities.User", "User")
                        .WithMany("UserTopicResults")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Topic");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.Course", b =>
                {
                    b.Navigation("Topics");

                    b.Navigation("UserCourses");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.CourseExamination", b =>
                {
                    b.Navigation("StudentExaminations");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.QuestionOption", b =>
                {
                    b.Navigation("UserAnswers");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.StudentCourseExamination", b =>
                {
                    b.Navigation("Uploads");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.TestQuestion", b =>
                {
                    b.Navigation("Options");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.Topic", b =>
                {
                    b.Navigation("Homeworks");

                    b.Navigation("TestQuestions");

                    b.Navigation("UserTopics");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.User", b =>
                {
                    b.Navigation("ExaminationAnswers");

                    b.Navigation("Homeworks");

                    b.Navigation("StudentExaminations");

                    b.Navigation("UserAnswers");

                    b.Navigation("UserCourses");

                    b.Navigation("UserTopicResults");
                });
#pragma warning restore 612, 618
        }
    }
}



================================================
FILE: StudentsManager.Mvc/Migrations/20210625125413_UpdateTopics.cs
================================================
﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentsManager.Mvc.Migrations
{
    public partial class UpdateTopics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE [dbo].[Topics] SET [CourseId] = '788f26d7-b885-4568-99d5-ef83e4e88ef4'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE [dbo].[Topics] SET [CourseId] = NULL");
        }
    }
}



================================================
FILE: StudentsManager.Mvc/Migrations/20210625125413_UpdateTopics.Designer.cs
================================================
﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentsManager.Mvc.Persistence;

namespace StudentsManager.Mvc.Migrations
{
    [DbContext(typeof(ManagerDbContext))]
    [Migration("20210625125413_UpdateTopics")]
    partial class UpdateTopics
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.CourseExamination", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ExtraResourceUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResourceUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CourseExaminations");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.CourseExaminationSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CourseExaminationSettings");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.ExaminationAnswer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Form")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Result")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ExaminationAnswers");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.Homework", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTimeOffset>("CreatedAtUtc")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Extension")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RepositoryLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TopicId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.HasIndex("UserId");

                    b.ToTable("Homeworks");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.QuestionOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTextOnly")
                        .HasColumnType("bit");

                    b.Property<Guid>("TestQuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TestQuestionId");

                    b.ToTable("QuestionOptions");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.ScreenSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ScreenSettings");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.StudentCourseExamination", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CourseExaminationId")
                        .HasColumnType("int");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CourseExaminationId");

                    b.HasIndex("UserId");

                    b.ToTable("StudentCourseExaminations");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.StudentCourseExaminationUpload", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTimeOffset>("CreatedAtUtc")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Extension")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StudentCourseExaminationId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("StudentCourseExaminationId");

                    b.ToTable("StudentExaminationUploads");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.TestQuestion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TopicId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.ToTable("TestQuestions");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.Topic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CanBeSeenAfter")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("CourseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExerciseFileUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ResourcesUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SequenceNumber")
                        .HasColumnType("int");

                    b.Property<string>("Tag")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Base64EncodePicture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FacultyNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("StudentsManager.Mvc.Domain.Entities.UserAnswer", b =>
                {
                    b.Property<int>("Id
