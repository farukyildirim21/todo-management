import { useState } from "react";

const TodoForm = ({ onCreate }) => {
    const [title, setTitle] = useState("");

    const handleSubmit = (e) => {
        e.preventDefault();

        if (!title.trim()) return;

        onCreate(title);
        setTitle("");
    };

    return (
        <form onSubmit={handleSubmit}>
            <input
                type="text"
                value={title}
                onChange={(e) => setTitle(e.target.value)}
                placeholder="New todo"
            />
            <button type="submit">Add</button>
        </form>
    );
};

export default TodoForm;
