import { useEffect } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { useQuiz } from './useQuiz';
import QuizProgress from './QuizProgress';
import QuizQuestion from './QuizQuestion';
import QuizResults from './QuizResults';
import QuizReview from './QuizReview';
import QuizStart from './QuizStart';

function Quiz() {
    const navigate = useNavigate();
    const {
        status,
        loadError,
        quiz,
        quizStarted,
        shuffledQuestions,
        currentQuestionIndex,
        currentQuestion,
        selectedAnswer,
        answers,
        isAnimating,
        quizCompleted,
        reviewMode,
        scoreDetails,
        feedbackState,
        submitStatus,
        submitError,
        history,
        historyLoading,
        historyError,
        questionCardRef,
        handleStart,
        handleAnswerClick,
        handleShowResults,
        retrySubmit,
        retryLoad,
    } = useQuiz();

    // The report is taller than the results card, so keep each screen starting at the top.
    useEffect(() => {
        if (reviewMode || quizCompleted) window.scrollTo({ top: 0 });
    }, [reviewMode, quizCompleted]);

    if (status === 'loading') {
        return <div className="quiz-status" role="status">Зареждане на теста...</div>;
    }

    if (status === 'disabled') {
        return <div className="quiz-status" role="status">Няма активен изпит.</div>;
    }

    if (status === 'none') {
        return <div className="quiz-status" role="status">Няма налични тестове.</div>;
    }

    if (status === 'exhausted') {
        return (
            <div className="quiz-status" role="status">
                <p>Вече сте предали RoboQuiz за този изпит.</p>
                <button type="button" className="soge-btn" onClick={() => navigate('/robo-examination')}>
                    Continue to RoboExamination
                </button>
                <Link className="soge-btn" to="/robo-examination/results">
                    View examination results
                </Link>
            </div>
        );
    }

    if (status === 'error') {
        return (
            <div className="quiz-status" role="alert">
                <p>{loadError}</p>
                <button type="button" className="soge-btn soge-btn--primary" onClick={retryLoad}>
                    Опитай отново
                </button>
            </div>
        );
    }

    if (!quizStarted) {
        return (
            <QuizStart
                totalQuestions={shuffledQuestions.length}
                type={quiz.type}
                variant={quiz.variant}
                onStart={handleStart}
                history={history}
                historyLoading={historyLoading}
                historyError={historyError}
            />
        );
    }

    if (reviewMode) {
        return <QuizReview answers={answers} onShowResults={handleShowResults} />;
    }

    if (quizCompleted) {
        return (
            <QuizResults
                scoreDetails={scoreDetails}
                totalQuestions={shuffledQuestions.length}
                answers={answers}
                submitStatus={submitStatus}
                submitError={submitError}
                onRetrySubmit={retrySubmit}
                onContinue={() => navigate('/robo-examination')}
                canContinue={submitStatus === 'saved' || submitStatus === 'duplicate'}
                quizType={quiz.type}
                quizVariant={quiz.variant}
            />
        );
    }

    return (
        <div className="soge-young-chatbot" ref={questionCardRef}>
            <QuizProgress
                currentIndex={currentQuestionIndex}
                totalQuestions={shuffledQuestions.length}
                answers={answers}
            />
            <QuizQuestion
                question={currentQuestion}
                selectedAnswer={selectedAnswer}
                onAnswerClick={handleAnswerClick}
                feedbackState={feedbackState}
                isAnimating={isAnimating}
            />
        </div>
    );
}

export default Quiz;
