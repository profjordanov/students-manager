# StudentsManager

StudentsManager is a teaching platform for managing course activity at the University of Economics - Varna. It combines an ASP.NET Core MVC application, a React single-page application, SQL Server persistence, and Azure-backed services for storage, messaging, AI, and text analysis. Students use the platform while extending it as part of their coursework.

[![Docker Compose Build Check](https://github.com/profjordanov/students-manager/actions/workflows/docker-compose.yml/badge.svg)](https://github.com/profjordanov/students-manager/actions/workflows/docker-compose.yml)
[![Deploy Manager to Azure App Service](https://github.com/profjordanov/students-manager/actions/workflows/dotnet-deploy.yml/badge.svg)](https://github.com/profjordanov/students-manager/actions/workflows/dotnet-deploy.yml)
[![CodeQL](https://github.com/profjordanov/students-manager/actions/workflows/github-code-scanning/codeql/badge.svg)](https://github.com/profjordanov/students-manager/actions/workflows/github-code-scanning/codeql)
[![CodeFactor](https://www.codefactor.io/repository/github/profjordanov/students-manager/badge)](https://www.codefactor.io/repository/github/profjordanov/students-manager)
[![SonarQube Cloud](https://sonarcloud.io/images/project_badges/sonarcloud-light.svg)](https://sonarcloud.io/summary/new_code?id=profjordanov_students-manager)

## Environments

| Environment | URL | Purpose |
|---|---|---|
| Server & API | https://students-manager.azurewebsites.net/ | Development deployment and API examples below |
| React SPA | https://students-manager-spa.azurewebsites.net/ | React version of the platform |

## Architecture

- **MVC application:** ASP.NET Core 10 with Razor Pages, MVC controllers, ASP.NET Core Identity, and static assets in `wwwroot`.
- **SPA:** React 19 with Vite and PWA support in `StudentsManager.Spa`.
- **Data:** Entity Framework Core 10 with SQL Server. Application startup applies pending migrations and seeds the database.
- **Integrations:** Azure Blob Storage, Azure Service Bus, Azure AI/OpenAI, and Azure Text Analytics.
- **Tests:** xUnit tests in `StudentsManager.Tests`, runnable directly or in Docker.

## Run Locally

### Full Stack With Docker

This is the most reproducible local setup. It starts SQL Server, Azurite, the MVC application, and the React SPA.

```bash
docker compose -f docker-compose.yml -f docker-compose.override.yml -p studentsmanager up --build -d
```

The SPA is available at http://localhost:3000. The MVC container publishes port 80 dynamically; retrieve its mapped host port with:

```bash
docker compose -f docker-compose.yml -f docker-compose.override.yml -p studentsmanager port studentsmanager.mvc 80
```

Stop the stack with:

```bash
docker compose -f docker-compose.yml -f docker-compose.override.yml -p studentsmanager down
```

The repository scripts provide the same workflows for a POSIX shell:

```bash
./run-app.sh
./run-tests.sh
```

### MVC Application

Install the [.NET 10 SDK](https://dotnet.microsoft.com/download). The application has no checked-in `appsettings` file, so provide configuration through user secrets or environment variables before starting it. At minimum, the startup registrations use a SQL Server connection, Azure Storage, and Azure Service Bus settings.

```bash
cd StudentsManager.Mvc
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "<sql-server-connection-string>"
dotnet user-secrets set "StorageSettings:AzureConnectionString" "<azure-storage-connection-string>"
dotnet user-secrets set "ServiceBusSettings:AzureConnectionString" "<azure-service-bus-connection-string>"
dotnet user-secrets set "ServiceBusSettings:QueueName" "<queue-name>"
dotnet user-secrets set "RoboExaminationSettings:AdminKey" "<private-admin-key>"
dotnet run
```

Add `MailSettings` and `AgentFrameworkAppSettings` when working on the features that use them. Keep all connection strings, keys, and production credentials out of source control.

By default, the project profile listens on `https://localhost:5001` and `http://localhost:5000`. Startup runs Entity Framework migrations and database seeding, so use a disposable or intentionally prepared local database.

The Azure chat integrations intentionally use `Azure.AI.OpenAI` **2.9.0-beta.1** with `OpenAI` **2.10.0**, also required by the Foundry dependencies. The older stable Azure SDK **2.1.0** calls an internal OpenAI API removed in newer versions and fails at runtime with `MissingMethodException` for `get_SerializedAdditionalRawData`. Do not downgrade OpenAI independently of Foundry. Run the SDK compatibility tests below when changing these packages.

After an SDK update, stop the MVC process, run `dotnet clean`, `dotnet restore`, and `dotnet build` against `StudentsManager.Tests/StudentsManager.Tests.csproj` (which also rebuilds MVC), then restart the application. Already-running processes retain their old assemblies.

### React SPA

Install a current Node.js LTS release, then run the Vite development server:

```bash
cd StudentsManager.Spa
npm install
npm run dev
```

Create a production bundle with `npm run build`.

## API

The examples below target the development deployment. Replace placeholder IDs and credentials with values valid in the environment you are calling.

| Area | Routes |
|---|---|
| Authentication | `POST /api/login` |
| Students | `GET /api/students`, `GET /api/students/{facultyNumber}`, `GET /api/students/profile/{studentId}`, `PUT /api/students/picture`, `PATCH /api/students/examination` |
| Forum | `GET /api/slido`, `GET /api/slido/questions`, `POST /api/slido/question`, `POST /api/slido/comment` |
| Events | `GET /api/events/{userId}`, `POST /api/events` |
| RoboQuiz | `GET /api/roboquiz?userId={id}`, `POST /api/roboquiz`, `POST /api/roboquizresults`, `GET /api/roboquizresults/{userId}` |
| RoboExamination | `GET /api/roboexamination?userId={id}`, `POST /api/roboexamination`, `POST /api/roboexaminationresults`, `GET /api/roboexaminationresults/{userId}`, `POST /api/roboexaminationresults/{attemptId}/evaluate` |
| Legacy AI examination answers | `GET /api/chatbot/examination-answers/{studentId}`, `POST /api/chatbot/examination-answers` |
| Course settings | `GET /api/homeworks/{userId}`, `GET /api/settings/enable/reg`, `GET /api/settings/disable/reg`, `GET /api/examinationsettings/enabled`, `GET /api/examinationsettings/{enable|disable}/{first|second}` |

RoboExamination student endpoints accept a caller-supplied `userId`; this matches the SPA's existing session-storage ID model and is not authentication. A caller can impersonate a known user ID, including reading that user's submitted answers through the results endpoint. Instructor endpoints for creating examination variants and re-evaluating submissions require `X-Admin-Key`, configured with `RoboExaminationSettings:AdminKey`. Do not send this key from the SPA.

The legacy Chatbot endpoints remain for historical data and are no longer used by the SPA.

### Robo assessments

An examination is the combination of RoboQuiz and RoboExamination, for either `First` or `Second`. The SPA checks `GET /api/ExaminationSettings/enabled`, which returns `{"id":1,"type":"First"}` or `{"id":2,"type":"Second"}` for the corresponding settings, or HTTP 204 when neither is enabled. Enabling one examination disables the other in the same database statement.

The homepage shows the active examination. Students without results for that type start at `/quiz` with one navigation click (the quiz still has its own instructions/start screen). Background availability refreshes keep the navigation links mounted. After the last answer students see their answers and percentage result; once saved, they can continue to `/robo-examination`. A saved result in either component shows **View examination results**. **Continue to RoboExamination** is offered only while that component has not been submitted. The examination API checks the active type and quiz prerequisite; no minimum quiz score is required.

The `/robo-examination/results` page combines the saved RoboQuiz percentage, variant and completion date with RoboExamination's grade, feedback, answers and evaluation status, filtered to the currently enabled type. Missing components are shown explicitly; pending/failed evaluation does not allow a second submission. Refresh rechecks the enabled type. The underlying result APIs retain full history for existing consumers; the shared SPA results service filters both histories together. No combined numeric grade is calculated because the two components use different scales.

Provision three definitions per type in **each** of `RoboQuizzes` and `RoboExaminations`, for example `First` variants A/B/C and `Second` variants D/E/F. Content must be provided by instructors; startup does not generate six question sets. Quiz and examination variant names are independent: a student may receive quiz A and examination C. Each component chooses randomly from its type's configured pool, persists the assignment, and reuses it on reload. A student may submit only one variant of each component per type, not every available variant. Adding a variant or toggling the enabled type never resets assignments or results. Existing quiz results also prevent another quiz for that type; historical multiple-variant results remain visible, not deleted.

Deploy the `AddRoboQuizAttempts` EF migration with the backend (startup already applies pending migrations). It adds a unique user/type assignment for quizzes, matching the existing examination assignment rule. Submitting a non-assigned quiz is rejected, and quiz results and scoreboard updates are saved together.

Add an examination definition with the instructor endpoint. The configured `RoboExaminationSettings:AdminKey` is sent in `X-Admin-Key` and must never be included in the SPA. Only open-text questions are supported: every question needs a unique string `id` (max 50 characters), `text`, and `"type": "text"`. `expectedAnswer` is no longer part of RoboExamination: evaluation uses the question and student's answer only. Both definition APIs normalize `first`/`second` to `First`/`Second`.

Startup upgrades existing examination definitions, including instructor-created variants. Legacy choices become plain-text context within the question, followed by "Answer in your own words"; `options` and `expectedAnswer` fields are removed and the type becomes `text`. Question IDs are preserved (legacy numeric IDs are read as strings), and assignments, submitted answers, and existing grades are not changed. The upgrade is idempotent and saved transactionally before requests are served; malformed legacy questions stop startup with an explicit error instead of losing their context. Back up the database before deploying, as with other data upgrades.

The chatbot uses a full-width, resizable six-row answer box (up to 2000 characters). Enter adds a line break; Ctrl+Enter or Cmd+Enter submits the answer.

```bash
curl --request POST 'https://students-manager.azurewebsites.net/api/RoboExamination' \
  --header 'Content-Type: application/json' \
  --header 'X-Admin-Key: <private-admin-key>' \
  --data '{
    "type": "First",
    "variant": "variantC",
    "questions": [
      {
        "id": "q1",
        "text": "Which array method returns a new array? Explain how it works.",
        "type": "text"
      },
      {
        "id": "q2",
        "text": "Explain the difference between let and const.",
        "type": "text"
      }
    ]
  }'
```

### Forum

Get paginated forum posts:

```bash
curl --location 'https://students-manager.azurewebsites.net/api/slido?limit=20&skip=0'
```

Post a question:

```bash
curl --location 'https://students-manager.azurewebsites.net/api/slido/question' \
  --header 'Content-Type: application/json' \
  --data '{
    "question": "How do I submit the coursework?"
  }'
```

Post a comment:

```bash
curl --location 'https://students-manager.azurewebsites.net/api/slido/comment' \
  --header 'Content-Type: application/json' \
  --data '{
    "forumQuestionId": 4,
    "description": "Check the assignment deadline in the course page."
  }'
```

Get question text only:

```bash
curl --location 'https://students-manager.azurewebsites.net/api/slido/questions?limit=20&skip=0'
```

Example response:

```json
[
  "How do I submit the coursework?",
  "When is the next examination?"
]
```

### Login and Profile

Log in with an email and password:

```bash
curl --request POST \
  --url 'https://students-manager.azurewebsites.net/api/login' \
  --header 'Content-Type: application/json' \
  --data '{
    "email": "student@example.edu",
    "password": "password"
  }'
```

Successful response:

```json
{
  "userId": "1eac9820-5e6e-4d10-6e94-08de36f40f78"
}
```

Invalid credentials return:

```json
{
  "message": "Invalid email or password."
}
```

Get a student profile:

```bash
curl --location 'https://students-manager.azurewebsites.net/api/students/profile/<student-id>'
```

Example response:

```json
{
  "id": "022a6007-f33c-47c3-b811-08de88b121f2",
  "fullName": "Dr J",
  "base64EncodePicture": null,
  "facultyNumber": "987987",
  "testQuestions": [
    {
      "testQuestionDescription": "Which syntax references an external script named xxx.js?",
      "questionOptionDescription": "<script src=xxx.js>",
      "wasCorrect": true
    }
  ]
}
```

The profile endpoint returns `404 Not Found` when no student matches the supplied ID.

Update a profile picture:

```bash
curl --location --request PUT 'https://students-manager.azurewebsites.net/api/students/picture' \
  --header 'Content-Type: application/json' \
  --data '{
    "facultyNumber": "123123123",
    "password": "password",
    "picture": "data:image/jpeg;base64,/9j/k="
  }'
```

### Events

Create an event:

```bash
curl --location 'https://students-manager.azurewebsites.net/api/events' \
  --header 'Content-Type: application/json' \
  --data '{
    "userId": "<user-id>",
    "type": "geolocation-position",
    "data": "{\"latitude\":43.2141,\"longitude\":27.9147}"
  }'
```

The endpoint returns `201 Created` with the persisted event. Retrieve events for a user with:

```bash
curl --location 'https://students-manager.azurewebsites.net/api/events/<user-id>'
```

Example response:

```json
[
  {
    "id": "fddea52a-5702-46c8-86f6-00ed51641660",
    "userId": "022a6007-f33c-47c3-b811-08de88b121f2",
    "datetimeUtc": "2026-03-24T14:33:42.3137213",
    "type": "geolocation-position",
    "data": "{\"latitude\":43.2141,\"longitude\":27.9147}"
  }
]
```

### Chatbot Examination Answers

Submit answers for evaluation. The request must include at least one answer; the result is saved even if the AI evaluation is unsuccessful.

```bash
curl --location 'https://students-manager.azurewebsites.net/api/chatbot/examination-answers' \
  --header 'Content-Type: application/json' \
  --data-raw '{
    "userId": "<user-id>",
    "answers": [
      {
        "questionId": "q123123",
        "questionText": "What is programming?",
        "answer": "Programming is the process of writing instructions for a computer."
      }
    ]
  }'
```

Retrieve previously saved examination answers:

```bash
curl --location 'https://students-manager.azurewebsites.net/api/chatbot/examination-answers/<student-id>'
```

## Tests

Run the test project directly:

```bash
dotnet test StudentsManager.Tests/StudentsManager.Tests.csproj
```

Run the Azure chat SDK compatibility regression tests without Azure credentials or SQL Server:

```bash
dotnet test StudentsManager.Tests/StudentsManager.Tests.csproj --filter FullyQualifiedName~AzureOpenAiCompatibilityTests
```

These tests use the real SDK with a local fake HTTP transport to verify token-limit serialization and evaluation response parsing for both Azure chat deployments.

Or run the Docker-based test environment, which starts SQL Server for the test container:

```bash
docker compose -f docker-compose.integration.yml up --exit-code-from studentsmanager.tests --build
```

## Repository Layout

```text
StudentsManager.Mvc/             ASP.NET Core MVC application, controllers, Razor views, services, EF migrations
StudentsManager.Spa/             React/Vite single-page application
StudentsManager.Tests/           xUnit test project
docker-compose.yml               Base Docker Compose services
docker-compose.override.yml      Local development Docker configuration
docker-compose.integration.yml   Docker test environment
```

## License

See [LICENSE](LICENSE) for license details.
