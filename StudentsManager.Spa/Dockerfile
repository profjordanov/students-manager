# ---------- Build stage ----------
FROM node:20-alpine AS build
WORKDIR /app

# Install deps first (better layer caching)
COPY package.json package-lock.json* ./
RUN if [ -f package-lock.json ]; then npm ci; else npm install; fi

# Copy source and build
COPY . .
# Optional API override, e.g. --build-arg VITE_API_BASE_URL=https://host/api
ARG VITE_API_BASE_URL
RUN npm run build

# ---------- Runtime stage (Nginx) ----------
FROM nginx:1.27-alpine AS runtime

# SPA-friendly Nginx config (React Router)
COPY nginx/default.conf /etc/nginx/conf.d/default.conf

# Static assets
COPY --from=build /app/dist /usr/share/nginx/html

EXPOSE 80

# Optional: basic container healthcheck
HEALTHCHECK --interval=30s --timeout=5s --start-period=10s --retries=3 \
  CMD wget -qO- http://127.0.0.1/ > /dev/null || exit 1

CMD ["nginx", "-g", "daemon off;"]
