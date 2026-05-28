const API_BASE_URL = 'http://localhost:5029/todos';
class TodoCrudService {
    async getTodos() {
        const response = await fetch(API_BASE_URL, {
            method: 'GET'
        });
        if (!response.ok) {
            throw new Error('Failed to load todos.');
        }
        return await response.json();
    }

    async getTodo(id) {
        const response = await fetch(`${API_BASE_URL}/${id}`, {
            method: 'GET'
        });
        if (!response.ok) {
            throw new Error('Failed to load todo.');
        }
        return await response.json();
    }

    async createTodo(title) {
        const response = await fetch(API_BASE_URL, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ title })
        });
        if (!response.ok) {
            throw new Error('Failed to create todo.');
        }
        return await response.json();
    }

    async updateTodo(id, updates) {
        const response = await fetch(`${API_BASE_URL}/${id}`, {
            method: 'PATCH',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(updates)
        });
        if (!response.ok) {
            throw new Error('Failed to update todo.');
        }
        return true;
    }

    async deleteTodo(id) {
        const response = await fetch(`${API_BASE_URL}/${id}`, {
            method: 'DELETE'
        });
        if (!response.ok) {
            throw new Error('Failed to delete todo.');
        }
        return true;
    }
}

export default TodoCrudService;