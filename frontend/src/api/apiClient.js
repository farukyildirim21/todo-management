import axios from "axios";

/**
 * Frontend -> Backend communication MUST go through KrakenD Gateway.
 * Frontend does NOT know userId.
 * User context is resolved via Basic Auth at Gateway/Backend.
 */

/**
 * DEMO BASIC AUTH
 * Change only this value to simulate different users.
 *
 * Examples:
 *  - "user1:1234"
 *  - "user2:1234"
 */
const BASIC_AUTH_CREDENTIALS = "emre:123";

/**
 * Base64 encoded Authorization header
 */
const basicAuthHeader = {
    Authorization: "Basic " + btoa(BASIC_AUTH_CREDENTIALS),
};

/**
 * Axios instance configured for KrakenD Gateway
 */
const api = axios.create({
    baseURL: "http://localhost:8081",
    timeout: 5000,
    headers: {
        "Content-Type": "application/json",
        ...basicAuthHeader,
    },
});

/* =========
   QUERIES
   ========= */

/**
 * Fetch todos for current user.
 * Supports:
 *  - Direct array response
 *  - KrakenD wrapped response: { collection: [] }
 */
export const getTodos = async () => {
    const response = await api.get("/todos");
    const data = response?.data;

    if (Array.isArray(data)) {
        return data;
    }

    if (data && Array.isArray(data.collection)) {
        return data.collection;
    }

    // Defensive fallback
    return [];
};

export const getTodoById = async (id) => {
    const response = await api.get(`/todos/${id}`);
    return response.data;
};

/* =========
   COMMANDS
   ========= */

/**
 * Create a newtodo
 */
export const createTodo = async (title) => {
    await api.post("/todos", { title });
};

/**
 * Marktodo as completed
 */
export const completeTodo = async (id) => {
    await api.post(`/todos/${id}/complete`);
};

/**
 * Canceltodo.
 */
export const cancelTodo = async (id) => {
    await api.post(`/todos/${id}/cancel`);
};
