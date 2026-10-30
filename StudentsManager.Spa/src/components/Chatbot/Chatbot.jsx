import { useCallback } from 'react';
import { Link } from 'react-router-dom';
import gsap from 'gsap';
import { useRoboExamination } from './useRoboExamination';
import ChatbotQuestion from './ChatbotQuestion';
import ChatbotOptions from './ChatbotOptions';
import ChatbotTextInput from './ChatbotTextInput';
import ChatbotForm from './ChatbotForm';

function Chatbot() {
    // The card only mounts after the examination loads, so animate it in on attach.
    const cardRef = useCallback((node) => {
        if (!node) return undefined;
        gsap.fromTo(node, { autoAlpha: 0, y: 40 }, { autoAlpha: 1, y: 0, duration: 0.9, ease: 'power2.out' });
        return () => gsap.killTweensOf(node);
    }, []);

    const {
        status,
        examination,
        currentStep,
        currentIndex,
        isTyping,
        isDismissed,
        submitStatus,
        submitError,
        submissionResult,
        handleTypingComplete,
        answer,
        dismiss,
        submit,
        retrySubmit,
        retryLoad,
    } = useRoboExamination();

    if (status === 'loading') {
        return <div className="quiz-status" role="status">Зареждане на изпита...</div>;
    }
    if (status === 'disabled') {
        return <div className="quiz-status" role="status">Няма активен изпит.</div>;
    }
    if (status === 'quizRequired') {
        return (
            <div className="quiz-status" role="status">
                <p>Първо решете RoboQuiz.</p>
                <Link className="soge-btn" to="/quiz">Към RoboQuiz</Link>
            </div>
        );
    }
    if (status === 'none') {
        return <div className="quiz-status" role="status">Няма налични изпити.</div>;
    }
    if (status === 'submitted') {
        return (
            <div className="quiz-status" role="status">
                <p>Вече сте предали изпита.</p>
                <Link className="soge-btn" to="/robo-examination/results">Виж резултата</Link>
            </div>
        );
    }
    if (status === 'error') {
        return (
            <div className="quiz-status" role="alert">
                <p>{submitError}</p>
                <button type="button" className="soge-btn soge-btn--primary" onClick={retryLoad}>
                    Опитай отново
                </button>
            </div>
        );
    }

    if (isDismissed) {
        return <div className="quiz-status" role="status">Можете да се върнете към изпита по-късно.</div>;
    }

    if (submitStatus === 'saved' || submitStatus === 'duplicate') {
        return (
            <div className="quiz-status" role="status">
                <p>
                    {submissionResult?.evaluationStatus === 'Failed'
                        ? 'Оценяването не бе успешно. Отговорите са запазени.'
                        : submitStatus === 'duplicate'
                            ? 'Отговорите вече са записани.'
                            : 'Отговорите са записани.'}
                </p>
                <Link className="soge-btn" to="/robo-examination/results">Виж резултата</Link>
            </div>
        );
    }

    if (!currentStep) return null;

    const isQuestionStep = currentStep.type !== 'intro' && currentStep.type !== 'finish';

    return (
        <div className="soge-young-chatbot" ref={cardRef}>
            <p className="soge-result-meta">
                Вариант {examination.variant} ({examination.type}). Неизпратените отговори не се запазват при презареждане.
            </p>
            <div className="soge-question">
                <ChatbotQuestion
                    key={`${examination.attemptId}-${currentIndex}`}
                    text={currentStep.text}
                    onTypingComplete={handleTypingComplete}
                />
            </div>
            <div className="soge-answer" style={{ opacity: isTyping ? 0 : 1 }}>
                {!isTyping && currentStep.type === 'intro' && (
                    <ChatbotOptions options={currentStep.options} onSelect={answer} />
                )}
                {!isTyping && isQuestionStep && (
                    <ChatbotTextInput
                        key={`${examination.attemptId}-${currentStep.id}`}
                        onSubmit={answer}
                    />
                )}
                {!isTyping && currentStep.type === 'finish' && (
                    <ChatbotForm
                        onConfirm={submit}
                        onCancel={dismiss}
                        isSubmitting={submitStatus === 'submitting'}
                        error={submitStatus === 'failed' ? submitError : ''}
                        showCancel={submitStatus !== 'failed'}
                    />
                )}
                {submitStatus === 'failed' && currentStep.type === 'finish' && (
                    <button
                        type="button"
                        className="soge-btn"
                        onClick={retrySubmit}
                        disabled={submitStatus === 'submitting'}
                    >
                        Опитай отново
                    </button>
                )}
            </div>
        </div>
    );
}

export default Chatbot;
