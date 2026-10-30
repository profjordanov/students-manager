function ChatbotOptions({ options, onSelect }) {
    const countClass = options.length <= 3 ? `btn-${options.length}` : 'btn-n';

    return (
        <div className={`soge-btn-wrapper ${countClass}`}>
            {options.map((text) => (
                <button key={text} type="button" className="soge-btn" onClick={() => onSelect(text)}>
                    {text}
                </button>
            ))}
        </div>
    );
}

export default ChatbotOptions;
