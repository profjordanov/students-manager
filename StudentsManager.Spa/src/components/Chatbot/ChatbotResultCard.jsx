function ChatbotResultCard({
    type,
    variant,
    submittedAtUtc,
    evaluationStatus,
    grade,
    feedback,
    answers,
    isLatest,
}) {
    const formattedDate = submittedAtUtc
        ? new Date(submittedAtUtc).toLocaleString()
        : 'Неизвестна дата';
    const cardClass = `soge-result-card${isLatest ? ' soge-result-card--latest' : ''}`;
    const statusText = evaluationStatus === 'Pending'
        ? 'Оценява се…'
        : evaluationStatus === 'Failed'
            ? 'Оценяването не бе успешно. Отговорите са запазени.'
            : null;

    const content = (
        <>
            <div className="soge-result-meta">
                <span className="soge-result-meta-date">{formattedDate}</span>
                <span>{type} · {variant}</span>
            </div>

            {statusText && (
                <p className={evaluationStatus === 'Failed' ? 'soge-error' : 'soge-result-processing'}
                    role={evaluationStatus === 'Failed' ? 'alert' : 'status'}>
                    {statusText}
                </p>
            )}
            {evaluationStatus === 'Succeeded' && (
                <>
                    <div className="soge-result-grade">
                        Оценка: <strong>{grade ?? '—'}</strong>
                    </div>
                    {feedback && (
                        <div className="soge-result-feedback">
                            <p>{feedback}</p>
                        </div>
                    )}
                </>
            )}
            <ul className="soge-result-qa ul-reset">
                {answers.map((item) => (
                    <li key={item.questionId} className="soge-result-qa-item">
                        <span className="soge-result-qa-q">{item.questionText}</span>
                        <span className="soge-result-qa-a">{item.answer}</span>
                    </li>
                ))}
            </ul>
        </>
    );

    if (isLatest) return <div className={cardClass}>{content}</div>;

    return (
        <details className={cardClass}>
            <summary className="soge-result-history-toggle">
                {formattedDate} — {type} · {variant}
                {evaluationStatus === 'Succeeded' ? ` · Оценка: ${grade}` : ` · ${statusText}`}
            </summary>
            <div className="soge-result-history-body">{content}</div>
        </details>
    );
}

export default ChatbotResultCard;
