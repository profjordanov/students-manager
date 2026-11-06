# Course tutor: RAG feasibility and implementation plan

## Conclusion

Yes. StudentsManager can add retrieval-augmented generation without replacing its .NET backend, React frontend, SQL Server database, or existing examination evaluator.

The recommended first feature is a course-material tutor. An ingestion pipeline prepares approved teaching documents; a query pipeline retrieves relevant passages and generates answers with verifiable source references. Pretrained embedding and language models are sufficient: custom model training, ML.NET, Python, an agent framework, and changes to grading are not prerequisites.

This is a plan only. No repository files, deployed services, application data, or cloud resources have been changed.

## Confirmed decisions

- React tutor from the first release, including real API authentication.
- Azure OpenAI for embeddings and generation; Azure AI Search for retrieval; retain SQL Server for application data.
- Separate frontend/backend origins, using bearer tokens rather than a same-origin cookie proxy.
- Keep access and refresh tokens in memory. A full reload requires login again. Logout clears local tokens; immediate server-side token revocation is outside this pilot.
- Single-question answers in English/Bulgarian, with a maximum question length of 2,000 characters.
- Only operator-approved, non-sensitive teaching materials: text PDFs, Markdown, and TXT.
- Operator-run ingestion; no instructor upload UI, OCR, archive crawling, student submissions, or persistent chat history.
- Answers must cite supporting sources. Insufficient evidence produces an explicit abstention, not an answer from general model knowledge.
- Disable tutor queries whenever either existing examination type is enabled.
- Configure model-output limits and request/usage quotas before live rollout.
- Leave RoboQuiz, RoboExamination grading, assignments, scores, and historical results unchanged.

## Repository evidence and implications

| Existing component | Implication |
| --- | --- |
| [.NET project](C:/GitHub/students-manager/StudentsManager.Mvc/StudentsManager.Mvc.csproj) and [Azure OpenAI evaluator](C:/GitHub/students-manager/StudentsManager.Mvc/Services/RoboExaminations/AzureOpenAiRoboExaminationEvaluator.cs) | OpenAI SDK and .NET integration already exist, but the evaluator receives question/answer data without retrieval. Add a separate tutor service; do not change its grading prompt or contract. |
| [Topic](C:/GitHub/students-manager/StudentsManager.Mvc/Domain/Entities/Topic.cs) and [topic seeds](C:/GitHub/students-manager/StudentsManager.Mvc/Services/Topics/TopicsSeeder.cs) | Topics have document/resource links and release dates. Some links are archives or placeholders, and some topic records lack a course association. They are candidate references, not an automatically ingestible or authorized corpus. |
| [UserCourse](C:/GitHub/students-manager/StudentsManager.Mvc/Domain/Entities/UserCourse.cs) and [database context](C:/GitHub/students-manager/StudentsManager.Mvc/Persistence/ManagerDbContext.cs) | Existing enrollment data can determine course access. SQL remains the authority for source publication, access, and active document versions. |
| [StorageService](C:/GitHub/students-manager/StudentsManager.Mvc/Services/Storage/StorageService.cs) | Its container-creation method enables public access. New RAG originals need a separate private-container path; do not silently change existing upload behavior. |
| [AuthService](C:/GitHub/students-manager/StudentsManager.Mvc/Services/Auth/AuthService.cs) and [AuthProvider](C:/GitHub/students-manager/StudentsManager.Spa/src/context/AuthProvider.jsx) | The backend signs in with Identity cookies, but the SPA currently treats a session-stored user ID as login state. A user ID alone must never authorize tutor access. |
| [Demo shortcut](C:/GitHub/students-manager/StudentsManager.Spa/src/pages/UserlessPage.jsx) | Preserve the legacy demo separately. It must not mint a token or unlock the tutor. |
| [Examination settings](C:/GitHub/students-manager/StudentsManager.Mvc/Services/CourseExaminations/ExaminationSettingsService.cs) | Reuse the existing enabled-examination query for the server-side tutor restriction. |
| [Compose configuration](C:/GitHub/students-manager/docker-compose.yml) | Local SQL Server is 2019. Keep vectors in Azure AI Search; no SQL upgrade or database replacement is required. |

No embedding client, vector-store integration, or local OpenAPI/Postman collection was found in the inspected application and contract locations.

## Architecture

### Ingestion pipeline

```text
Operator-approved files + course/topic manifest
  -> validate approval, format, size, course mapping, and release policy
  -> store versioned originals in a private Blob container
  -> extract text with page/heading provenance
  -> split into bounded, overlapping chunks
  -> generate Azure OpenAI embeddings
  -> upsert text, vectors, and filterable metadata into Azure AI Search
  -> publish the completed document version in SQL
```

