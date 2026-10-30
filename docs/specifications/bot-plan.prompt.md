# Plan: RoboExamination / Chatbot — open-text questions only

## Target content schema
```json
[
  { "id": "q1", "text": "What is a closure?", "type": "text", "expectedAnswer": "optional, server-side only" }
]
```
- `id`: string, required, 1–50 chars, unique (ordinal). Legacy numeric ids (`1`) still load (Newtonsoft coerces numbers to strings).
- `text`: required, 1–1000 chars.
- `type`: must be `"text"`.
- `expectedAnswer`: optional, ≤ 2000 chars (kept — improves AI grading, never sent to client).
- `options` removed from the active question contract; migration-only types can read legacy JSON.

## Backend (StudentsManager.Mvc)
1. `RoboExaminationQuestion`: `Id` → `string`; remove `Options`.
2. Delete `RoboExaminationOption`.
3. `RoboExaminationInput.Validate`: drop all options rules; type must be `text`; id non-empty/≤50/unique.
4. `RoboExaminationAnswerInput.QuestionId` → `[Required] string`.
5. `RoboExaminationQuestionView`: `Id` → `string`; remove `Options`; `Type` always `"text"`.
6. `RoboExaminationAnswerView.QuestionId` → `string`.
7. `RoboExaminationService`: drop option-answer validation, `StoredAnswer.QuestionId` → `string`, ordinal id comparisons, simplified evaluation items.
8. `RoboExaminationEvaluationItem` → `(Question, ExpectedAnswer, Answer)`; evaluator prompt says open-text answers.
9. `RoboExaminationSeeder` (First/A): six open-text questions with ids `q1`..`q6`.
10. Upgrade every existing variant automatically during startup seeding, before serving requests (not just fresh databases):
    - Convert legacy choices into plain-text context with an open-response instruction; do not discard the information needed to answer "which of the following" prompts.
    - Preserve existing question IDs and expected answers. Numeric IDs deserialize as strings without adding a `q` prefix, so in-progress and historical submissions still match.
    - Persist only definition changes, in one transactional save. Keep assignments, submitted answers, grades, and variant identities unchanged.
    - Remove the legacy options field, including empty arrays on text questions. Running the upgrade again is a no-op.
    - Fail explicitly on malformed legacy content; never silently discard it. Back up the DB before deployment.

## Frontend (StudentsManager.Spa)
1. `Chatbot.jsx`: remove the `options` question branch; every question step renders the text answer box. Intro keeps Да/Не buttons.
2. `ChatbotTextInput.jsx`: `<input>` → multi-line `<textarea>` (6 rows, vertical resize, 2000-char counter). Enter = newline, Ctrl/Cmd+Enter = submit.
3. `index.css`: replace underline single-line input styles with a full-width bordered answer box. Give the chatbot card a stable responsive width, preserve question/context line breaks, and keep `pre-wrap` for answers on the results page.
4. Pre-existing bug fixed (blocked the input from being seen): `ChatbotPage` animated `.soge-young-chatbot` on mount while the exam was still loading, so the card stayed `visibility: hidden`. The entrance animation now runs from a callback ref in `Chatbot.jsx` when the card mounts.

## Docs
- README instructor `curl` example and `docs/specifications/roboexamination.prompt.md` content schema → text-only.

## Validation and scope
- No new automated tests or test-suite execution (per request). Adapt existing fixtures to string IDs/text-only questions and remove obsolete option-validation cases so the existing projects still compile.
- Build MVC and its dependent test project, and build/lint the SPA.
- Verify the startup conversion against a disposable local database, including persistence, repeat execution, historical answers, and assigned attempts. Do not connect to or change production data.
- Keep RoboQuiz and the legacy Chatbot endpoints unchanged.

## Verification completed
- MVC and its dependent test project compile; SPA production build succeeds.
- Chatbot implementation lint passes. Full SPA lint still reports the unrelated existing `react-refresh/only-export-components` issue in `src/context/AuthContext.jsx:6`.
- Disposable SQL Server verification confirmed fresh seeding, persisted upgrades for First/A and instructor variants, unchanged repeat startup, stable assigned IDs, unchanged historical answers/timestamps, successful free-text submission, and historical re-evaluation with the original answer key. Invalid legacy content failed explicitly. The disposable database was deleted.
- Browser verification with a local mock API confirmed six rows, vertical resizing, 2000-character limit, full-width layout at desktop/mobile sizes, Enter inserting a newline, Ctrl+Enter advancing, and final submission preserving multiline text and both legacy/string IDs.
