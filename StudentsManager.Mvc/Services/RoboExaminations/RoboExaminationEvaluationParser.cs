using Newtonsoft.Json;

namespace StudentsManager.Mvc.Services.RoboExaminations
{
    public static class RoboExaminationEvaluationParser
    {
        public static RoboExaminationEvaluation Parse(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw))
                throw new RoboExaminationEvaluationException("The evaluator returned an empty response.", raw);

            var content = raw.Trim();
            if (content.StartsWith("```", StringComparison.Ordinal))
            {
                var firstNewline = content.IndexOf('\n');
                var closingFence = content.LastIndexOf("```", StringComparison.Ordinal);
                if (firstNewline < 0 || closingFence <= firstNewline)
                    throw new RoboExaminationEvaluationException("The evaluator returned malformed fenced JSON.", raw);
                content = content[(firstNewline + 1)..closingFence].Trim();
            }

            EvaluationPayload? payload;
            try
            {
                payload = JsonConvert.DeserializeObject<EvaluationPayload>(content);
            }
            catch (JsonException exception)
            {
                throw new RoboExaminationEvaluationException("The evaluator response was not valid JSON.", raw, exception);
            }

            if (payload is null || payload.Grade is null or < 2 or > 6)
                throw new RoboExaminationEvaluationException("The evaluator grade must be between 2 and 6.", raw);
            if (string.IsNullOrWhiteSpace(payload.OverallFeedback))
                throw new RoboExaminationEvaluationException("The evaluator feedback cannot be empty.", raw);

            return new RoboExaminationEvaluation(payload.Grade.Value, payload.OverallFeedback.Trim(), raw);
        }

        private sealed class EvaluationPayload
        {
            [JsonProperty("grade")]
            public int? Grade { get; set; }

            [JsonProperty("overallFeedback")]
            public string? OverallFeedback { get; set; }
        }
    }
}
