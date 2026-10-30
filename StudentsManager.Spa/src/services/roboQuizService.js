import axios from 'axios';
import { baseUrl } from './apiConfig';

export async function getEnabledExamination() {
    const response = await axios.get(`${baseUrl}/ExaminationSettings/enabled`, {
        validateStatus: (status) => status === 200 || status === 204,
    });

    if (response.status === 204) return { status: 'disabled' };

    const examination = response.data;
    if (!examination
        || typeof examination !== 'object'
        || Array.isArray(examination)
        || !Number.isInteger(examination.id)
        || examination.id <= 0
        || !['First', 'Second'].includes(examination.type)) {
        throw new Error('The enabled examination response is invalid.');
    }

    return { status: 'enabled', examination };
}

export async function getNextQuiz(userId) {
    try {
        const response = await axios.get(`${baseUrl}/RoboQuiz`, { params: { userId } });
        return { status: 'ready', quiz: response.data };
    } catch (error) {
        const statusCode = error?.response?.status;
        const message = error?.response?.data?.message;

        if (statusCode === 403) return { status: 'disabled' };
        if (statusCode === 410) return { status: 'exhausted' };
        if (statusCode === 404 && message === 'Няма налични тестове') return { status: 'none' };

        throw error;
    }
}

export async function submitQuizResult({ userId, roboQuizId, scorePercentage }) {
    try {
        await axios.post(`${baseUrl}/RoboQuizResults`, {
            userId,
            roboQuizId,
            scorePercentage,
        });
        return { status: 'saved' };
    } catch (error) {
        if (error?.response?.status === 409) return { status: 'duplicate' };
        throw error;
    }
}

export async function getQuizResults(userId) {
    const response = await axios.get(`${baseUrl}/RoboQuizResults/${encodeURIComponent(userId)}`);
    return response.data;
}
