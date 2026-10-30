## Plan: Database-Backed RoboQuiz

Replace the static [quizQuestions.js](../StudentsManager.Spa/src/data/quizQuestions.js) file (24 questions, 4 sampled per attempt) and the `/Events` result logging with quiz variants stored in the database and percentage results. The SPA stays the quiz UI. `StudentsManager.Mvc` serves quiz content, gates access by the active `CourseExaminationSetting`, and records one result per user per variant.

**Requirements (fixed)**
1. No Identity cookies, claims, or `[Authorize]`. The SPA sends the `userId` held in `sessionStorage` (set by [LoginPage.jsx](../StudentsManager.Spa/src/pages/LoginPage.jsx) from `POST /api/Login`). This matches `Events` and `Chatbot`.
2. Implementation only. No unit tests are required.
3. Persist only `ScorePercentage`. Selected answer IDs are stored nowhere, including in `Events`.
4. Only logged-in users see the quiz. Content is served only while an examination setting is enabled, and the server enforces this too.

---

### Gaps found in the original plan and how this plan resolves them

| # | Gap | Resolution |
|---|-----|------------|
| 1 | "Quiz data based on active settings" names no link between a quiz and a setting. | `RoboQuiz.Type` stores the `CourseExaminationSetting.Type` value (`"First"` / `"Second"`). The server resolves the active setting itself; the client never chooses the type. |
| 2 | The gate exists only in the client. Anyone can call `GET /RoboQuiz` or `POST /RoboQuizResults` directly. | Both endpoints check the active setting on the server. They return `403` when nothing is enabled or when the quiz's type is not the active one. |
| 3 | `FirstOrDefault(Enabled)` is nondeterministic when "First" and "Second" are both enabled. | Order by `Id` and take the first one. Document the rule. |
| 4 | Routes `/RoboQuiz` and `/ExaminationSettings/Enabled` don't match the project. Every API controller uses `api/[controller]`, and the SPA `baseUrl` ends in `/api`. | Use `api/RoboQuiz`, `api/RoboQuizResults`, `api/ExaminationSettings/enabled` (lowercase, like the existing `enable/first`). |
| 5 | Without Identity, the API has no way to know the user. | `userId` is passed explicitly: query string on GET, body on POST. Validate that it parses as a `Guid` and that the user exists and is not soft-deleted. |
| 6 | Nothing prevents duplicate results or races, so "exclude completed variants" can drift. | Unique index on `(UserId, RoboQuizId)`. On `DbUpdateException`, return `409`. |
| 7 | The percentage is unbounded. | `[Range(0, 100)]` on the input plus a DB check constraint. |
| 8 | `POST /RoboQuiz` accepts arbitrary JSON, which can break the SPA renderer. | Bind to typed DTOs that mirror `quizQuestions.js`. Validate structure, then serialize to camelCase. Unique `(Type, Variant)`. |
| 9 | Returning `StringifiedJsonContent` as-is double-encodes JSON (`"[{\"id\":…}]"`). | Deserialize the content into DTOs and return it as a real JSON array. |
| 10 | The history panel reads `Events`, which will no longer receive quiz results. | Add `GET api/RoboQuizResults/{userId}`. Add `CompletedAtUtc` to the result entity so history can be ordered. |
| 11 | Scoring is hard-coded to 4 questions (`QUIZ_QUESTION_COUNT`, pass at 4 correct, half point at 2). | Run every question in the variant, in shuffled order. Base points on percentage: 100% gives 1 point, ≥50% gives 0.5. The 4-question behaviour stays the same. |
| 12 | Answer-level `quiz-answer` events log `selectedId`, which breaks requirement 3. | Remove all quiz `Events` logging, the guest ID, and `attemptId` from the quiz. |
| 13 | Once the static file is gone, the database has no content. | Bootstrap by posting the 24 existing questions as six 4-question variants (step 13), then delete the file. |
| 14 | `/quiz` and its header link are public. | Move the route into the `isLoggedIn` block (redirect to `/login`) and hide the link for guests. |
| 15 | The "no quiz" outcomes are unclear. | One documented status per outcome (see the API contract). The SPA shows a distinct message for each. |

---

### Data model

**`RoboQuiz`** in [Domain/Entities](../StudentsManager.Mvc/Domain/Entities)
- `int Id`
- `string Type`: required, max 100, equal to a `CourseExaminationSetting.Type`
- `string Variant`: required, max 100
- `string StringifiedJsonContent`: required, `nvarchar(max)`, camelCase JSON array in the `quizQuestions.js` shape
- `ICollection<RoboQuizUserResult>? Results`
- Unique index `(Type, Variant)`