- Implement explicit ingestion command mode in the existing .NET application, reusing DI services and exiting without starting the web server or running the ordinary startup seeding flow. Avoid a new microservice, scheduler, or Service Bus dependency for this pilot.
- Keep document ID, version, content hash, course/topic mapping, title, source locator, visibility, ingestion state, and actionable failure information in SQL.
- Keep chunk IDs, source version, page/heading or text offsets, text, embedding, course/topic, and release metadata in the index.
- Preserve code examples and document boundaries. Reject empty, encrypted, or image-only PDFs explicitly rather than publishing an empty successful import.
- Make reruns idempotent. Inspect individual indexing results; do not publish partially indexed versions. Failed jobs retain retryable state without duplicating chunks.
- On withdrawal, make the SQL source unavailable immediately, then remove its indexed chunks and original according to the retention policy. Search-index cleanup failures must remain visible and retryable.
- Use the same embedding deployment, dimensionality, and preprocessing version for ingestion and queries. A change requires a compatible rebuilt index, not mixing vector spaces.
- Use a small extraction dependency only after checking PDF support and licensing. Reuse the existing OpenAI, Azure Identity, and Blob SDKs; add Azure Search SDK support.

### Query pipeline

```text
React question + course/topic selection + bearer token
  -> authenticate and resolve the current user from claims
  -> verify active user, enrollment, tutor availability, input, and quotas
  -> embed the question
  -> hybrid keyword/vector retrieval with server-built access filters
  -> revalidate retrieved document versions and publication against SQL
  -> generate a grounded answer from bounded supporting passages
  -> validate citation IDs and recheck availability
  -> return the answer and server-resolved source references
```

- Hybrid search is appropriate for conceptual questions and exact programming terms. Start with five context passages; tune chunking and evidence acceptance on a development evaluation set.
- Build filters from verified enrollment and published course/topic metadata, including release dates. Never accept access filters or a trusted user ID from the caller.
- Apply access filters during retrieval, and recheck returned sources before sending any text to the model. Stale, withdrawn, unpublished, or mismatched-version results must not become model context or citations.
- A disabled tutor or active examination rejects the query before provider calls. Recheck availability before releasing an answer.
- Treat retrieved text and questions as untrusted data. No tool execution, arbitrary URL fetching, or prompt instructions from source documents.
- Citation IDs must refer to retrieved, authorized passages. Resolve titles, locations, and source-viewer links on the server; do not trust model-generated URLs.
- Citation existence does not prove factual support. Measure supporting evidence with instructor review and adversarial examples.
- Distinguish no supporting evidence from dependency failures. Do not mask outages with fabricated answers or success-shaped fallbacks.

## Authentication and compatibility

- Add ASP.NET Core Identity's built-in opaque bearer-token handler alongside the existing MVC cookie scheme. These tokens are not JWTs and are suitable for this single-backend pilot, not an OAuth/OIDC identity-provider replacement.
- Add narrowly scoped token login, refresh, and current-user endpoints using existing accounts and Identity validation. Do not expose the full Identity endpoint bundle, particularly registration/reset endpoints, merely to obtain token support.
- Preserve the existing `POST /api/login` request/response and Razor cookie behavior. Migrate the SPA login path to the additive bearer endpoints.
- Use an explicit bearer-only authorization policy for tutor endpoints. Reject missing/expired tokens, soft-deleted users, and unenrolled users.
- Keep tokens in memory, attach them through a dedicated API client, coordinate refresh attempts, and clear tokens and tutor answers on logout/account change. Never log tokens or persist them in local/session storage or the PWA cache.
- Preserve the existing user ID only for legacy features that still require it; it does not establish a trusted session. Keep demo state distinct from authenticated state.
- Logout is local token removal, not immediate revocation. Validate user state and security-stamp changes on refresh; bound access-token lifetime and document the residual validity window.
- Configure persistent, protected ASP.NET Data Protection keys for deployments/restarts and shared keys if the backend is scaled out.
- Add an origin allowlist policy for the new auth/tutor routes and permit the Authorization header. Preserve legacy CORS behavior outside this scope, and verify middleware ordering so the old global policy does not bypass the new policy.
- Broader remediation of existing caller-supplied-identity endpoints is separate work. This feature must not rely on their weaker identity model.

## Planned API contract

Capture the following in a local authoritative OpenAPI document during implementation. Generate a small runnable Postman collection from it, maintaining assertions/examples explicitly; no workspace publication is required.

