import TodoPage from "./pages/TodoPage";

function App() {
    return (
        <div className="app-container">
            <header className="app-header">
                <h1>Mini Todo Management System</h1>
                <p>Manage your todos in a simple and clean way</p>
            </header>

            <main className="app-content">
                <TodoPage />
            </main>
        </div>
    );
}

export default App;
