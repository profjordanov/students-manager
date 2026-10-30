import React, { useEffect, useLayoutEffect, useRef, useState } from 'react';
import { Link } from 'react-router-dom';
import Typed from 'typed.js';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import {
    faGraduationCap,
    faComments,
    faRobot,
    faClipboardCheck,
} from '@fortawesome/free-solid-svg-icons';
import robotImage from '../assets/home/robot.png';
import { useAuth } from '../context/AuthContext';
import { getErrorMessage } from '../services/apiConfig';
import { getEnabledExamination } from '../services/roboQuizService';
import { getExaminationResults } from '../services/examinationResultsService';
import { getProfile } from '../services/userService';
import './HomePage.css';

// ─── Constants ───────────────────────────────────────────────────────────────

const FALLBACK_STATS = {
    mastery: 75,
    masteryBar: 75,
    precision: 92,
    precisionBar: 92,
    uptime: 124,
    uptimeBar: 65,
    masteryLabel: '15 tasks completed this week',
    precisionLabel: 'Top 10% global accuracy',
    uptimeLabel: 'Top 5% of this month',
};

const KONAMI_SEQUENCE = [
    'ArrowUp', 'ArrowUp', 'ArrowDown', 'ArrowDown',
    'ArrowLeft', 'ArrowRight', 'ArrowLeft', 'ArrowRight',
    'b', 'a',
];

const FEATURES = [
    {
        icon: faGraduationCap,
        title: 'Course Management',
        desc: 'Track progress across modules with real-time analytics and smart recommendations.',
    },
    {
        icon: faComments,
        title: 'Forum',
        desc: 'Collaborate with peers and instructors in threaded, asynchronous discussions.',
    },
    {
        icon: faRobot,
        title: 'AI Chatbot',
        desc: 'Get instant, AI-powered answers to your technical and curriculum questions.',
    },
    {
        icon: faClipboardCheck,
        title: 'Tests & Quizzes',
        desc: 'Sharpen skills with adaptive assessments, instant feedback, and leaderboards.',
    },
];

const COMMUNITY_CARDS = [
    {
        emoji: '🌍',
        title: 'Open Community',
        desc: 'A welcoming space for learners of every background and skill level.',
    },
    {
        emoji: '🚀',
        title: 'Learn by Doing',
        desc: 'Hands-on challenges, real projects, and instant feedback to accelerate your growth.',
    },
    {
        emoji: '🤝',
        title: 'Grow Together',
        desc: 'Collaborate with peers, share knowledge, and level up as a team.',
    },
];

// Generated once at module level — Math.random() is not allowed inside useMemo
const CONFETTI_PIECES = Array.from({ length: 45 }, (_, i) => ({
    id: i,
    left: `${Math.random() * 100}%`,
    delay: `${(Math.random() * 0.9).toFixed(2)}s`,
    color: `hsl(${Math.floor(Math.random() * 360)}, 75%, 60%)`,
    size: `${7 + Math.floor(Math.random() * 7)}px`,
    duration: `${2 + Math.random() * 1.2}s`,
}));

// ─── Sub-components ───────────────────────────────────────────────────────────

const AnimatedNumber = ({ value, duration = 2000 }) => {
    const [count, setCount] = useState(0);
    useEffect(() => {
        let start = 0;
        const end = parseInt(value);
        if (start === end) return;
        const step = Math.abs(Math.floor(duration / end));
        const timer = setInterval(() => {
            start += 1;
            setCount(start);
            if (start === end) clearInterval(timer);
        }, step);
        return () => clearInterval(timer);
    }, [value, duration]);
    return <>{count}</>;
};

const SkeletonEngineBox = () => (
    <div className='engine-box skeleton-mode' aria-hidden='true'>
        <div className='box-top'>
            <span className='skel skel-label'></span>
            <span className='skel skel-percent'></span>
        </div>
        <div className='box-bar'>
            <div className='skel skel-fill'></div>
        </div>
        <div className='box-bottom'>
            <span className='skel skel-text-lg'></span>
            <span className='skel skel-text-sm'></span>
        </div>
    </div>
);

