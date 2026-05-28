import { useEffect, useMemo, useState } from 'react';
import TodoCrudService from './services/todocrud.js';
import './todo.css';

interface TodoItem {
    id: number | string;
    title: string;
    completed: boolean;
}


const Todo = () => {
    const [items, setItems] = useState<TodoItem[]>([]);
    const [title, setTitle] = useState('');
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState('');
    const todoService = useMemo(() => new TodoCrudService(), []);

    const remaining = useMemo(
        () => items.filter((item) => !item.completed).length,
        [items]
    );

    useEffect(() => {
        let isMounted = true;

        const loadTodos = async () => {
            setLoading(true);
            setError('');
            try {
                const data = await todoService.getTodos();
                if (!isMounted) {
                    return;
                }
                setItems(Array.isArray(data) ? data : []);
            } catch (err) {
                if (!isMounted) {
                    return;
                }
                setError(err instanceof Error ? err.message : 'Failed to load todos.');
            } finally {
                if (!isMounted) {
                    return;
                }
                setLoading(false);
            }
        };

        loadTodos();

        return () => {
            isMounted = false;
        };
    }, [todoService]);

    const handleSubmit = async (event) => {
        event.preventDefault();
        const trimmedTitle = title.trim();
        if (!trimmedTitle) {
            return;
        }

        setError('');
        try {
            const created = await todoService.createTodo(trimmedTitle);
            setItems((prev) => [created, ...prev]);
            setTitle('');
        } catch (err) {
            setError(err instanceof Error ? err.message : 'Failed to create todo.');
        }
    };

    const handleToggle = async (item: TodoItem) => {
        const nextCompleted = !item.completed;
        setError('');
        try {
            await todoService.updateTodo(item.id, { completed: nextCompleted });
            setItems((prev) =>
                prev.map((entry) => (entry.id === item.id ? { ...entry, completed: nextCompleted } : entry))
            );
        } catch (err) {
            setError(err instanceof Error ? err.message : 'Failed to update todo.');
        }
    };

    const handleDelete = async (item: TodoItem) => {
        setError('');
        try {
            await todoService.deleteTodo(item.id);
            setItems((prev) => prev.filter((entry) => entry.id !== item.id));
        } catch (err) {
            setError(err instanceof Error ? err.message : 'Failed to delete todo.');
        }
    };

    return (
        <main className="todo-page">
            <header className="todo-header">
                <p className="todo-eyebrow">Todo</p>
                <h1>Todo App</h1>
                <p className="todo-subtitle">
                    {loading ? 'Loading...' : `${remaining} remaining / ${items.length} total`}
                </p>
            </header>

            <section className="todo-card">
                <form className="todo-form" onSubmit={handleSubmit}>
                    <input
                        className="todo-input"
                        value={title}
                        onChange={(event) => setTitle(event.target.value)}
                        placeholder="Add a new task"
                        aria-label="Todo title"
                    />
                    <button className="todo-button" type="submit">
                        Add
                    </button>
                </form>

                {error ? <p className="todo-error">{error}</p> : null}

                <ul className="todo-list">
                    {items.length === 0 && !loading ? (
                        <li className="todo-empty">No todos yet. Add one above.</li>
                    ) : null}
                    {items.map((item) => (
                        <li key={item.id} className="todo-item">
                            <label className="todo-item-label">
                                <input
                                    type="checkbox"
                                    checked={item.completed}
                                    onChange={() => handleToggle(item)}
                                />
                                <span className={item.completed ? 'todo-title todo-title--done' : 'todo-title'}>
                                    {item.title}
                                </span>
                            </label>
                            <button
                                className="todo-delete"
                                type="button"
                                onClick={() => handleDelete(item)}
                            >
                                Delete
                            </button>
                        </li>
                    ))}
                </ul>
            </section>
        </main>
    );
};

export default Todo;
