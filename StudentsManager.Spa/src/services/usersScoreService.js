import { baseUrl } from './apiConfig';

export const getUsersScore = async () => {
  const res = await fetch(`${baseUrl}/usersscore`);

  if (!res.ok) {
    throw new Error('Failed to fetch users score');
  }

  return res.json();
};
