# RoboQuiz and RoboExamination improvement plan

Status: proposed implementation plan. This document does not implement application changes.

## Scope

1. Replace the Quiz and RoboExamination navigation links with an active-assessment announcement below "Welcome, Fellow Builders".
2. Keep quiz results visible until the student clicks "Continue to RoboExamination".
3. Ensure instructor-created examinations deliver their option and text questions to eligible students.

Implementation only: no tests, test tooling, new dependencies, authoring UI, retakes, or draft-answer persistence. Reuse existing components, services, endpoints, and database tables.

This plan supersedes the navigation and automatic-redirect requirements in [roboexamination.prompt.md](roboexamination.prompt.md).

## Preserve existing rules

- The enabled-settings endpoint selects the active type. Keep its current first-enabled-setting behavior; do not add multi-assessment selection.
- One saved quiz result of the active type unlocks the examination, regardless of score. Completing every quiz variant is not required.
- Examination definitions are reusable across students. Keep random selection among matching variants and one stable attempt per `(userId, examinationType)`, including the existing concurrent-assignment protection.
- Quiz and examination variant names need not match. Creating `First / variantC` adds an eligible variant; it does not replace existing assignments or force every student to receive C.
- Preserve scoring, result history, evaluation, login guards, and existing routes, including `/chatbot` and `/chatbot/results`.

## 1. Move assessment entry to the homepage

Files: [Header.jsx](../../StudentsManager.Spa/src/components/Layout/Header.jsx), [HomePage.jsx](../../StudentsManager.Spa/src/pages/HomePage.jsx), [HomePage.css](../../StudentsManager.Spa/src/pages/HomePage.css), [roboQuizService.js](../../StudentsManager.Spa/src/services/roboQuizService.js).

- Remove only Quiz and RoboExamination from the shared desktop/mobile menu. Adjust menu-ref indexes without changing the remaining menu behavior or removing routes.
- Add the announcement immediately after the hero and before "Platform Capabilities", using existing styling and localization conventions. Keep it visible without depending on the homepage's one-time animation observer.
- Reuse `getEnabledExamination()` directly; no new settings service or global state is needed. Load on mount and refresh on window focus.
- For signed-in students, reuse `getQuizResults(userId)` and match the saved result type to the active type. Do not request student history for guests.
- Keep availability and history state separate. Ignore stale responses after unmount, user changes, or active-type changes; never show another user's continuation state.

| State | Announcement |
| --- | --- |
| Loading | Small status message; the rest of the homepage remains usable. |
| No active examination (`204`) | Hide the announcement. |
| Active, guest | Show the active type and a login link. |
| Active, no matching saved quiz | Show "Start RoboQuiz", linking to `/quiz`. |
| Active, matching saved quiz | Show "Continue to RoboExamination" and a secondary examination-results link. |
| Request failure | Show an error and retry action. A history failure must not hide a known active examination or imply no saved quiz exists. |

Treat malformed `200` availability responses as errors, not as disabled examinations. Use existing error logging/messages and accessible status text and controls.

Do not call the examination-assignment GET from the homepage: it creates an attempt. The examination page remains responsible for authoritative eligibility and submission checks.

## 2. Make quiz continuation explicit

Files: [Quiz.jsx](../../StudentsManager.Spa/src/components/Quiz/Quiz.jsx), [QuizResults.jsx](../../StudentsManager.Spa/src/components/Quiz/QuizResults.jsx). Keep the existing save logic and identity guards in [useQuiz.js](../../StudentsManager.Spa/src/components/Quiz/useQuiz.js).

- Remove the automatic navigation effect and redirect-only refs/imports.
- Keep automatic saving after the final answer. Leave the score, points, correct-answer count, type, variant, and answer summary visible until the student chooses to leave.
- Replace the "redirecting" message with a save confirmation. Add "Continue to RoboExamination", enabled only when saving returns `saved` or confirms an existing result as `duplicate`.
- While saving, show the saving state and disable continuation. On failure, preserve the results and answers and keep the existing retry action.
- Navigate to `/robo-examination` only on an explicit click, without replacing the quiz route in history.
- For `exhausted`, retain the explanation and add the same explicit continuation action. Do not redirect or fabricate a new quiz result.

Keep the destination's `disabled`, `quizRequired`, `none`, `submitted`, and retryable-error states. A settings change must show the current state, not trigger a redirect loop. Reloading need not restore the in-memory answer summary; saved history and homepage continuation remain available.