**`RoboQuizUserResult`**
- `int Id`
- `int ScorePercentage`: check constraint `ScorePercentage BETWEEN 0 AND 100`
- `DateTime CompletedAtUtc`: added for history ordering. It is not an answer ID, so requirement 3 still holds.
- `int RoboQuizId` → `RoboQuiz` (required; `DeleteBehavior.Restrict` so deleting a quiz cannot erase scores)
- `Guid UserId` → `User` (required)
- Unique index `(UserId, RoboQuizId)`

**Content schema**, validated on create:
```json
[
  { "id": 1, "question": "…", "options": [ { "id": 1, "text": "…" } ], "correctAnswer": 2 }
]
```
Rules:
- At least 1 question.
- Question `id`s are unique.
- Each question has non-empty `question` text and ≥2 options.
- Option `id`s are unique within a question.
- `correctAnswer` matches one of the question's option `id`s.

---

### API contract

All responses are JSON. Errors use `{ "message": "…" }`, which [apiConfig.getErrorMessage](../StudentsManager.Spa/src/services/apiConfig.js) already reads.

| Endpoint | Success | Errors |
|----------|---------|--------|
| `GET api/ExaminationSettings/enabled` | `200 { id, type }`, the first enabled setting by `Id` | `204` when none is enabled |
| `GET api/RoboQuiz?userId={guid}` | `200 { id, type, variant, questions: [...] }`, a random variant of the active type that the user hasn't completed | `400` bad/missing `userId` · `404` unknown user · `403` no active examination · `404` no variants for the active type ("Няма налични тестове") · `410` all variants completed ("Вече сте решили всички варианти") |
| `POST api/RoboQuiz` body `{ type, variant, questions: [...] }` | `201 { id, type, variant }` | `400` validation · `409` duplicate `(type, variant)` |
| `POST api/RoboQuizResults` body `{ userId, roboQuizId, scorePercentage }` | `201 { id, roboQuizId, scorePercentage, completedAtUtc }` | `400` validation · `404` unknown user/quiz · `403` the quiz's type is not the active examination · `409` already submitted |
| `GET api/RoboQuizResults/{userId}` | `200 [{ id, type, variant, scorePercentage, completedAtUtc }]`, newest first | `400` bad `userId` |

`410 Gone` separates "you have finished everything" from "nothing is configured" (`404`). The original plan's "appropriate feedback" requirement needs that distinction.

---

### Steps

*Phase 1: Persistence*
1. Add `RoboQuiz.cs` and `RoboQuizUserResult.cs` under [Domain/Entities](../StudentsManager.Mvc/Domain/Entities). Add `ICollection<RoboQuizUserResult>? RoboQuizResults` to [User.cs](../StudentsManager.Mvc/Domain/Entities/User.cs) next to `PowerUpAnalyses`.
2. Add `ConfigureRoboQuizzes()` to [OnModelCreatingConfiguration.cs](../StudentsManager.Mvc/Persistence/OnModelCreatingConfiguration.cs), modelled on `ConfigurePowerUpAnalyses`. It configures the max lengths, both unique indexes, the FKs (Restrict on quiz), and the check constraint (`ToTable(t => t.HasCheckConstraint(...))`).
3. In [ManagerDbContext.cs](../StudentsManager.Mvc/Persistence/ManagerDbContext.cs), add `DbSet<RoboQuiz> RoboQuizzes` and `DbSet<RoboQuizUserResult> RoboQuizUserResults` and call `builder.ConfigureRoboQuizzes()`.
4. Run `dotnet ef migrations add AddRoboQuizzes --project StudentsManager.Mvc`. [Program.cs](../StudentsManager.Mvc/Program.cs) applies it on startup. *Depends on 1–3.*

*Phase 2: Application layer (parallel with Phase 1 except for entity types)*

5. **Inputs/views** in new `RoboQuizzes` folders under [Domain/Inputs](../StudentsManager.Mvc/Domain/Inputs) and [Domain/Views](../StudentsManager.Mvc/Domain/Views):
   - `RoboQuizQuestion`, `RoboQuizOption`: the content DTOs, shared by input and output.
   - `RoboQuizInput`: `Type`, `Variant`, `Questions`. Implements `IValidatableObject` for the content rules.
   - `RoboQuizResultInput`: `UserId`, `RoboQuizId`, `[Range(0,100)] ScorePercentage`.
   - `RoboQuizView`, `RoboQuizResultView`.
