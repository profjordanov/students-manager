import { useState, useEffect, useCallback, useRef } from 'react';
import { Link } from 'react-router-dom';
import { useAuth } from '../../context/AuthContext';
import { getErrorMessage } from '../../services/apiConfig';
import { getEnabledExamination } from '../../services/roboQuizService';
import { getExaminationResults } from '../../services/examinationResultsService';
import ChatbotResultCard from './ChatbotResultCard';

function ChatbotResults() {
    const { userId } = useAuth();
    const [results, setResults] = useState(null);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState('');
    const requestIdRef = useRef(0);

    const fetchResults = useCallback(async () => {
        if (!userId) return;
        const requestId = ++requestIdRef.current;
        const requestedUserId = userId;
        setIsLoading(true);
        setError('');
        try {
            const availability = await getEnabledExamination();
            if (requestId !== requestIdRef.current) return;
            const examination = availability.examination ?? null;
            const data = examination
                ? await getExaminationResults(requestedUserId, examination.type)
                : { quizResults: [], roboExaminationResults: [] };
            if (requestId !== requestIdRef.current) return;
            setResults({ ...data, examination, userId: requestedUserId });
        } catch (err) {
            if (requestId !== requestIdRef.current) return;
            setError(getErrorMessage(err, 'Резултатите не могат да бъдат заредени.'));
        } finally {
            if (requestId === requestIdRef.current) {
                setIsLoading(false);
            }
        }
    }, [userId]);

    useEffect(() => {
        let active = true;
        Promise.resolve().then(() => {
            if (active) void fetchResults();
        });
        return () => {
            active = false;
            requestIdRef.current += 1;
        };
    }, [fetchResults]);

    const currentResults = results?.userId === userId ? results : null;
    const loading = isLoading || (!error && !currentResults);

    if (!userId) {
        return (
            <div className="soge-results center">
                <p>Влезте в профила си, за да видите резултатите.</p>
            </div>
        );
    }

    return (
        <div className="soge-results">
            <div className="soge-results-header">
                <h2>Резултати от изпита{currentResults?.examination ? ` — ${currentResults.examination.type}` : ''}</h2>
                <button
                    type="button"
                    className="soge-btn"
                    onClick={fetchResults}
                    disabled={loading}
                >
                    {loading ? 'Зареждане…' : 'Обнови'}
                </button>
            </div>

            {loading && (
                <div className="soge-results-loading center" aria-live="polite">
                    Зареждане на резултатите…
                </div>
            )}
            {!loading && error && <p className="soge-error" role="alert">{error}</p>}
            {!loading && !error && !currentResults.examination && (
                <p className="soge-results-empty">Няма активен изпит.</p>
            )}

            {!loading && !error && currentResults.examination && (
                <>
                    <section aria-label="RoboQuiz">
                        <h3>RoboQuiz</h3>
                        {currentResults.quizResults.length === 0 ? (
                            <>
                                <p className="soge-results-empty">Все още няма предаден RoboQuiz за този изпит.</p>
                                <Link className="soge-btn" to="/quiz">Към RoboQuiz</Link>
                            </>
                        ) : currentResults.quizResults.map((result) => (
                            <div className="soge-result-card" key={result.id}>
                                <div className="soge-result-meta">
                                    <span>{new Date(result.completedAtUtc).toLocaleString()}</span>
                                    <span>{result.type} · {result.variant}</span>
                                </div>
                                <div className="soge-result-grade">
                                    Резултат: <strong>{result.scorePercentage}%</strong>
                                </div>
                            </div>
                        ))}
                    </section>
                    <section aria-label="RoboExamination">
                        <h3>RoboExamination</h3>
                        {currentResults.roboExaminationResults.length === 0 ? (
                            <>
                                <p className="soge-results-empty">Все още няма предаден RoboExamination за този изпит.</p>
                                {currentResults.quizResults.length > 0 && (
                                    <Link className="soge-btn" to="/robo-examination">Продължи към RoboExamination</Link>
                                )}
                            </>
                        ) : currentResults.roboExaminationResults.map((item) => (
                            <ChatbotResultCard key={item.attemptId} {...item} isLatest />
                        ))}
                    </section>
                </>
            )}
        </div>
    );
}

export default ChatbotResults;
