import { getQuizResults } from './roboQuizService';
import { getRoboExaminationResults } from './roboExaminationService';

export async function getExaminationResults(userId, type) {
    if (!['First', 'Second'].includes(type)) {
        throw new Error('The examination type must be First or Second.');
    }

    const [quizResults, roboExaminationResults] = await Promise.all([
        getQuizResults(userId),
        getRoboExaminationResults(userId),
    ]);

    if ([quizResults, roboExaminationResults].some((results) =>
        !Array.isArray(results) || results.some((result) =>
            !result || !['First', 'Second'].includes(result.type)))) {
        throw new Error('The examination results response is invalid.');
    }

    return {
        quizResults: quizResults.filter((result) => result.type === type),
        roboExaminationResults: roboExaminationResults.filter((result) => result.type === type),
    };
}
