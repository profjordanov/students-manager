import axios from 'axios';
import { baseUrl } from './apiConfig';

export async function getRoboExamination(userId) {
    try {
        const response = await axios.get(`${baseUrl}/RoboExamination`, { params: { userId } });
        return { status: 'ready', examination: response.data };
    } catch (error) {
        const code = error?.response?.data?.code;
        if (code === 'noActiveExamination') return { status: 'disabled' };
        if (code === 'roboQuizRequired') return { status: 'quizRequired' };
        if (code === 'noVariants') return { status: 'none' };
        if (code === 'alreadySubmitted') return { status: 'submitted' };
        throw error;
    }
}

export async function submitRoboExamination({ userId, attemptId, answers }) {
    try {
        const response = await axios.post(`${baseUrl}/RoboExaminationResults`, {
            userId,
            attemptId,
            answers,
        });
        return { status: 'saved', result: response.data };
    } catch (error) {
        if (error?.response?.status === 409
            && error?.response?.data?.code === 'alreadySubmitted') {
            return { status: 'duplicate' };
        }
        throw error;
    }
}

export async function getRoboExaminationResults(userId) {
    const response = await axios.get(
        `${baseUrl}/RoboExaminationResults/${encodeURIComponent(userId)}`
    );
    return response.data;
}
