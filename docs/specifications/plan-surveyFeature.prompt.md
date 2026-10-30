## Plan: Survey feature (StudentsManager.Mvc)

This adds a `Survey` table, a `SurveyController` and four views. A logged-in student answers the 4 questions once and uploads a required picture. The picture is saved with the existing `IStorageService.UploadToContainerAsync`, in the student's own container, the same way Homework uploads work. After submitting, the Survey page shows the student's answers read-only. `/Survey/Results` shows every submission as a card to any logged-in user. Styling reuses the `soge-*` classes from style.css, and style.css itself stays unchanged.

**Steps**

*Phase 1: Data layer*
1. Add a `Survey` entity in [Domain/Entities](StudentsManager.Mvc/Domain/Entities). It inherits `_Base.File` (FileName, Path, Extension, CreatedAtUtc), with the same `using File = …` alias as [Homework.cs](StudentsManager.Mvc/Domain/Entities/Homework.cs). Fields:
   - `int Id`
   - `PreviousExperience` (Q1)
   - `CourseExpectations` (Q2)
   - `Hobby` (Q3)
   - `DreamProject` (Q4)
   - `Guid UserId` and `User? User`
2. Add `ICollection<Survey>? Surveys` to [User.cs](StudentsManager.Mvc/Domain/Entities/User.cs#L21), next to `Courseworks`. *Parallel with step 1.*
3. Add `ConfigureSurveys()` to [OnModelCreatingConfiguration.cs](StudentsManager.Mvc/Persistence/OnModelCreatingConfiguration.cs#L245), modelled on `ConfigureUserCourseworks`:
   - required FK to User and a **unique index on UserId**
   - each answer: `IsRequired().HasMaxLength(2000).IsUnicode().HasComment(...)`
   - FileName and Path: required, max 500; Extension: required, max 100
4. In [ManagerDbContext.cs](StudentsManager.Mvc/Persistence/ManagerDbContext.cs#L48), add `DbSet<Survey> Surveys` and call `builder.ConfigureSurveys()` next to [line 62](StudentsManager.Mvc/Persistence/ManagerDbContext.cs#L62).
5. Generate the migration: `dotnet ef migrations add AddSurveys --project StudentsManager.Mvc`. It is applied on startup by [Program.cs](StudentsManager.Mvc/Program.cs#L35). *Depends on steps 1–4.*

*Phase 2: Application layer (parallel with Phase 1; step 9 depends on step 1)*

6. Add `SurveyConstants` in a new Surveys folder under [Services](StudentsManager.Mvc/Services), following the existing `AuthConstants` pattern. It holds:
   - the 4 question texts, as `const` so `[Display]` can use them
   - `MaxPictureSizeInBytes` = 5 MB
   - allowed extensions (`.jpg`, `.jpeg`, `.png`, `.webp`) and matching content types
   - the blob name prefix `survey-`
7. Add `SurveySubmission` in a new Surveys folder under [Domain/Inputs](StudentsManager.Mvc/Domain/Inputs):
   - 4 answers with `[Required]`, `[StringLength(2000)]` and `[Display(Name = question)]`
   - `[Required] IFormFile? Picture`
   - implements `IValidatableObject`: rejects empty files, files over 5 MB, and any extension or content type not on the allowlist (so SVG and GIF are rejected)
8. Add `SurveyResultView` in a new Surveys folder under [Domain/Views](StudentsManager.Mvc/Domain/Views): FullName, the 4 answers, PicturePath, CreatedAtUtc.
9. Add `ISurveysService` / `SurveysService`, built like [HomeworksService.UploadAsync](StudentsManager.Mvc/Services/Homeworks/HomeworksService.cs#L41) (injects `ManagerDbContext` and `IStorageService`):
   - `GetByUserIdAsync(userId)` returns `SurveyResultView?`.
   - `SubmitAsync(userId, input)`:
     - Builds the blob name on the server as `survey-{Guid:N}{ext}`. It never uses the client's file name.
     - Uploads to container `userId`, disposing the stream afterwards.
     - Stores Path = `PathBasis + userId + "/" + fileName` and Extension = content type (same convention as the existing upload code).
     - Saves trimmed answers with `CreatedAtUtc = UtcNow`.
   - `GetAllAsync()`: `AsNoTracking`, skips soft-deleted users (`user.IsDeleted == false`, as in [StudentsGradingServiceV2](StudentsManager.Mvc/Services/Statistics/StudentsGradingServiceV2.cs#L24)), newest first. Both reads share one projection.
10. Register `AddScoped<ISurveysService, SurveysService>()` in [DependenciesConfiguration.AddApplicationServices](StudentsManager.Mvc/Configurations/DependenciesConfiguration.cs#L129).

*Phase 3: Web layer (depends on Phase 2)*

11. Add `SurveyController : BaseController` in [Controllers](StudentsManager.Mvc/Controllers), with `[Authorize]` and a primary constructor like `AuthController`. It gets the user id from `IPrincipalService.GetUserIdByClaimsPrincipal`. Actions:
    - `GET Index`: if the user already submitted, shows `View("Submitted", result)`; otherwise the empty form.
    - `POST Index` with `[ValidateAntiForgeryToken]`, as in [ForumController](StudentsManager.Mvc/Controllers/ForumController.cs#L28):
      - already submitted: redirect back
      - invalid `ModelState`: show the form again with errors
      - otherwise `SubmitAsync`, then redirect back to Index (post-redirect-get, so a refresh doesn't resubmit)
    - `GET Results`: all submissions.
12. Add four views in a new `Views/Survey` folder (default `_Layout`, wrapped in `.total-wrap-content` so content clears the fixed header):
    - **Index**:
      - Form: `method="post"`, `enctype="multipart/form-data"`.
      - Layout: `.soge-title` heading with a `.red` accent; each question in a `.soge-question`.
      - Inputs: each answer is a textarea (`maxlength`) inside `.soge-input-wrapper > .soge-input` with `.soge-input-line`, the same structure as [script.js](StudentsManager.Mvc/wwwroot/js/script.js#L2751). The file input has `accept=".jpg,.jpeg,.png,.webp"` and a "max 5 MB" hint.
      - Submit: `.soge-btn-wrapper > button.soge-btn`.
      - Validation: messages shown next to each field; client-side validation scripts included via `_ValidationScriptsPartial`.
    - **_SurveyResultPartial** (one card): picture (`loading="lazy"`, alt text), name in `.red`, the 4 questions with answers, and the date. Answers render through normal Razor encoding (no `Html.Raw`), with `white-space: pre-line` to keep line breaks.
    - **Submitted**: thank-you message, the user's card from the partial, and a link to Results.
    - **Results**: a grid of cards and a message when there are no submissions yet.
    - View-specific CSS (textarea sizing, results grid, image size) goes in `@section Styles`, as in [Homework.cshtml](StudentsManager.Mvc/Pages/Homework.cshtml#L8). It uses the style.css colours (#b16f5f, #e3e3e3, #6f6f6f).
13. Add a "Survey" nav item in the `menu-left` list of [_Layout.cshtml](StudentsManager.Mvc/Views/Shared/_Layout.cshtml#L66), after "Профил" ([L76](StudentsManager.Mvc/Views/Shared/_Layout.cshtml#L76)), using `asp-controller="Survey" asp-action="Index"`.

**Relevant files**
- [ManagerDbContext.cs](StudentsManager.Mvc/Persistence/ManagerDbContext.cs), [OnModelCreatingConfiguration.cs](StudentsManager.Mvc/Persistence/OnModelCreatingConfiguration.cs), [User.cs](StudentsManager.Mvc/Domain/Entities/User.cs), [DependenciesConfiguration.cs](StudentsManager.Mvc/Configurations/DependenciesConfiguration.cs), [_Layout.cshtml](StudentsManager.Mvc/Views/Shared/_Layout.cshtml): modified
- [StorageService.cs](StudentsManager.Mvc/Services/Storage/StorageService.cs#L32): reused unchanged
- [HomeworksService.cs](StudentsManager.Mvc/Services/Homeworks/HomeworksService.cs), [CourseworksService.cs](StudentsManager.Mvc/Services/Courseworks/CourseworksService.cs), [AuthController.cs](StudentsManager.Mvc/Controllers/AuthController.cs), [Register.cshtml](StudentsManager.Mvc/Views/Auth/Register.cshtml): templates to copy from

**Verification**
1. Run `dotnet build StudentsManager.sln`.
2. Check the generated migration and model snapshot:
   - `Surveys` table with 4 × `nvarchar(2000)` NOT NULL answer columns
   - `nvarchar(500)` FileName and Path, `nvarchar(100)` Extension, `datetimeoffset` CreatedAtUtc
   - FK to `AspNetUsers` and a unique `IX_Surveys_UserId`
3. Optional: xUnit + Moq tests for `SurveySubmission.Validate` (Moq is already referenced in StudentsManager.Tests):
   - valid 1 MB `.jpg`
   - 0 bytes
   - 6 MB
   - `.gif`
   - `.svg`
   - `.png` sent with a `text/html` content type
4. Manual checks:
   - The nav link opens the form; an empty submit shows validation messages; a bad picture shows a readable error.
   - A valid submit shows the Submitted view with the picture. The blob `survey-<guid>.<ext>` is in container `<userId>` and there is one row in `dbo.Surveys`.
   - Going back and resubmitting adds no second row.
   - Another student can see Results; a logged-out visitor is sent to `/auth/login`.
   - An answer of `<script>alert(1)</script>` is shown as plain text.
   - The nav still looks right at 1025–1280 px and in the mobile menu.

**Decisions**
- From your answers: Results open to any logged-in user; one read-only submission per user; English text exactly as given; nav link; style.css not edited; picture required, JPG/PNG/WEBP, max 5 MB.
- The student's name comes from `User.FullName`. The name/program question in [specifications/survey.txt](specifications/survey.txt) is left out, following your list.
- The upload relies on the per-user container created at registration ([AuthService.cs](StudentsManager.Mvc/Services/Auth/AuthService.cs#L63)), the same assumption the Homework and exam uploads make.
- No explicit request size limit is added. Kestrel's default (~28 MB) caps uploads, and the 5 MB rule gives a readable validation message.
- Out of scope: roles or an admin area, editing or deleting submissions, pagination, localization, SPA changes, feature toggles.

**Further Considerations**
1. **Picture content type.** `UploadToContainerAsync` doesn't set a Content-Type, so blobs are stored as `application/octet-stream`. They still display in `<img>`, but opening the URL directly downloads the file. Option A (recommended): keep as is. Option B: add an optional `contentType` parameter that sets the blob's Content-Type; existing callers are unaffected.
2. **Double submit.** Two simultaneous submits would hit the unique index. The second gets a 500 error and leaves an unused blob. Option A: accept, since it's rare. Option B: catch `DbUpdateException` and redirect, and/or disable the submit button after the first click.
