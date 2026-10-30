import { QUIZ_HALF_POINT_PERCENTAGE, QUIZ_PASS_PERCENTAGE } from './quizConstants';

function formatHistoryDate(date) {
    if (!date) return 'последен опит';

    return new Intl.DateTimeFormat('bg-BG', {
        day: '2-digit',
        month: 'short',
        year: 'numeric',
        hour: '2-digit',
        minute: '2-digit',
    }).format(new Date(date));
}

function QuizStart({ totalQuestions, type, variant, onStart, history, historyLoading, historyError }) {
    return (
        <section className="quiz-start section-init-test">
            <div className="bg-image quiz-start__bg" aria-hidden="true" />
            <div className="section-box center">
                <div className="section-init-test-txt quiz-start__panel">
                    <h1 className="title-big white">
                        Готов ли си за <strong>теста?</strong>
                    </h1>
                    <p className="p-mid white regular quiz-start__copy">
                        Вариант {variant} ({type}) включва {totalQuestions} въпроса.
                        За 5 точки са необходими {QUIZ_PASS_PERCENTAGE}% верни отговори,
                        а резултат от поне {QUIZ_HALF_POINT_PERCENTAGE}% дава 2.5 точки.
                    </p>
                    <div className="quiz-start__stats" aria-label="Правила за оценяване">
                        <span><strong>{QUIZ_PASS_PERCENTAGE}%</strong> =  5 точки</span>
                        <span><strong>{QUIZ_HALF_POINT_PERCENTAGE}%</strong> = 2.5 точки</span>
                        <span><strong>{totalQuestions}</strong> въпроса</span>
                    </div>
                    <div className="btn-box quiz-start__actions">
                        <button type="button" className="btn-mid btn-red quiz-start__button" onClick={onStart}>
                            Започни теста
                        </button>
                    </div>
                    <div className="quiz-history" aria-live="polite">
                        <h2 className="quiz-history__title">Последни резултати</h2>
                        {historyLoading && <p className="quiz-history__status">Зареждане...</p>}
                        {!historyLoading && historyError && (
                            <p className="quiz-history__status">{historyError}</p>
                        )}
                        {!historyLoading && !historyError && history.length === 0 && (
                            <p className="quiz-history__status">Все още няма записани опити.</p>
                        )}
                        {!historyLoading && !historyError && history.length > 0 && (
                            <ul className="quiz-history__list">
                                {history.map((attempt) => (
                                    <li key={attempt.id} className="quiz-history__item">
                                        <span>{formatHistoryDate(attempt.date)}</span>
                                        <strong>{attempt.percentage}%</strong>
                                        <small>{attempt.type} · вариант {attempt.variant}</small>
                                    </li>
                                ))}
                            </ul>
                        )}
                    </div>
                </div>
            </div>
        </section>
    );
}

export default QuizStart;