// ─── Main Component ───────────────────────────────────────────────────────────

function HomePage() {
    const { userId } = useAuth();
    const canvasRef = useRef(null);
    const subtitleRef = useRef(null);
    const containerRef = useRef(null);
    const availabilityRequestId = useRef(0);
    const historyRequestId = useRef(0);
    const currentUserId = useRef(userId);
    const currentActiveType = useRef(null);

    const [timeLeft, setTimeLeft] = useState('');
    const [stats, setStats] = useState(null); // null = loading
    const [availability, setAvailability] = useState({ status: 'loading', examination: null, error: '' });
    const [examinationHistory, setExaminationHistory] = useState({ status: 'idle', userId: null, type: null });
    const [availabilityRetry, setAvailabilityRetry] = useState(0);
    const [historyRetry, setHistoryRetry] = useState(0);
    const [easterEgg, setEasterEgg] = useState(false);
    // useRef avoids re-renders on every keypress while still tracking sequence position
    const konamiIdxRef = useRef(0);
    const easterEggTimerRef = useRef(null);
    const activeType = availability.examination?.type ?? null;

    useLayoutEffect(() => {
        currentUserId.current = userId;
    }, [userId]);

    useLayoutEffect(() => {
        currentActiveType.current = activeType;
    }, [activeType]);

    useEffect(() => {
        let active = true;

        const loadAvailability = async (background = false) => {
            const requestId = ++availabilityRequestId.current;
            // Keep navigation mounted when a click first gives focus back to the window.
            setAvailability((previous) => background && previous.status === 'ready'
                ? previous
                : { ...previous, status: 'loading', error: '' });
            try {
                const result = await getEnabledExamination();
                if (!active || requestId !== availabilityRequestId.current) return;
                setAvailability(result.status === 'disabled'
                    ? { status: 'disabled', examination: null, error: '' }
                    : { status: 'ready', examination: result.examination, error: '' });
            } catch (error) {
                if (!active || requestId !== availabilityRequestId.current) return;
                console.error('Active examination loading failed:', error);
                setAvailability({
                    status: 'error',
                    examination: null,
                    error: getErrorMessage(error, 'The active assessment is unavailable right now.'),
                });
            }
        };

        const handleFocus = () => {
            void loadAvailability(true);
            setHistoryRetry((value) => value + 1);
        };

        void loadAvailability();
        window.addEventListener('focus', handleFocus);
        return () => {
            active = false;
            availabilityRequestId.current += 1;
            window.removeEventListener('focus', handleFocus);
        };
    }, [availabilityRetry, userId]);

    useEffect(() => {
        let active = true;
        const requestId = ++historyRequestId.current;
        if (!userId || !activeType) {
            return () => {
                active = false;
                historyRequestId.current += 1;
            };
        }

        const requestedUserId = userId;
        const requestedType = activeType;

        getExaminationResults(requestedUserId, requestedType)
            .then((results) => {
                if (!active
                    || requestId !== historyRequestId.current
                    || currentUserId.current !== requestedUserId
                    || currentActiveType.current !== requestedType) return;
                setExaminationHistory({
                    status: 'loaded',
                    userId: requestedUserId,
                    type: requestedType,
                    retry: historyRetry,
                    hasQuizResult: results.quizResults.length > 0,
                    hasExaminationResult: results.roboExaminationResults.length > 0,
                });
            })
            .catch((error) => {
                if (!active
                    || requestId !== historyRequestId.current
                    || currentUserId.current !== requestedUserId
                    || currentActiveType.current !== requestedType) return;
                console.error('Examination history loading failed:', error);
                setExaminationHistory({
                    status: 'error',
                    userId: requestedUserId,
                    type: requestedType,
                    retry: historyRetry,
                    error: getErrorMessage(error, 'Saved examination results could not be checked.'),
                });
            });

        return () => {
            active = false;
            historyRequestId.current += 1;
        };
    }, [activeType, historyRetry, userId]);

    // Safety: clear both inline-style AND class-based overflow locks.
    // index.css contains body.of-hidden, body.modal_show, and html.fp-enabled rules
    // that are set by legacy scripts elsewhere in the app and survive a plain
    // body.style.overflow = '' reset.
    useEffect(() => {
        document.body.style.overflow = '';
        document.body.classList.remove('of-hidden', 'modal_show');
        document.documentElement.classList.remove('of-hidden', 'modal_show', 'fp-enabled');
        return () => {
            document.body.style.overflow = '';
            clearTimeout(easterEggTimerRef.current);
        };
    }, []);

    // Lock body scroll while celebration overlay is visible; restore when it closes
    useEffect(() => {
        document.body.style.overflow = easterEgg ? 'hidden' : '';
    }, [easterEgg]);

    // Matrix rain canvas
    useEffect(() => {
        const canvas = canvasRef.current;
        const ctx = canvas.getContext('2d');
        canvas.width = window.innerWidth;
        canvas.height = window.innerHeight;

        const characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789<>/{}[];:+-*';
        const fontSize = 16;
        const columns = canvas.width / fontSize;
        const drops = Array(Math.floor(columns)).fill(1);

        const draw = () => {
            ctx.fillStyle = 'rgba(0, 0, 0, 0.08)';
            ctx.fillRect(0, 0, canvas.width, canvas.height);
            ctx.fillStyle = '#5fb1a8';
            ctx.font = fontSize + 'px monospace';

            for (let i = 0; i < drops.length; i++) {
                const text = characters.charAt(Math.floor(Math.random() * characters.length));
                ctx.fillText(text, i * fontSize, drops[i] * fontSize);
                if (drops[i] * fontSize > canvas.height && Math.random() > 0.975) drops[i] = 0;
                drops[i]++;
            }
        };
        const interval = setInterval(draw, 35);

        const handleResize = () => {
            canvas.width = window.innerWidth;
            canvas.height = window.innerHeight;
        };
        window.addEventListener('resize', handleResize);

        return () => {
            clearInterval(interval);
            window.removeEventListener('resize', handleResize);
        };
    }, []);

    // Countdown to midnight
    useEffect(() => {
        const calculateTimeLeft = () => {
            const now = new Date();
            const midnight = new Date();
            midnight.setHours(24, 0, 0, 0);
            const diff = midnight - now;
            const h = Math.floor(diff / (1000 * 60 * 60));
            const m = Math.floor((diff / (1000 * 60)) % 60);
            const s = Math.floor((diff / 1000) % 60);
            return `${String(h).padStart(2, '0')}:${String(m).padStart(2, '0')}:${String(s).padStart(2, '0')}`;
        };
        const timer = setInterval(() => setTimeLeft(calculateTimeLeft()), 1000);
        return () => clearInterval(timer);
    }, []);

    // Typewriter effect on hero subtitle
    useEffect(() => {
        if (!subtitleRef.current) return;
        const typed = new Typed(subtitleRef.current, {
            strings: [
                'Your learning hub — powered by AI, built for curious minds.',
                'Track progress, solve challenges, and grow with your peers.',
                'Everything you need to go from beginner to builder.',
            ],
            typeSpeed: 38,
            backSpeed: 18,
            backDelay: 2800,
            startDelay: 500,
            loop: true,
            smartBackspace: true,
            cursorChar: '|',
        });
        return () => typed.destroy();
    }, []);

    // Scroll-triggered fade-in via IntersectionObserver.
    // Deferred via requestIdleCallback (setTimeout fallback) so observer setup
    // doesn't compete with initial paint and first-frame rendering.
    useEffect(() => {
        const observer = new IntersectionObserver(
            (entries) =>
                entries.forEach((entry) => {
                    if (entry.isIntersecting) {
                        entry.target.classList.add('is-visible');
                        observer.unobserve(entry.target);
                    }
                }),
            { rootMargin: '-10% 0px', threshold: 0 }
        );

        const setup = () => {
            const sections = containerRef.current?.querySelectorAll('.fade-in-section');
            sections?.forEach((el) => observer.observe(el));
        };

        const idleId = 'requestIdleCallback' in window
            ? requestIdleCallback(setup)
            : setTimeout(setup, 0);

        return () => {
            observer.disconnect();
            'requestIdleCallback' in window
                ? cancelIdleCallback(idleId)
                : clearTimeout(idleId);
        };
    }, []);

    // Fetch real stats for the logged-in student; anonymous visitors see generic fallback stats.
    useEffect(() => {
        if (!userId) return;

        getProfile(userId)
            .then((data) => {
                const questions = data.testQuestions ?? [];

                if (questions.length === 0) {
                    setStats(FALLBACK_STATS);
                    return;
                }

                const total = questions.length;
                const correct = questions.filter((q) => q.wasCorrect).length;
                const mastery = Math.round((correct / total) * 100);

                // Weighted precision: recent 10 answers count double
                const recent = questions.slice(-10);
                const recentCorrect = recent.filter((q) => q.wasCorrect).length;
                const precision = Math.min(
                    100,
                    Math.round(((correct + recentCorrect * 2) / (total + recent.length)) * 100)
                );

                // Uptime: ~8 min per question attempt, bar capped at 200h
                const uptime = Math.max(1, Math.round((total * 8) / 60));
                const uptimeBar = Math.min(100, Math.round((uptime / 200) * 100));

                setStats({
                    mastery,
                    masteryBar: mastery,
                    precision,
                    precisionBar: precision,
                    uptime,
                    uptimeBar,
                    masteryLabel: `${correct} correct out of ${total} attempts`,
                    precisionLabel: `Based on last ${Math.min(10, total)} questions`,
                    uptimeLabel: `~${uptime}h total study time`,
                });
            })
            .catch(() => setStats(FALLBACK_STATS));
    }, [userId]);

    // Anonymous visitors never fetch, so they see the fallback immediately instead of a loading skeleton.
    const displayStats = userId ? stats : FALLBACK_STATS;
    const historyMatchesCurrentAssessment = userId
        && examinationHistory.userId === userId
        && examinationHistory.type === activeType
        && (examinationHistory.retry === historyRetry || examinationHistory.status === 'loaded');
    const isHistoryLoading = userId && !historyMatchesCurrentAssessment;

    // Konami code easter egg — useRef tracks index without triggering re-renders
    useEffect(() => {
        const handleKey = (e) => {
            const idx = konamiIdxRef.current;
            if (e.key !== KONAMI_SEQUENCE[idx]) {
                konamiIdxRef.current = e.key === KONAMI_SEQUENCE[0] ? 1 : 0;
                return;
            }
            const next = idx + 1;
            if (next === KONAMI_SEQUENCE.length) {
                clearTimeout(easterEggTimerRef.current);
                setEasterEgg(true);
                easterEggTimerRef.current = setTimeout(() => setEasterEgg(false), 3200);
                konamiIdxRef.current = 0;
            } else {
                konamiIdxRef.current = next;
            }
        };
        window.addEventListener('keydown', handleKey);
        return () => window.removeEventListener('keydown', handleKey);
    }, []);

    return (
        <div className='hp-container' ref={containerRef}>
            <canvas ref={canvasRef} className='matrix-canvas' aria-hidden='true'></canvas>

            {/* ── Hero ─────────────────────────────────────────────────────── */}
            <section className='hp-hero' aria-labelledby='hero-title'>
                <div className='hero-main'>
                    <div className='hero-content'>
                        <h1 className='hero-title' id='hero-title'>
                            Welcome, <br />
                            <span>Fellow Builders</span>
                        </h1>
                        <p className='hero-subtitle' ref={subtitleRef} aria-live='polite'></p>
                    </div>
                    <div className='robot-container' aria-hidden='true'>
                        <img src={robotImage} alt='' className='robot-anim' role='presentation' />
                    </div>
                </div>

                {availability.status === 'loading' && (
                    <section className='assessment-announcement' aria-label='Active assessment'>
                        <p role='status'>Checking for an active assessment...</p>
                    </section>
                )}
                {availability.status === 'error' && (
                    <section className='assessment-announcement' aria-label='Active assessment'>
                        <div role='alert'>
                            <p>{availability.error}</p>
                            <button
                                type='button'
                                className='assessment-announcement__button'
                                onClick={() => setAvailabilityRetry((value) => value + 1)}
                            >
                                Retry
                            </button>
                        </div>
                    </section>
                )}
                {availability.status === 'ready' && availability.examination && (
                    <section
                        className='assessment-announcement assessment-announcement--active'
                        aria-labelledby='assessment-title'
                    >
                        <div>
                            <p className='assessment-announcement__eyebrow'>Assessment open</p>
                            <h2 id='assessment-title'>Active Robo assessment: {availability.examination.type}</h2>
                            {!userId && (
                                <p>
                                    <Link to='/login' className='assessment-announcement__link'>Log in to continue</Link>
                                </p>
                            )}
                            {isHistoryLoading && (
                                <p role='status'>Checking your saved examination results...</p>
                            )}
                            {historyMatchesCurrentAssessment && examinationHistory.status === 'error' && (
                                <div role='alert'>
                                    <p>{examinationHistory.error}</p>
                                    <button
                                        type='button'
                                        className='assessment-announcement__button'
                                        onClick={() => setHistoryRetry((value) => value + 1)}
                                    >
                                        Retry checking results
                                    </button>
                                </div>
                            )}
                            {historyMatchesCurrentAssessment
                                && examinationHistory.status === 'loaded'
                                && (examinationHistory.hasQuizResult || examinationHistory.hasExaminationResult) && (
                                    <div className='assessment-announcement__actions'>
                                        <Link to='/robo-examination/results' className='assessment-announcement__link'>
                                            View examination results
                                        </Link>
                                        {examinationHistory.hasQuizResult && !examinationHistory.hasExaminationResult && (
                                            <Link to='/robo-examination' className='assessment-announcement__secondary'>
                                                Continue to RoboExamination
                                            </Link>
                                        )}
                                    </div>
                                )}
                            {historyMatchesCurrentAssessment
                                && examinationHistory.status === 'loaded'
                                && !examinationHistory.hasQuizResult
                                && !examinationHistory.hasExaminationResult && (
                                    <Link to='/quiz' className='assessment-announcement__link'>Start RoboQuiz</Link>
                                )}
                        </div>
                    </section>
                )}
            </section>

            {/* ── Platform Feature Cards ────────────────────────────────────── */}
            <section
                className='hp-section features-section fade-in-section'
                aria-labelledby='features-title'
            >
                <div className='zone-header'>
                    <h2 className='zone-title' id='features-title'>
                        Platform Capabilities
                    </h2>
                    <p className='zone-sub'>Everything you need to get started and keep going</p>
                </div>
                <div className='features-grid'>
                    {FEATURES.map(({ icon, title, desc }) => (
                        <article
                            className='feature-card'
                            key={title}
                            tabIndex='0'
                            aria-label={title}
                        >
                            <div className='feature-icon-wrap' aria-hidden='true'>
                                <FontAwesomeIcon icon={icon} className='feature-icon' />
                            </div>
                            <h3 className='feature-card-title'>{title}</h3>
                            <p className='feature-card-desc'>{desc}</p>
                        </article>
                    ))}
                </div>
            </section>

            {/* ── Our Community ──────────────────────────────────────────── */}
            <section
                className='hp-section community-section fade-in-section'
                aria-labelledby='community-title'
            >
                <div className='zone-header'>
                    <h2 className='zone-title' id='community-title'>
                        Our Community
                    </h2>
                    <p className='zone-sub'>Built by learners, for learners</p>
                </div>
                <p className='community-intro'>
                    This platform is designed for folks who want to sharpen their skills,
                    tackle real-world challenges, and grow alongside a supportive community.
                    Whether you are just getting started or looking to level up, you will find
                    the tools, resources, and people to help you succeed.
                </p>
                <div className='community-grid'>
                    {COMMUNITY_CARDS.map(({ emoji, title, desc }) => (
                        <article className='community-card' key={title} tabIndex='0'>
                            <span className='community-emoji' aria-hidden='true'>{emoji}</span>
                            <h3 className='community-card-title'>{title}</h3>
                            <p className='community-card-desc'>{desc}</p>
                        </article>
                    ))}
                </div>
            </section>

            {/* ── Learning Core ─────────────────────────────────────────────── */}
            <section
                className='hp-section engine-zone fade-in-section'
                aria-labelledby='engine-title'
            >
                <div className='zone-header'>
                    <h2 className='zone-title' id='engine-title'>
                        Learning Core
                    </h2>
                    <p className='zone-sub'>
                        {displayStats === null ? 'Loading live data…' : 'Real-time neural progress tracking'}
                    </p>
                </div>

                {displayStats === null ? (
                    // ── Skeleton loading state
                    <div className='engine-grid' aria-label='Loading statistics' aria-busy='true'>
                        <SkeletonEngineBox />
                        <SkeletonEngineBox />
                        <SkeletonEngineBox />
                    </div>
                ) : (
                    // ── Real data
                    <div className='engine-grid'>
                        <div className='engine-box' data-tooltip='Percentage of correct answers'>
                            <div className='box-top'>
                                <span className='box-label'>Algorithm Mastery</span>
                                <span className='box-percent'>
                                    <AnimatedNumber value={displayStats.mastery} />%
                                </span>
                            </div>
                            <div
                                className='box-bar'
                                role='progressbar'
                                aria-valuenow={displayStats.mastery}
                                aria-valuemin={0}
                                aria-valuemax={100}
                                aria-label='Algorithm Mastery progress'
                            >
                                <div
                                    className='box-fill'
                                    style={{ '--target-width': `${displayStats.masteryBar}%` }}
                                ></div>
                            </div>
                            <div className='box-bottom'>
                                <span className='box-context'>{displayStats.masteryLabel}</span>
                                <span className='box-goal'>Hit 80% to earn the "Coder" badge</span>
                            </div>
                        </div>

                        <div className='engine-box' data-tooltip='Weighted recent-answer accuracy'>
                            <div className='box-top'>
                                <span className='box-label'>Logic Precision</span>
                                <span className='box-percent'>
                                    <AnimatedNumber value={displayStats.precision} />%
                                </span>
                            </div>
                            <div
                                className='box-bar'
                                role='progressbar'
                                aria-valuenow={displayStats.precision}
                                aria-valuemin={0}
                                aria-valuemax={100}
                                aria-label='Logic Precision progress'
                            >
                                <div
                                    className='box-fill highlight'
                                    style={{ '--target-width': `${displayStats.precisionBar}%` }}
                                ></div>
                            </div>
                            <div className='box-bottom'>
                                <span className='box-context'>{displayStats.precisionLabel}</span>
                                <span className='box-goal'>Reach 95% for "Flawless" status</span>
                            </div>
                        </div>

                        <div className='engine-box' data-tooltip='Estimated total study hours'>
                            <div className='box-top'>
                                <span className='box-label'>Total Uptime</span>
                                <span className='box-percent'>
                                    <AnimatedNumber value={displayStats.uptime} />h
                                </span>
                            </div>
                            <div
                                className='box-bar'
                                role='progressbar'
                                aria-valuenow={displayStats.uptimeBar}
                                aria-valuemin={0}
                                aria-valuemax={100}
                                aria-label='Total Uptime progress'
                            >
                                <div
                                    className='box-fill gold'
                                    style={{ '--target-width': `${displayStats.uptimeBar}%` }}
                                ></div>
                            </div>
                            <div className='box-bottom'>
                                <span className='box-context'>{displayStats.uptimeLabel}</span>
                                <span className='box-goal'>Keep it up — "Senior" level is within reach</span>
                            </div>
                        </div>
                    </div>
                )}
            </section>

            {/* ── Daily Challenge ───────────────────────────────────────────── */}
            <section
                className='hp-section daily-challenge fade-in-section'
                aria-labelledby='challenge-title'
            >
                <div className='mac-terminal' role='region' aria-label='Daily coding challenge'>
                    <div className='terminal-header'>
                        <div className='terminal-buttons' aria-hidden='true'>
                            <span className='dot red'></span>
                            <span className='dot yellow'></span>
                            <span className='dot green'></span>
                        </div>
                        <div className='terminal-title'>daily_challenge.js</div>
                    </div>
                    <div className='terminal-body'>
                        <h2 className='challenge-title' id='challenge-title'>
                            Today's Mission
                        </h2>
                        <p className='challenge-desc'>
                            Write a function to find the first non-repeating character in a string.
                        </p>
                        <div className='code-container'>
                            <code className='challenge-snippet'>
                                {`function findFirstUnique(str) {\n  // Your logic here...\n  return result;\n}`}
                            </code>
                        </div>
                        <div className='terminal-actions'>
                            <div className='challenge-timer'>
                                <span className='timer-label'>TIME REMAINING:</span>
                                <span
                                    className='timer-value'
                                    aria-live='polite'
                                    aria-label={`Time remaining: ${timeLeft}`}
                                >
                                    {timeLeft}
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
            </section>

            {/* ── Wall of Fame ──────────────────────────────────────────────── */}
            <section
                className='hp-section achievements-section fade-in-section'
                aria-labelledby='achievements-title'
            >
                <h2 className='section-title' id='achievements-title'>
                    Wall of Fame
                </h2>
                <div className='achievements-grid'>
                    <article className='achievement-card'>
                        <div className='firework-container' aria-hidden='true'>
                            <div className='firework'></div>
                            <div className='firework'></div>
                        </div>
                        <div className='achievement-icon' aria-hidden='true'>🏆</div>
                        <h3>Quiz Master</h3>
                        <p>100% Score on Advanced Matrix Quiz</p>
                        <div className='student-list' aria-label='Achievers'>
                            <span>@ivan_dev</span>
                            <span>@mary_js</span>
                            <span>@kris_tech</span>
                        </div>
                    </article>
                    <article className='achievement-card'>
                        <div className='firework-container' aria-hidden='true'>
                            <div className='firework'></div>
                            <div className='firework'></div>
                        </div>
                        <div className='achievement-icon' aria-hidden='true'>🤖</div>
                        <h3>Bot Whisperer</h3>
                        <p>Successfully debugged the AI Chatbot</p>
                        <div className='student-list' aria-label='Achievers'>
                            <span>@alex_99</span>
                            <span>@nina_codes</span>
                            <span>@george_m</span>
                        </div>
                    </article>
                    <article className='achievement-card'>
                        <div className='firework-container' aria-hidden='true'>
                            <div className='firework'></div>
                            <div className='firework'></div>
                        </div>
                        <div className='achievement-icon' aria-hidden='true'>📜</div>
                        <h3>Logic Architect</h3>
                        <p>Submitted flawless Homework #4</p>
                        <div className='student-list' aria-label='Achievers'>
                            <span>@stefan_v</span>
                            <span>@elena_b</span>
                            <span>@viktor_d</span>
                        </div>
                    </article>
                </div>
            </section>

            {/* ── Konami Easter Egg Overlay ─────────────────────────────────── */}
            {easterEgg && (
                <div
                    className='easter-overlay'
                    role='alert'
                    aria-live='assertive'
                    aria-label='Achievement unlocked: Code Ninja'
                >
                    <div className='confetti-container' aria-hidden='true'>
                        {CONFETTI_PIECES.map((p) => (
                            <div
                                key={p.id}
                                className='confetti-piece'
                                style={{
                                    '--left': p.left,
                                    '--delay': p.delay,
                                    '--color': p.color,
                                    '--size': p.size,
                                    '--duration': p.duration,
                                }}
                            />
                        ))}
                    </div>
                    <div className='easter-content'>
                        <div className='easter-icon' aria-hidden='true'>🎉</div>
                        <h2 className='easter-title'>Achievement Unlocked</h2>
                        <p className='easter-subtitle'>Code Ninja!</p>
                    </div>
                </div>
            )}
        </div>
    );
}

export default HomePage;
