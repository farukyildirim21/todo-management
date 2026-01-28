import TodoItem from "./TodoItem";

const TodoList = ({ todos, onComplete, onCancel }) => {
    if (!todos || todos.length === 0) {
        return <p>No todos yet.</p>;
    }

    return (
        <div>
            {todos.map((todo) => (
                <TodoItem
                    key={todo.id}
                    todo={todo}
                    onComplete={onComplete}
                    onCancel={onCancel}
                />
            ))}
        </div>
    );
};

export default TodoList;
