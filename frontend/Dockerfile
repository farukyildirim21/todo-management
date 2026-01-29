# ----------------------------------
# Frontend (React + Vite) - Dev Mode
# ----------------------------------
FROM node:20-alpine

# Set working directory
WORKDIR /app

# Copy package files first (for cache)
COPY package.json package-lock.json* ./

# Install dependencies
RUN npm install

# Copy rest of the source
COPY . .

# Expose Vite dev server port
EXPOSE 5173

# Run Vite dev server
CMD ["npm", "run", "dev", "--", "--host", "0.0.0.0", "--port", "5173"]