6. **Examination settings.** Add `Task<CourseExaminationSetting?> GetFirstEnabledAsync()` to [IExaminationSettingsService](../StudentsManager.Mvc/Services/CourseExaminations/IExaminationSettingsService.cs) and [ExaminationSettingsService](../StudentsManager.Mvc/Services/CourseExaminations/ExaminationSettingsService.cs): `AsNoTracking().Where(s => s.Enabled).OrderBy(s => s.Id).FirstOrDefaultAsync()`.
7. **`IRoboQuizService` / `RoboQuizService`** in `Services/RoboQuizzes`, injecting `ManagerDbContext` and `IExaminationSettingsService`. It returns a small result type (status + payload) so controllers only map to HTTP:
   - `GetNextAsync(Guid userId)`: check the user exists → get the active setting → load variants of `setting.Type` with no result for `userId` → pick one with `OrderBy(_ => Guid.NewGuid())` (SQL Server `NEWID()`) → deserialize the content. Distinguish "no variants" (none exist for the type) from "exhausted" (they exist, all completed).
   - `CreateAsync(RoboQuizInput)`: trim `Type`/`Variant`. Serialize `Questions` with Newtonsoft `CamelCasePropertyNamesContractResolver` (the app uses `AddNewtonsoftJson`). Map a unique-index violation to the duplicate status.
   - `SubmitResultAsync(RoboQuizResultInput)`: check the user and quiz exist → check `quiz.Type == activeSetting.Type` → insert with `CompletedAtUtc = DateTime.UtcNow` → map a unique-index `DbUpdateException` to the duplicate status. This covers concurrent double-submits.
   - `GetResultsAsync(Guid userId)`: `AsNoTracking`, join quiz for type/variant, newest first.
8. Register `AddScoped<IRoboQuizService, RoboQuizService>()` in [DependenciesConfiguration.AddApplicationServices](../StudentsManager.Mvc/Configurations/DependenciesConfiguration.cs).

*Phase 3: Web layer (depends on Phase 2)*

9. Add to [ExaminationSettingsController](../StudentsManager.Mvc/Controllers/ExaminationSettingsController.cs): `[HttpGet("enabled")]` → `200 { id, type }` or `NoContent()`.
10. `RoboQuizController` (`[Route("api/[controller]")]`, `[ApiController]`, primary constructor like [LoginController](../StudentsManager.Mvc/Controllers/LoginController.cs)): `GET` with `[FromQuery] Guid userId` and `POST` with `RoboQuizInput`. No `[Authorize]` (requirement 1).
11. `RoboQuizResultsController`: `POST` with `RoboQuizResultInput`, `GET {userId:guid}`. `CreatedAtAction` points at the GET.

*Phase 4: SPA (depends on Phase 3 contract only; can start against mocks)*

12. Add `services/roboQuizService.js` using `axios` and `baseUrl`, as in [chatbotService.js](../StudentsManager.Spa/src/services/chatbotService.js): `getEnabledExamination()`, `getNextQuiz(userId)`, `submitQuizResult({ userId, roboQuizId, scorePercentage })`, `getQuizResults(userId)`. Map HTTP statuses to typed outcomes (`disabled`, `none`, `exhausted`, `duplicate`).
13. **Bootstrap content** before deleting the static file. Post the 24 questions from [quizQuestions.js](../StudentsManager.Spa/src/data/quizQuestions.js) to `POST api/RoboQuiz` as `Type: "First"`, `Variant: "A"…"F"` (4 questions each) using a one-off Node script run locally. Do not commit it. Then delete `src/data/quizQuestions.js`.
14. Refactor [useQuiz.js](../StudentsManager.Spa/src/components/Quiz/useQuiz.js):
    - Take no `questions` argument.
    - Remove `getGuestQuizUserId`, `createAttemptId`, `logQuizEvent`, `QUIZ_EVENT_TYPES`, and the `eventsService` import.
    - On mount (with `userId`), load the settings first, then the next quiz, and set `status`: `loading | ready | disabled | none | exhausted | error`.
    - `shuffledQuestions = shuffleArray(quiz.questions)` (whole variant, no slicing).
    - On the last answer, call `calculateQuizResult`, then submit `{ userId, roboQuizId: quiz.id, scorePercentage: result.percentage }` and track `submitStatus: idle | submitting | saved | failed | duplicate`. Keep answers in memory on failure and expose `retrySubmit`. Treat `409` as already saved.
    - History comes from `getQuizResults(userId)` and is refreshed after a successful save.
    - `handleRestart` loads the *next* variant. A completed variant is never re-run.
    - Answer review stays client-side only (in memory, never sent).
