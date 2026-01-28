// src/models//T
/**
 * 
 * Backend tarafından dönen enum değerleriyle birebir olmalıdır.
 */
export const TodoStatus = {
    Created: "Created",
    Completed: "Completed",
    Cancelled: "Cancelled",
};

/**
 * 
 * Frontend tarafında bi
 */
export function Todo(id, title, status) {
    return {
        id,
        title,
        status,
    };
}
