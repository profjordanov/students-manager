import { useEffect, useState } from 'react';
import { getUsersScore } from '../services/usersScoreService';

const formatScore = (value) => Number(value ?? 0).toFixed(2);

const getDisplayName = (score) => score.fullName || score.userName || score.userId;

export default function UsersScorePage() {
  const [scores, setScores] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [loadError, setLoadError] = useState('');

  useEffect(() => {
    getUsersScore()
      .then((data) => setScores(Array.isArray(data) ? data : []))
      .catch((err) => {
        console.error('Failed to load users score', err);
        setLoadError("Failed to load users' score.");
      })
      .finally(() => setIsLoading(false));
  }, []);

  return (
    <div className="users-score-page">
      <h1>User&apos;s Score</h1>
      <p className="subtitle">Examination scores and points of all users</p>

      {isLoading && <p className="users-score-status">Loading…</p>}
      {loadError && <p className="users-score-status" role="alert">{loadError}</p>}
      {!isLoading && !loadError && scores.length === 0 && (
        <p className="users-score-status">No scores available.</p>
      )}

      {!isLoading && !loadError && scores.length > 0 && (
        <div className="users-score-table-wrap">
          <table className="users-score-table">
            <thead>
              <tr>
                <th>User</th>
                <th>First Examination</th>
                <th>Second Examination</th>
                <th>Points</th>
                <th>Total</th>
              </tr>
            </thead>
            <tbody>
              {scores.map((score) => (
                <tr key={score.userId}>
                  <td>
                    <div className="student-name">{getDisplayName(score)}</div>
                    {score.fullName && score.userName && (
                      <div className="student-username">@{score.userName}</div>
                    )}
                  </td>
                  <td>{formatScore(score.firstExaminationScore)}</td>
                  <td>{formatScore(score.secondExaminationScore)}</td>
                  <td>{formatScore(score.points)}</td>
                  <td><strong>{formatScore(score.total)}</strong></td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
    </div>
  );
}
