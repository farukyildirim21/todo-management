const TodoItem = ({ todo, onComplete, onCancel }) => {
    const isCreated = todo.status === "Created";

    return (
        <div>
      <span>
        {todo.title} ({todo.status})
      </span>

            {isCreated && (
                <>
                    <button onClick={() => onComplete(todo.id)}>
                        Complete
                    </button>
                    <button onClick={() => onCancel(todo.id)}>
                        Cancel
                    </button>
                </>
            )}
        </div>
    );
};

export default TodoItem;