| Operation | Contract |
| --- | --- |
| `POST /api/auth/token` | Existing account credentials -> bearer token response. No registration. Rate-limit failed authentication and avoid account-disclosing errors. |
| `POST /api/auth/refresh` | Refresh token -> renewed token response; invalid tokens or inactive users return 401. |
| `GET /api/auth/me` | Bearer-authenticated identity for the SPA; no caller-supplied identity. |
| `GET /api/tutor/status` | Authenticated availability and accessible published course/topic selections; communicate examination/feature-disabled state. |
| `POST /api/tutor/ask` | `{ courseId, topicId?, question }`; question required, trimmed, 1-2,000 characters. No `userId`, conversation history, or arbitrary search-filter input. |
| `GET /api/tutor/sources/{sourceId}?version=...&chunkId=...` | Authorized source/version metadata and the cited excerpt; verify the chunk belongs to that published version and recheck enrollment and tutor availability. No direct public private-Blob URL. |

Tutor response:

```json
{
  "status": "answered",
  "answer": "A concise answer supported by the cited passages.",
  "citations": [
    {
      "sourceId": "server-resolved-source-id",
      "version": "source-version",
      "title": "Approved teaching document",
      "page": 3,
      "section": null,
      "chunkId": "retrieved-chunk-id"
    }
  ]
}
```

- `answered` requires a nonempty answer and at least one valid supporting citation.
- `insufficientEvidence` returns `answer: null` and `citations: []`; the UI displays an explicit no-evidence message.
- Follow the repository's `{ message, code }` error convention for new feature errors: 400 validation, 401 authentication, 403 enrollment/examination restriction, 404 unavailable source, 429 quota exceeded, 502 invalid provider output, and 503 disabled/unavailable dependency.
- Include bounded retries only for appropriate transient upstream failures, cancellation, and explicit error reporting. Do not automatically replay question submissions after an ambiguous failure.
- The source viewer uses the authenticated client, not a bare browser link that loses its Authorization header. Render answers/excerpts safely without executing embedded HTML.

## Implementation todos

1. **Defining contracts, corpus, and configuration**
   - Add the new auth/tutor contract and examples; define the reviewed pilot corpus and evaluation questions.
   - Define dedicated RAG/model/search settings, with the feature disabled by default.
   - Require explicit ingestion limits, generation/context limits, per-user rate limits, and application usage quotas before enabling live access. Select Azure region, model deployments, and service tier after checking availability and budget.
   - Keep new AI settings separate from the evaluator's existing ServiceBus-named settings.
2. **Adding scoped bearer authentication**
   - Extend [dependency registration](C:/GitHub/students-manager/StudentsManager.Mvc/Configurations/DependenciesConfiguration.cs) and [startup](C:/GitHub/students-manager/StudentsManager.Mvc/Program.cs); add narrow auth endpoints and tests.
   - Update [SPA auth state](C:/GitHub/students-manager/StudentsManager.Spa/src/context/AuthProvider.jsx), [login page](C:/GitHub/students-manager/StudentsManager.Spa/src/pages/LoginPage.jsx), and relevant [API clients](C:/GitHub/students-manager/StudentsManager.Spa/src/services).
   - Preserve demo and existing Razor/legacy API behavior, with explicit isolation from tutor authorization.
3. **Building durable document ingestion**
   - Add RAG document metadata, EF configuration/migration, private original storage, ingestion command mode, extraction/chunking, embeddings, and Azure Search indexing.
   - Implement observable import/retry/withdrawal and version-publication behavior.
4. **Implementing retrieval and tutor endpoints**
   - Add focused services under the existing [service structure](C:/GitHub/students-manager/StudentsManager.Mvc/Services), with auth/enrollment/exam checks, hybrid retrieval, bounded generation, source validation, and error mapping.
   - Reuse enrollment and examination settings; do not invoke or alter examination grading.
5. **Adding the React tutor**
   - Add a separate `/tutor` page and service; integrate [routing](C:/GitHub/students-manager/StudentsManager.Spa/src/App.jsx) and [navigation](C:/GitHub/students-manager/StudentsManager.Spa/src/components/Layout/Header.jsx).
   - Include verified-login gating, course/topic selection, input validation, pending/cancel states, citations, source viewer, no-evidence state, examination-disabled state, and retryable error messages.
   - Preserve the examination chatbot as a different feature. Do not cache authenticated tutor responses for offline use.
6. **Validating behavior and retrieval quality**
   - Add focused unit and HTTP integration tests for new behavior and relevant existing auth/assessment regressions.
   - Exercise provider failure scenarios against deterministic local HTTP stand-ins; keep normal tests independent of paid Azure calls.
   - Evaluate retrieval and grounded answers on held-out English/Bulgarian questions before any student rollout.
