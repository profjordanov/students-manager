import axios from "axios"
import { baseUrl } from './apiConfig';

export const login = async (loginRequest) => {
    const response = await axios.post(`${baseUrl}/login`, loginRequest);
    return response.data;
};

export const getProfile = async (id) => {
    const res = await axios.get(`${baseUrl}/students/profile/${id}`);
    return res.data;
};

export const updatePicture = async (data) => {
    const res = await axios.put(`${baseUrl}/students/picture`, data);
    return res.data;
};

export const getStudents = async () => {
    const res = await axios.get(`${baseUrl}/students/`, {
        auth: { username: 'guest', password: 'guest' },
    });
    return res.data;
};