## 3. Keep database-backed delivery and fix the concrete gaps

The existing creation and assignment flow already supports mixed question types. The reported delivery failure has not been reproduced, so do not assume it requires a backend rewrite.

### Creation and assignment

Files: [RoboExaminationInput.cs](../../StudentsManager.Mvc/Domain/Inputs/RoboExaminations/RoboExaminationInput.cs), [RoboExaminationController.cs](../../StudentsManager.Mvc/Controllers/RoboExaminationController.cs), [RoboExaminationService.cs](../../StudentsManager.Mvc/Services/RoboExaminations/RoboExaminationService.cs).

- Keep `POST /api/RoboExamination`, protected by the server-configured `X-Admin-Key`, returning `201` with the created ID, type, and variant. Never put the key in the SPA.
- Preserve existing validation: configured type, nonblank variant, unique positive question IDs, nonblank text, supported question types, length limits, and unique `(type, variant)` definitions.
- Fix the validation gap for explicit `options: null` on text questions: reject it with the existing structured validation response before storage. Omitted text options must still become an empty list. Option questions require at least two distinct nonblank choices.
- Keep `expectedAnswer` optional and server-only. The following request must work without it:

```json
{
  "type": "First",
  "variant": "variantC",
  "questions": [
    {
      "id": 1,
      "text": "Which array method returns a new array?",
      "type": "options",
      "options": [
        { "text": "map" },
        { "text": "push" },
        { "text": "pop" },
        { "text": "splice" }
      ]
    },
    {
      "id": 2,
      "text": "Explain the difference between let and const.",
      "type": "text"
    }
  ]
}
```

- Keep `GET /api/RoboExamination?userId={id}` and its response: `attemptId`, `roboExaminationId`, `type`, `variant`, and ordered `questions`. Creation accepts option objects; delivery intentionally returns option strings and `[]` for text questions.
- Preserve authored question IDs, text, order, and choices through storage and delivery. Use the assigned database definition, never quiz history or bundled fallback questions.
- If content is unavailable, trace the active setting, same-type quiz prerequisite, matching variants, and existing assignment before changing delivery logic. Fix the failing boundary without redesigning the schema or assignment policy.

### Rendering and submission

Files: [Chatbot.jsx](../../StudentsManager.Spa/src/components/Chatbot/Chatbot.jsx), [useRoboExamination.js](../../StudentsManager.Spa/src/components/Chatbot/useRoboExamination.js), [ChatbotQuestion.jsx](../../StudentsManager.Spa/src/components/Chatbot/ChatbotQuestion.jsx), [ChatbotTextInput.jsx](../../StudentsManager.Spa/src/components/Chatbot/ChatbotTextInput.jsx), [roboExaminationService.js](../../StudentsManager.Spa/src/services/roboExaminationService.js).

- Render every delivered question in order, retaining the existing introduction, finish step, and string-to-option-object adapter. Do not rename the Chatbot feature or add alternative payload formats.
- Render question text literally, including programming syntax; do not interpret authored text as HTML.
- Add a synchronous transition guard in the examination hook so repeated clicks/Enter cannot record multiple answers or schedule multiple advances. Clear pending transitions on dismissal, reload, and unmount.
- Key the text input by attempt and question ID so each question starts with fresh value and submission state.
- Keep submission as `{ userId, attemptId, answers: [{ questionId, answer }] }`, using the assigned attempt and exact delivered option text. Preserve server validation requiring every assigned question exactly once.
- Map only `alreadySubmitted` to the duplicate/saved UI. Do not treat every `409` as a duplicate; surface other errors such as `evaluationSuperseded` and keep results/status reachable.
- Preserve answers on submission failure and retain retry. Saved answers with pending or failed evaluation must remain saved and accessible through results, without asking the student to retake the examination.

## 4. Update related documentation

- Update [README.md](../../README.md) with the homepage entry point, explicit quiz continuation, creation example, and existing API prerequisites.
- Replace the superseded navigation and automatic-redirect requirements in [roboexamination.prompt.md](roboexamination.prompt.md).
- Document that an enabled type needs both quiz and examination content; the existing `First / A` seed does not supply every type. Do not overwrite definitions or delete student attempts/results to make new content appear.

Resulting journey: homepage announcement -> quiz -> visible saved result -> explicit continuation -> assigned database examination -> saved answers and results. Returning students with a matching saved quiz can continue directly from the homepage.
