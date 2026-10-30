import { useEffect, useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faMessage, faBook } from "@fortawesome/free-solid-svg-icons";
import { getRoboExaminationResults } from "../services/roboExaminationService";
import { getStudents } from "../services/userService";

export default function ComradesPage() {

    const [students, setStudents] = useState([]);
    const [loadError, setLoadError] = useState("");
    const [info, setInfo] = useState(null);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [selectedStudent, setSelectedStudent] = useState(null);

    useEffect(() => {
        getStudents()
            .then((data) => setStudents(Array.isArray(data) ? data : []))
            .catch((err) => {
                console.error("Failed to load students", err);
                setLoadError("Failed to load students.");
            });
    }, []);

    async function showStudent(userId) {
        if (!userId) return;

        try {
            const data = await getRoboExaminationResults(userId);

            if (!Array.isArray(data) || data.length === 0) {
                setInfo({ grade: "N/A", feedback: "No data", answers: [], evaluationStatus: "None" });
                return;
            }

            const latest = data[0];

            setInfo({
                grade: latest.grade ?? "N/A",
                feedback: latest.feedback ?? "N/A",
                answers: latest.answers ?? [],
                evaluationStatus: latest.evaluationStatus,
                type: latest.type,
                variant: latest.variant,
            });
        } catch (err) {
            console.error("Failed to load examination answers", err);
            setInfo({ grade: "Error", feedback: "Failed to load data", answers: [] });
        } finally {
            setIsModalOpen(true);
        }
    }

    return (
        <div className="comrades-page">
            <h1>Comrades Page</h1>
            <p className="subtitle">Where all peers meet</p>

            {loadError && <p role="alert">{loadError}</p>}

            <div className="students-grid">
                {students.map((student) => {

                    const avatarSrc = student.base64EncodePicture
                        ? student.base64EncodePicture.replace(
                            "data:image/jpeg;base64,data:image/png;base64,",
                            "data:image/png;base64,"
                        )
                        : `https://api.dicebear.com/9.x/initials/svg?seed=${encodeURIComponent(student.fullName)}`;

                    return (
                        <div
                            key={student.userName}
                            onClick={() => {
                                if (!student.id) return;
                                setSelectedStudent(student);
                                showStudent(student.id);
                            }}
                            className="student-card"
                        >
                            <img
                                src={avatarSrc}
                                alt={student.fullName}
                                className="student-avatar"
                            />

                            <div className="student-info">
                                <div className="student-name">{student.fullName}</div>
                                <div className="student-username">@{student.userName}</div>
                                <div className="student-fn">FN: {student.facultyNumber}</div>
                                <div className="student-email">{student.email ? atob(student.email) : ""}</div>
                            </div>

                            <div className="card-actions">
                                <button
                                    className="btn-icon"
                                    onClick={(e) => {
                                        e.stopPropagation();
                                    }}
                                    title="Message"
                                >
                                    <FontAwesomeIcon icon={faMessage} />
                                </button>

                                {/* todo : expose new endpoint */}
                                <button
                                    className="btn-icon"
                                    onClick={(e) => {
                                        e.stopPropagation();
                                    }}
                                    title="View Homeworks"
                                >
                                    <FontAwesomeIcon icon={faBook} />
                                </button>
                            </div>
                        </div>
                    );
                })}
            </div>

            {isModalOpen && info && (
                <div className="modal-overlay" onClick={() => setIsModalOpen(false)}>
                    <div className="modal-content" onClick={(e) => e.stopPropagation()}>
                        <button
                            className="modal-close"
                            onClick={() => setIsModalOpen(false)}
                            aria-label="Close"
                        >
                            ✕
                        </button>
                        <h3>Grade: {info.grade}</h3>
                        <p><strong>Feedback:</strong> {info.feedback}</p>
                        <hr />
                        <h3>RoboExamination — {info.type} · {info.variant} by <span className="span-name">@{selectedStudent?.userName}</span></h3>
                        {info.evaluationStatus === "Pending" && <p>Оценява се…</p>}
                        {info.evaluationStatus === "Failed" && <p>Оценяването не бе успешно. Отговорите са запазени.</p>}
                        {info.answers.length > 0 ? (
                            <ol>
                                {info.answers.map((a, index) => (
                                    <li key={index} style={{ marginBottom: "10px" }}>
                                        <div>
                                            <strong>Q:</strong>{" "}
                                            <strong>
                                                {a.questionText || "Question not available"}
                                            </strong>
                                        </div>
                                        <div>
                                            <strong>A:</strong>{" "}
                                            {a.answer || "N/A"}
                                        </div>
                                    </li>
                                ))}
                            </ol>
                        ) : (
                            <p>No answers available.</p>
                        )}
                    </div>
                </div>
            )}
        </div>
    );
}