15. Update [quizConstants.js](../StudentsManager.Spa/src/components/Quiz/quizConstants.js): remove `QUIZ_QUESTION_COUNT`. Replace the count-based thresholds with `QUIZ_PASS_PERCENTAGE = 100` and `QUIZ_HALF_POINT_PERCENTAGE = 50`. Update [QuizStart.jsx](../StudentsManager.Spa/src/components/Quiz/QuizStart.jsx) wording to match.
16. Update [Quiz.jsx](../StudentsManager.Spa/src/components/Quiz/Quiz.jsx) and [QuizStart.jsx](../StudentsManager.Spa/src/components/Quiz/QuizStart.jsx):
    - Drop the static import.
    - Render loading, disabled ("Няма активен изпит"), none, exhausted, and error-with-retry states.
    - Show type/variant on the start screen.
    - Map history fields to `{ date: completedAtUtc, percentage: scorePercentage, type, variant }`.
    - Show save status and retry on [QuizResults.jsx](../StudentsManager.Spa/src/components/Quiz/QuizResults.jsx).
17. Gate access. In [App.jsx](../StudentsManager.Spa/src/App.jsx), move `/quiz` into the `isLoggedIn` block and add `<Route path='/quiz' element={<Navigate to='/login' replace />} />` to the guest block. In [Header.jsx](../StudentsManager.Spa/src/components/Layout/Header.jsx), wrap the Quiz link in `isLoggedIn`. These are the only two `/quiz` references today.

*Phase 5: Verification (no unit tests per requirement 2)*

18. Backend: `dotnet build StudentsManager.sln`. Confirm the generated migration contains both tables, both unique indexes, the check constraint, and the Restrict FK.
19. SPA: `npm run lint` and `npm run build` in `StudentsManager.Spa`. Grep to confirm no remaining references to `quizQuestions`, `quizGuestUserId`, or `quiz-answer`.
20. Manual API smoke test (curl), in order:
    - The settings endpoint returns `204` when everything is disabled, and the quiz endpoint returns `403`.
    - Enable "First" (`GET api/ExaminationSettings/enable/first`).
    - Create a variant → `201`. Create it again → `409`. Invalid `correctAnswer` → `400`.
    - Get a quiz, then submit → `201`. Submit again → `409`. `scorePercentage: 101` → `400`.
    - After all variants are submitted, GET → `410`.
    - Disable "First", then submit for one of its variants → `403`.
21. Manual browser check: as a guest, `/quiz` redirects to login and the link is hidden. When logged in, cover a full run, the disabled/exhausted messages, a failed save + retry (stop the API mid-quiz), history refresh, and mobile layout.

---

### Decisions and trade-offs
- **Client-side grading.** Requirement 3 stores only the percentage, so `correctAnswer` is sent to the browser and the client computes the score. A determined user can forge it. Accepted for this scope. Server-side grading would need answer IDs submitted (not persisted) and keys hidden.
- **Caller-supplied `userId`.** With no Identity (requirement 1), any caller can act as any user ID. This matches the existing `Events`/`Chatbot` APIs. The unique index and the active-exam check limit the impact.
- **`POST api/RoboQuiz` is unauthenticated**, like the existing `enable/*` toggles. Recommended follow-up (out of scope): require an admin key from configuration via a header.
- **Immutable variants.** No update/delete endpoints. Changing content means creating a new variant, so existing scores keep their meaning.
- **No Events backfill.** Historic `quiz-result` events stay in `Events` and are not migrated.
- **CORS** stays `AllowAnyOrigin`. That is valid because no credentials or cookies are sent.

### Files touched
- **Mvc (new):** `Domain/Entities/RoboQuiz.cs`, `Domain/Entities/RoboQuizUserResult.cs`, `Domain/Inputs/RoboQuizzes/*`, `Domain/Views/RoboQuizzes/*`, `Services/RoboQuizzes/IRoboQuizService.cs`, `Services/RoboQuizzes/RoboQuizService.cs`, `Controllers/RoboQuizController.cs`, `Controllers/RoboQuizResultsController.cs`, `Migrations/*_AddRoboQuizzes*.cs`
- **Mvc (edit):** `User.cs`, `ManagerDbContext.cs`, `OnModelCreatingConfiguration.cs`, `IExaminationSettingsService.cs`, `ExaminationSettingsService.cs`, `ExaminationSettingsController.cs`, `DependenciesConfiguration.cs`, `ManagerDbContextModelSnapshot.cs` (generated)
- **Spa (new):** `src/services/roboQuizService.js`
- **Spa (edit):** `App.jsx`, `Header.jsx`, `Quiz.jsx`, `useQuiz.js`, `quizConstants.js`, `QuizStart.jsx`, `QuizResults.jsx`
- **Spa (delete):** `src/data/quizQuestions.js` (after step 13)
