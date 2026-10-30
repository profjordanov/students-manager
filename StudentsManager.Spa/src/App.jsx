import { lazy, Suspense } from 'react';
import { Navigate, Route, Routes } from 'react-router-dom';
import { useAuth } from './context/AuthContext';

import Layout from './components/Layout/Layout';

const HomePage = lazy(() => import('./pages/HomePage'));
const LoginPage = lazy(() => import('./pages/LoginPage'));
const ProfilePage = lazy(() => import('./pages/ProfilePage'));
const SlidoPage = lazy(() => import('./pages/SlidoPage'));
const ChatbotPage = lazy(() => import('./pages/ChatbotPage'));
const ChatbotResultsPage = lazy(() => import('./pages/ChatbotResultsPage'));
const QuizPage = lazy(() => import('./pages/QuizPage'));
const UserlessPage = lazy(() => import('./pages/UserlessPage'));
const ComradesPage = lazy(() => import('./pages/ComradesPage'));
const TimelinePage = lazy(() => import('./pages/TimelinePage'));
const UsersScorePage = lazy(() => import('./pages/UsersScorePage'));

function App() {
  const { isLoggedIn } = useAuth();

  return (
    <Layout>
      <Suspense fallback={null}>
        <Routes>
          <Route path='/' element={<HomePage />} />
          <Route path='/userless' element={<UserlessPage />} />
          <Route path='/users-score' element={<UsersScorePage />} />
          {isLoggedIn ? (
            <>
              <Route path='/slido' element={<SlidoPage />} />
              <Route path='/comrades' element={<ComradesPage />} />
              <Route path='/profile' element={<ProfilePage />} />
              <Route path='/quiz' element={<QuizPage />} />
              <Route path='/robo-examination' element={<ChatbotPage />} />
              <Route path='/robo-examination/results' element={<ChatbotResultsPage />} />
              <Route path='/chatbot' element={<Navigate to='/robo-examination' replace />} />
              <Route path='/chatbot/results' element={<Navigate to='/robo-examination/results' replace />} />
              <Route path='/timeline' element={<TimelinePage />} />
              <Route path='/login' element={<Navigate to='/' replace />} />
            </>
          ) : (
            <>
              <Route path='/login' element={<LoginPage />} />
              <Route path='/slido' element={<Navigate to='/login' replace />} />
              <Route path='/quiz' element={<Navigate to='/login' replace />} />
              <Route path='/comrades' element={<Navigate to='/login' replace />} />
              <Route path='/timeline' element={<Navigate to='/login' replace />} />
              <Route path='/profile' element={<Navigate to='/login' replace />} />
              <Route path='/robo-examination' element={<Navigate to='/login' replace />} />
              <Route path='/robo-examination/results' element={<Navigate to='/login' replace />} />
              <Route path='/chatbot' element={<Navigate to='/login' replace />} />
              <Route path='/chatbot/results' element={<Navigate to='/login' replace />} />
            </>
          )}
          <Route path='*' element={<Navigate to='/' replace />} />
        </Routes>
      </Suspense>
    </Layout>
  );
}

export default App;
