export const baseUrl =
    import.meta.env.VITE_API_BASE_URL || 'https://students-manager.azurewebsites.net/api';

// ASP.NET error responses may be a plain string, { message }, or ProblemDetails ({ title }).
export function getErrorMessage(err, fallback) {
    const data = err?.response?.data;
    if (typeof data === 'string' && data.trim()) return data;
    return data?.message || data?.title || err?.message || fallback;
}
