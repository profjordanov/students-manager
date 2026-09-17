flowchart TD

subgraph group_clients["Client applications"]
  node_spa_bootstrap["React SPA<br/>Vite React client<br/>[main.jsx]"]
  node_spa_routes["SPA routes and UI<br/>React application<br/>[App.jsx]"]
  node_spa_auth["SPA auth state<br/>React context<br/>[AuthContext.jsx]"]
  node_spa_features["Quiz, chatbot and events<br/>SPA features<br/>[useQuiz.js]"]
  node_mvc_views["Razor MVC UI<br/>server-rendered views<br/>[Index.cshtml]"]
  node_razor_pages["Exam and grading pages<br/>Razor Pages"]
end

subgraph group_backend["ASP.NET Core backend"]
  node_application["ASP.NET Core host<br/>application entry point<br/>[Program.cs]"]
  node_composition["DI and registrations<br/>composition root"]
  node_controllers["MVC and API controllers<br/>transport boundary"]
  node_auth["Authentication and principal<br/>auth services<br/>[AuthService.cs]"]
  node_assessment["Tests and answers<br/>assessment services<br/>[TestsService.cs]"]
  node_examinations["Examination workflow<br/>exam services"]
  node_grading["Grading and reporting<br/>statistics service"]
  node_community["Forum and events<br/>community service<br/>[ForumService.cs]"]
  node_chatbot["Chatbot answer flow<br/>AI feature service"]
end

subgraph group_data["Domain and persistence"]
  node_dbcontext[("EF Core database boundary<br/>DbContext")]
  node_assessment_model["Assessment model<br/>domain entities"]
  node_sql_server[("SQL Server<br/>relational database")]
end

subgraph group_integrations["External services"]
  node_ai_agent["AI agent integration<br/>Agent Framework service"]
  node_azure_bus["Azure Service Bus<br/>message broker"]
  node_storage["Blob-like storage<br/>storage service<br/>[StorageService.cs]"]
end

node_spa_bootstrap -->|"boots"| node_spa_routes
node_spa_routes -->|"uses"| node_spa_auth
node_spa_routes -->|"composes"| node_spa_features
node_spa_auth -->|"auth API calls"| node_controllers
node_spa_features -->|"API calls"| node_controllers
node_mvc_views -->|"rendered by"| node_controllers
node_razor_pages -->|"hosted by"| node_application
node_application -->|"configures"| node_composition
node_composition -->|"wires"| node_controllers
node_controllers -->|"delegates identity"| node_auth
node_controllers -->|"delegates tests"| node_assessment
node_controllers -->|"delegates exams"| node_examinations
node_controllers -->|"delegates community"| node_community
node_controllers -->|"delegates AI answers"| node_chatbot
node_auth -->|"persists users"| node_dbcontext
node_assessment -->|"persists answers"| node_dbcontext
node_examinations -->|"persists attempts"| node_dbcontext
node_grading -->|"persists results"| node_dbcontext
node_assessment -->|"computes reporting"| node_grading
node_examinations -->|"grades attempts"| node_grading
node_chatbot -->|"requests answers"| node_ai_agent
node_chatbot -->|"saves answers"| node_dbcontext
node_examinations -->|"stores uploads"| node_storage
node_dbcontext -->|"maps"| node_assessment_model
node_dbcontext -->|"reads and writes"| node_sql_server
node_community -->|"persists forums and events"| node_dbcontext
node_application -.->|"sends messages"| node_azure_bus

click node_spa_bootstrap "https://github.com/profjordanov/students-manager/blob/main/StudentsManager.Spa/src/main.jsx"
click node_spa_routes "https://github.com/profjordanov/students-manager/blob/main/StudentsManager.Spa/src/App.jsx"
click node_spa_auth "https://github.com/profjordanov/students-manager/blob/main/StudentsManager.Spa/src/context/AuthContext.jsx"
click node_spa_features "https://github.com/profjordanov/students-manager/blob/main/StudentsManager.Spa/src/components/Quiz/useQuiz.js"
click node_mvc_views "https://github.com/profjordanov/students-manager/blob/main/StudentsManager.Mvc/Views/Test/Index.cshtml"
click node_application "https://github.com/profjordanov/students-manager/blob/main/StudentsManager.Mvc/Program.cs"
click node_composition "https://github.com/profjordanov/students-manager/blob/main/StudentsManager.Mvc/Configurations/DependenciesConfiguration.cs"
click node_controllers "https://github.com/profjordanov/students-manager/blob/main/StudentsManager.Mvc/Controllers/StudentsController.cs"
click node_auth "https://github.com/profjordanov/students-manager/blob/main/StudentsManager.Mvc/Services/Auth/AuthService.cs"
click node_assessment "https://github.com/profjordanov/students-manager/blob/main/StudentsManager.Mvc/Services/Tests/TestsService.cs"
click node_examinations "https://github.com/profjordanov/students-manager/blob/main/StudentsManager.Mvc/Services/CourseExaminations/StudentCourseExaminationService.cs"
click node_grading "https://github.com/profjordanov/students-manager/blob/main/StudentsManager.Mvc/Services/Statistics/StudentsGradingServiceV2.cs"
click node_community "https://github.com/profjordanov/students-manager/blob/main/StudentsManager.Mvc/Services/Forum/ForumService.cs"
click node_chatbot "https://github.com/profjordanov/students-manager/blob/main/StudentsManager.Mvc/Services/Clicks/ExaminationAnswersService.cs"
click node_dbcontext "https://github.com/profjordanov/students-manager/blob/main/StudentsManager.Mvc/Persistence/ManagerDbContext.cs"
click node_assessment_model "https://github.com/profjordanov/students-manager/blob/main/StudentsManager.Mvc/Domain/Entities/CourseExamination.cs"
click node_ai_agent "https://github.com/profjordanov/students-manager/blob/main/StudentsManager.Mvc/Services/AI/AgentFrameworkService.cs"
click node_azure_bus "https://github.com/profjordanov/students-manager/blob/main/StudentsManager.Mvc/Services/Messaging/AzureServiceBusSender.cs"
click node_storage "https://github.com/profjordanov/students-manager/blob/main/StudentsManager.Mvc/Services/Storage/StorageService.cs"

classDef toneNeutral fill:#f8fafc,stroke:#334155,stroke-width:1.5px,color:#0f172a
classDef toneBlue fill:#dbeafe,stroke:#2563eb,stroke-width:1.5px,color:#172554
classDef toneAmber fill:#fef3c7,stroke:#d97706,stroke-width:1.5px,color:#78350f
classDef toneMint fill:#dcfce7,stroke:#16a34a,stroke-width:1.5px,color:#14532d
classDef toneRose fill:#ffe4e6,stroke:#e11d48,stroke-width:1.5px,color:#881337
classDef toneIndigo fill:#e0e7ff,stroke:#4f46e5,stroke-width:1.5px,color:#312e81
classDef toneTeal fill:#ccfbf1,stroke:#0f766e,stroke-width:1.5px,color:#134e4a
class node_spa_bootstrap,node_spa_routes,node_spa_auth,node_spa_features,node_mvc_views,node_razor_pages toneBlue
class node_application,node_composition,node_controllers,node_auth,node_assessment,node_examinations,node_grading,node_community,node_chatbot toneAmber
class node_dbcontext,node_assessment_model,node_sql_server toneMint
class node_ai_agent,node_azure_bus,node_storage toneRose
