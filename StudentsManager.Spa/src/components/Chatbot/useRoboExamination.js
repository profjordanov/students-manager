import { useCallback, useEffect, useLayoutEffect, useRef, useState } from 'react';
import { useAuth } from '../../context/AuthContext';
import { getErrorMessage } from '../../services/apiConfig';
import {
    getRoboExamination,
    submitRoboExamination,
} from '../../services/roboExaminationService';
import {
    FINISH_STEP,
    INTRO_STEP,
    STEP_TRANSITION_DELAY,
} from './roboExaminationConstants';

export function useRoboExamination() {
    const { userId } = useAuth();
    const [status, setStatus] = useState('loading');
    const [examination, setExamination] = useState(null);
    const [currentIndex, setCurrentIndex] = useState(0);
    const [answersByQuestionId, setAnswersByQuestionId] = useState({});
    const [isTyping, setIsTyping] = useState(true);
    const [isDismissed, setIsDismissed] = useState(false);
    const [submitStatus, setSubmitStatus] = useState('idle');
    const [submitError, setSubmitError] = useState('');
    const [submissionResult, setSubmissionResult] = useState(null);
    const loadRequestId = useRef(0);
    const submitRequestId = useRef(0);
    const transitionTimer = useRef(null);
    const transitionPending = useRef(false);
    const mountedRef = useRef(false);
    const userIdRef = useRef(userId);
    const attemptIdRef = useRef(null);

    useLayoutEffect(() => {
        userIdRef.current = userId;
    }, [userId]);

    const load = useCallback(async () => {
        const requestId = ++loadRequestId.current;
        if (transitionTimer.current) clearTimeout(transitionTimer.current);
        transitionTimer.current = null;
        transitionPending.current = false;
        setStatus('loading');
        setExamination(null);
        setSubmitStatus('idle');
        setSubmitError('');
        setSubmissionResult(null);
        setAnswersByQuestionId({});
        setCurrentIndex(0);
        setIsDismissed(false);
        attemptIdRef.current = null;

        if (!userId) {
            setStatus('error');
            setSubmitError('Необходимо е да влезете в профила си.');
            return;
        }

        try {
            const result = await getRoboExamination(userId);
            if (!mountedRef.current || requestId !== loadRequestId.current || userIdRef.current !== userId) return;
            if (result.status !== 'ready') {
                setStatus(result.status);
                return;
            }
            setExamination(result.examination);
            attemptIdRef.current = result.examination.attemptId;
            setStatus('ready');
        } catch (error) {
            if (!mountedRef.current || requestId !== loadRequestId.current || userIdRef.current !== userId) return;
            console.error('RoboExamination loading failed:', error);
            setSubmitError(getErrorMessage(error, 'Изпитът не може да бъде зареден в момента.'));
            setStatus('error');
        }
    }, [userId]);

    useEffect(() => {
        let active = true;
        mountedRef.current = true;
        Promise.resolve().then(() => {
            if (active) void load();
        });
        return () => {
            active = false;
            mountedRef.current = false;
            loadRequestId.current += 1;
            submitRequestId.current += 1;
            if (transitionTimer.current) clearTimeout(transitionTimer.current);
            transitionTimer.current = null;
            transitionPending.current = false;
        };
    }, [load]);

    const steps = examination
        ? [INTRO_STEP, ...examination.questions, FINISH_STEP]
        : [];
    const currentStep = steps[currentIndex] ?? null;

    const answer = useCallback((answerText) => {
        if (!currentStep || currentStep.type === 'finish' || transitionPending.current) return;
        transitionPending.current = true;
        setIsTyping(true);

        if (currentStep.type !== 'intro') {
            setAnswersByQuestionId((previous) => ({
                ...previous,
                [currentStep.id]: {
                    questionId: currentStep.id,
                    answer: answerText,
                },
            }));
        } else if (answerText === 'Не') {
            transitionTimer.current = setTimeout(() => {
                transitionTimer.current = null;
                transitionPending.current = false;
                setIsDismissed(true);
            }, STEP_TRANSITION_DELAY);
            return;
        }

        transitionTimer.current = setTimeout(() => {
            transitionTimer.current = null;
            transitionPending.current = false;
            setCurrentIndex((index) => index + 1);
        }, STEP_TRANSITION_DELAY);
    }, [currentStep]);

    const dismiss = useCallback(() => {
        if (transitionTimer.current) clearTimeout(transitionTimer.current);
        transitionTimer.current = null;
        transitionPending.current = false;
        setIsDismissed(true);
    }, []);

    const handleTypingComplete = useCallback(() => {
        setIsTyping(false);
    }, []);

    const submit = useCallback(async () => {
        if (!userId || !examination || !currentStep || currentStep.type !== 'finish') return;
        const answers = examination.questions.map((question) => answersByQuestionId[question.id]);
        if (answers.some((item) => !item)) {
            setSubmitError('Отговорете на всички въпроси преди предаване.');
            setSubmitStatus('failed');
            return;
        }

        const requestId = ++submitRequestId.current;
        const submittedUserId = userId;
        const submittedAttemptId = examination.attemptId;
        setSubmitStatus('submitting');
        setSubmitError('');

        try {
            const result = await submitRoboExamination({
                userId: submittedUserId,
                attemptId: submittedAttemptId,
                answers,
            });
            if (!mountedRef.current
                || requestId !== submitRequestId.current
                || userIdRef.current !== submittedUserId
                || attemptIdRef.current !== submittedAttemptId) return;
            setSubmitStatus(result.status);
            setSubmissionResult(result.result ?? null);
        } catch (error) {
            if (!mountedRef.current
                || requestId !== submitRequestId.current
                || userIdRef.current !== submittedUserId
                || attemptIdRef.current !== submittedAttemptId) return;
            console.error('RoboExamination submission failed:', error);
            setSubmitError(getErrorMessage(error, 'Отговорите не могат да бъдат записани.'));
            setSubmitStatus('failed');
        }
    }, [answersByQuestionId, currentStep, examination, userId]);

    return {
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
        retrySubmit: submit,
        retryLoad: load,
    };
}
