import { useState, useEffect, useLayoutEffect, useRef, useCallback } from 'react';
import gsap from 'gsap';
import { useAuth } from '../../context/AuthContext';
import { getErrorMessage } from '../../services/apiConfig';
import {
    getEnabledExamination,
    getNextQuiz,
    getQuizResults,
    submitQuizResult,
} from '../../services/roboQuizService';
import {
    FEEDBACK_DELAY,
    SLIDE_DURATION,
    SLIDE_OFFSET,
    calculateQuizResult,
    shuffleArray,
} from './quizConstants';

export function useQuiz() {
    const { userId } = useAuth();
    const [status, setStatus] = useState('loading');
    const [loadError, setLoadError] = useState('');
    const [quiz, setQuiz] = useState(null);
    const [shuffledQuestions, setShuffledQuestions] = useState([]);
    const [quizStarted, setQuizStarted] = useState(false);
    const [currentQuestionIndex, setCurrentQuestionIndex] = useState(0);
    const [answers, setAnswers] = useState([]);
    const [isAnimating, setIsAnimating] = useState(false);
    const [quizCompleted, setQuizCompleted] = useState(false);
    const [reviewMode, setReviewMode] = useState(false);
    const [scoreDetails, setScoreDetails] = useState(() => calculateQuizResult([], 0));
    const [feedbackState, setFeedbackState] = useState(null);
    const [submitStatus, setSubmitStatus] = useState('idle');
    const [submitError, setSubmitError] = useState('');
    const [history, setHistory] = useState([]);
    const [historyLoading, setHistoryLoading] = useState(false);
    const [historyError, setHistoryError] = useState('');

    const questionCardRef = useRef(null);
    const feedbackTimerRef = useRef(null);
    const requestIdRef = useRef(0);
    const historyRequestIdRef = useRef(0);
    const mountedRef = useRef(false);
    const currentUserIdRef = useRef(userId);
    const currentQuizIdRef = useRef(quiz?.id ?? null);
    useLayoutEffect(() => {
        currentUserIdRef.current = userId;
        currentQuizIdRef.current = quiz?.id ?? null;
    }, [quiz?.id, userId]);

    const loadHistory = useCallback(async () => {
        if (!userId) return;

        const requestId = ++historyRequestIdRef.current;
        const requestedUserId = userId;
        setHistoryLoading(true);
        setHistoryError('');
        try {
            const results = await getQuizResults(requestedUserId);
            if (!mountedRef.current
                || requestId !== historyRequestIdRef.current
                || currentUserIdRef.current !== requestedUserId) return;
            setHistory(results.map((result) => ({
                id: result.id,
                date: result.completedAtUtc,
                percentage: result.scorePercentage,
                type: result.type,
                variant: result.variant,
            })));
        } catch (error) {
            if (!mountedRef.current
                || requestId !== historyRequestIdRef.current
                || currentUserIdRef.current !== requestedUserId) return;
            console.error('Quiz history loading failed:', error);
            setHistoryError('Историята не може да бъде заредена в момента.');
        } finally {
            if (requestId === historyRequestIdRef.current
                && currentUserIdRef.current === requestedUserId) setHistoryLoading(false);
        }
    }, [userId]);

    const loadNextQuiz = useCallback(async () => {
        const requestId = ++requestIdRef.current;
        const requestedUserId = userId;
        setStatus('loading');
        setLoadError('');
        setQuizStarted(false);

        try {
            if (!requestedUserId) {
                setStatus('error');
                setLoadError('Необходимо е да влезете в профила си.');
                return;
            }

            const examinationResult = await getEnabledExamination();
            if (requestId !== requestIdRef.current || currentUserIdRef.current !== requestedUserId) return;
            if (examinationResult.status === 'disabled') {
                setQuiz(null);
                setStatus('disabled');
                return;
            }

            const quizResult = await getNextQuiz(requestedUserId);
            if (requestId !== requestIdRef.current || currentUserIdRef.current !== requestedUserId) return;

            if (quizResult.status !== 'ready') {
                setQuiz(null);
                setStatus(quizResult.status);
                return;
            }

            setQuiz(quizResult.quiz);
            setShuffledQuestions(shuffleArray(quizResult.quiz.questions));
            setCurrentQuestionIndex(0);
            setAnswers([]);
            setIsAnimating(false);
            setQuizCompleted(false);
            setReviewMode(false);
            setScoreDetails(calculateQuizResult([], quizResult.quiz.questions.length));
            setFeedbackState(null);
            setSubmitStatus('idle');
            setSubmitError('');
            setStatus('ready');
        } catch (error) {
            if (requestId !== requestIdRef.current || currentUserIdRef.current !== requestedUserId) return;
            console.error('Quiz loading failed:', error);
            setQuiz(null);
            setLoadError(getErrorMessage(error, 'Тестът не може да бъде зареден в момента.'));
            setStatus('error');
        }
    }, [userId]);

    const persistResult = useCallback(async (scorePercentage) => {
        if (!userId || !quiz) return;

        const requestId = requestIdRef.current;
        const submittedUserId = userId;
        const submittedQuizId = quiz.id;
        const isCurrentSubmission = () => mountedRef.current
            && requestId === requestIdRef.current
            && currentUserIdRef.current === submittedUserId
            && currentQuizIdRef.current === submittedQuizId;

        setSubmitStatus('submitting');
        setSubmitError('');
        try {
            const result = await submitQuizResult({
                userId: submittedUserId,
                roboQuizId: submittedQuizId,
                scorePercentage,
            });
            if (!isCurrentSubmission()) return;
            setSubmitStatus(result.status);
            void loadHistory();
        } catch (error) {
            if (!isCurrentSubmission()) return;
            console.error('Quiz result save failed:', error);
            setSubmitError(getErrorMessage(error, 'Резултатът не може да бъде записан.'));
            setSubmitStatus('failed');
        }
    }, [loadHistory, quiz, userId]);

    useEffect(() => {
        let active = true;
        mountedRef.current = true;
        const questionCard = questionCardRef.current;

        Promise.resolve().then(() => {
            if (!active) return;
            void loadNextQuiz();
            void loadHistory();
        });

        return () => {
            active = false;
            mountedRef.current = false;
            requestIdRef.current += 1;
            historyRequestIdRef.current += 1;
            if (feedbackTimerRef.current) clearTimeout(feedbackTimerRef.current);
            if (questionCard) gsap.killTweensOf(questionCard);
        };
    }, [loadHistory, loadNextQuiz]);

    useEffect(() => {
        if (!quizStarted || !questionCardRef.current) return undefined;

        const tween = gsap.fromTo(
            questionCardRef.current,
            { x: SLIDE_OFFSET, autoAlpha: 0 },
            {
                x: 0,
                autoAlpha: 1,
                duration: SLIDE_DURATION,
                ease: 'power2.out',
                onComplete: () => setIsAnimating(false),
            }
        );

        return () => tween.kill();
    }, [currentQuestionIndex, quizStarted]);

    const handleStart = useCallback(() => {
        setQuizStarted(true);
    }, []);

    const handleAnswerClick = useCallback((optionId) => {
        if (isAnimating) return;

        const currentQuestion = shuffledQuestions[currentQuestionIndex];
        const selectedOption = currentQuestion.options.find((option) => option.id === optionId);
        const correctOption = currentQuestion.options.find((option) => option.id === currentQuestion.correctAnswer);
        const isCorrect = currentQuestion.correctAnswer === optionId;
        const nextAnswers = [
            ...answers,
            {
                questionId: currentQuestion.id,
                question: currentQuestion.question,
                selectedId: optionId,
                selectedText: selectedOption?.text || '',
                correctId: currentQuestion.correctAnswer,
                correctText: correctOption?.text || '',
                options: currentQuestion.options,
                isCorrect,
            },
        ];

        setAnswers(nextAnswers);
        setFeedbackState(isCorrect ? 'correct' : 'incorrect');
        setIsAnimating(true);

        feedbackTimerRef.current = setTimeout(() => {
            if (!questionCardRef.current) return;

            gsap.to(questionCardRef.current, {
                x: -SLIDE_OFFSET,
                autoAlpha: 0,
                duration: SLIDE_DURATION,
                ease: 'power2.in',
                onComplete: () => {
                    const nextIndex = currentQuestionIndex + 1;
                    if (nextIndex >= shuffledQuestions.length) {
                        const result = calculateQuizResult(nextAnswers, shuffledQuestions.length);
                        setScoreDetails(result);
                        setQuizCompleted(false);
                        setReviewMode(true);
                        setIsAnimating(false);
                        persistResult(result.percentage);
                    } else {
                        gsap.set(questionCardRef.current, { x: SLIDE_OFFSET, autoAlpha: 0 });
                        setFeedbackState(null);
                        setCurrentQuestionIndex(nextIndex);
                    }
                },
            });
        }, FEEDBACK_DELAY);
    }, [answers, currentQuestionIndex, isAnimating, persistResult, shuffledQuestions]);

    const handleShowResults = useCallback(() => {
        setReviewMode(false);
        setQuizCompleted(true);
    }, []);

    const handleShowReview = useCallback(() => {
        setQuizCompleted(false);
        setReviewMode(true);
    }, []);

    const handleRestart = useCallback(() => {
        if (feedbackTimerRef.current) clearTimeout(feedbackTimerRef.current);
        if (questionCardRef.current) gsap.killTweensOf(questionCardRef.current);
        setAnswers([]);
        setCurrentQuestionIndex(0);
        setIsAnimating(false);
        setQuizCompleted(false);
        setReviewMode(false);
        setFeedbackState(null);
        void loadNextQuiz();
    }, [loadNextQuiz]);

    const currentQuestion = shuffledQuestions[currentQuestionIndex];
    const selectedAnswer = answers.length > currentQuestionIndex
        ? answers[currentQuestionIndex].selectedId
        : null;

    return {
        userId,
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
        handleShowReview,
        handleRestart,
        retrySubmit: () => persistResult(scoreDetails.percentage),
        retryLoad: loadNextQuiz,
    };
}
