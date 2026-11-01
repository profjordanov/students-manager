import { useState, useRef, useEffect, useCallback } from 'react';

const MAX_ANSWER_LENGTH = 2000;

function ChatbotTextInput({ onSubmit }) {
    const [value, setValue] = useState('');
    const [shaking, setShaking] = useState(false);
    const submitted = useRef(false);
    const shakeTimer = useRef(null);

    useEffect(() => () => clearTimeout(shakeTimer.current), []);

    const handleSubmit = useCallback(() => {
        if (submitted.current) return;
        const trimmed = value.trim();
        if (!trimmed) {
            setShaking(true);
            shakeTimer.current = setTimeout(() => setShaking(false), 600);
            return;
        }
        submitted.current = true;
        onSubmit(trimmed);
    }, [value, onSubmit]);

    // Enter inserts a new line; Ctrl/Cmd+Enter submits.
    const handleKeyDown = useCallback((e) => {
        if (e.key === 'Enter' && (e.ctrlKey || e.metaKey) && !e.nativeEvent.isComposing) {
            e.preventDefault();
            handleSubmit();
        }
    }, [handleSubmit]);

    return (
        <div className={`soge-input-wrapper ${shaking ? 'soge-shake' : ''}`}>
            <label htmlFor="soge-answer-input" className="soge-input-label">Type your answer in English</label>
            <textarea
                id="soge-answer-input"
                className="soge-textarea"
                rows={6}
                value={value}
                maxLength={MAX_ANSWER_LENGTH}
                onChange={(e) => setValue(e.target.value)}
                onKeyDown={handleKeyDown}
                aria-describedby="soge-answer-hint"
                autoFocus
            />
            <div className="soge-input-footer">
                <span id="soge-answer-hint" className="soge-input-hint">
                    Ctrl+Enter to submit · {value.length}/{MAX_ANSWER_LENGTH}
                </span>
                <button type="button" className="soge-btn soge-btn--primary" onClick={handleSubmit}>Submit</button>
            </div>
        </div>
    );
}

export default ChatbotTextInput;