7. **Documenting and preparing guarded deployment**
   - Update the [README](C:/GitHub/students-manager/README.md) with architecture, bearer-session behavior, local setup, import/reindex/withdraw procedures, contracts, quotas, and rollout/rollback.
   - Deploy backend contracts before enabling the SPA tutor. Enable only after corpus approval, tests, Azure permissions/quota checks, and evaluation gates pass.
   - Roll back by disabling the feature; leave exams and existing application data untouched.

Dependencies: 2 and 3 depend on 1; 4 depends on 2 and 3; 5 depends on 2 and 4; 6 depends on 3, 4, and 5; 7 depends on 6. Write component tests alongside implementation rather than deferring all testing to step 6.

## Verification and release gates

- Authentication: token login/refresh, expiry, full-reload re-login, logout clearing, demo isolation, soft-deleted users, allowed/disallowed origins, and preservation of Razor cookies and legacy login responses.
- Access: missing/forged tokens, modified user IDs, cross-course requests, unreleased topics, withdrawn/stale source versions, and both enabled examination types. Rejected requests must not invoke generation.
- Ingestion: idempotent reruns, changed documents, partial index failures, resumable failures, empty/image-only PDFs, and removal. Published versions remain internally consistent.
- Output: enforce the response shape, 2,000-character boundary, valid source references, explicit abstention, quota enforcement, dependency timeouts/429/5xx, and prompt-injection examples.
- Run the smallest relevant .NET filters first, for example `dotnet test .\StudentsManager.Tests\StudentsManager.Tests.csproj --filter "FullyQualifiedName~Tutor|FullyQualifiedName~Rag|FullyQualifiedName~TokenAuth"`. Add targeted assessment regression filters where shared startup/auth changes warrant them.
- HTTP auth tests must exercise the actual middleware, not only directly instantiated controllers. Add the normal ASP.NET test-host dependency if needed. Database-backed tests use an explicit disposable test database, never the deployed database.
- Run `npm run lint` and `npm run build` in the SPA, followed by browser checks for login, refresh/reload, citations, examination blocking, and existing quiz/examination navigation.
- Use an instructor-reviewed held-out set with balanced English/Bulgarian answerable and unanswerable questions. Proposed acceptance gates: relevant evidence in the top five for at least 90% of answerable questions in each language; at least 95% of answered cases fully supported by cited text; at least 95% abstention on unanswerable cases.
- Require zero unauthorized-source disclosures in the access test set and 100% resolvable/authorized citations in returned answered responses. These are test gates, not guarantees that hallucinations are impossible.
- Measure latency and token/search usage; enable only within configured budgets. Enforce application usage ceilings, rather than relying solely on Azure billing alerts. Persist usage accounting needed for caps across restarts/instances.

## Costs, constraints, and later extensions

- Additional resources include an embedding deployment, Search index/service capacity, private source storage, and generation/search usage. Existing Azure grading access does not prove availability or quota for these resources.
- Confirm availability, data residency, model suitability for Bulgarian, and content rights before ingestion. Only reviewed non-sensitive teaching material and necessary question text may reach cloud models; exclude student identifiers, answers, grades, profiles, credentials, and logs.
- Use managed identity and least-privilege Azure roles where available. Separate index-writing operator permissions from web-app retrieval permissions; keep keys out of the SPA, repository, and telemetry.
- Do not enable the existing disabled Service Bus answer-processing service as part of this work.
- Later work could add teacher-reviewed question drafting, an instructor document-management UI, OCR, or conversational history. Grounded grading would require a separately approved design for rubrics, evidence snapshots, reproducibility, and human review.

## Research sources and verification limits

- [Azure AI Search hybrid retrieval](https://learn.microsoft.com/en-us/azure/search/hybrid-search-overview)
- [Vector-query filtering](https://learn.microsoft.com/en-us/azure/search/vector-search-filters)
- [Embedding generation and matching query/index models](https://learn.microsoft.com/en-us/azure/search/vector-search-how-to-generate-embeddings)
- [Azure OpenAI v1 API](https://learn.microsoft.com/en-us/azure/foundry/openai/api-version-lifecycle)
- [ASP.NET Core Identity bearer tokens, refresh, and logout limitations](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity-api-authorization?view=aspnetcore-10.0)

Repository source inspection and current Microsoft documentation support feasibility. Orbit discovery returned a partial Azure chat-completion integration, not an end-to-end RAG contract. Its suggested `api-version=v1` query parameter conflicts with Microsoft's v1 documentation; follow the official API contract rather than copying that suggestion.

The installed Postman CLI rejected `postman status`, so workspace/resource/Context Graph discovery was not used. No Postman setup was changed. A subsequent version probe did not complete and was stopped.

No build, application test, live Azure call, document download/import, or paid-resource provisioning was performed for this planning task. Actual corpus quality, resource availability, model quality, latency, and costs remain implementation-stage verification gates.
