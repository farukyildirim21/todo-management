import { useEffect, useState } from "react";

import {
    getTodos,
    createTodo,
    completeTodo,
    cancelTodo,
} from "../api/apiClient";

import TodoForm from "../components/TodoForm";
import TodoList from "../components/TodoList";

const TodoPage = () => {
    const [todos, setTodos] = useState([]);

    // QUERY
    const loadTodos = async () => {
        try {
            const data = await getTodos();
            setTodos(Array.isArray(data) ? data : []);
        } catch (err) {
            console.error("Failed to load todos", err);
            setTodos([]);
        }
    };


    // PAGE LOAD
    useEffect(() => {
        loadTodos();
    }, []);

    // COMMANDS
    const handleCreate = async (title) => {
        await createTodo(title);
        await loadTodos();
    };

    const handleComplete = async (id) => {
        await completeTodo(id);
        await loadTodos();
    };

    const handleCancel = async (id) => {
        await cancelTodo(id);
        await loadTodos();
    };

    return (
        <div>
            <h1>Todo List</h1>

            <TodoForm onCreate={handleCreate} />

            <TodoList
                todos={todos}
                onComplete={handleComplete}
                onCancel={handleCancel}
            />
        </div>
    );
};

export default TodoPage;
