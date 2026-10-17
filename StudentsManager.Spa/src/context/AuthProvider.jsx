import { useCallback, useMemo, useState } from 'react';
import { AuthContext } from './AuthContext';
import { logEvent } from '../services/eventsService';

export const AuthProvider = ({ children }) => {
  const [userId, setUserId] = useState(() => sessionStorage.getItem('userId'));

  const login = useCallback((newUserId) => {
    sessionStorage.setItem('userId', newUserId);
    setUserId(newUserId);

    logEvent(newUserId, 'profile-login', {
      message: 'User logged in',
    });
  }, []);

  const logout = useCallback(() => {
    sessionStorage.removeItem('userId');
    setUserId(null);
  }, []);

  const value = useMemo(
    () => ({ isLoggedIn: Boolean(userId), userId, login, logout }),
    [userId, login, logout]
  );

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
};
