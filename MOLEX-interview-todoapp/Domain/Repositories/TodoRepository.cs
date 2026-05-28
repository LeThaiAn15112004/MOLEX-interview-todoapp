using Microsoft.EntityFrameworkCore;
using MOLEX_interview_todoapp.Domain.DBconfig;
using MOLEX_interview_todoapp.Domain.Models;

namespace MOLEX_interview_todoapp.Domain.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoAppDbContext _context;

        public TodoRepository(TodoAppDbContext context)
        {
            _context = context;
        }

        public async Task<Todo> AddAsync(Todo todo)
        {
            try
            {
                _context.Todos.Add(todo);
                await _context.SaveChangesAsync();
                return todo;
            }
            catch
            {
                return new Todo();
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var existing = await _context.Todos.FirstOrDefaultAsync(t => t.Id == id);
                if (existing is null)
                {
                    return false;
                }

                _context.Todos.Remove(existing);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IReadOnlyList<Todo>> GetAllAsync()
        {
            try
            {
                return await _context.Todos.AsNoTracking().ToListAsync();
            }
            catch
            {
                return Array.Empty<Todo>();
            }
        }

        public async Task<Todo?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Todos.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(Todo todo)
        {
            try
            {
                var existing = await _context.Todos.FirstOrDefaultAsync(t => t.Id == todo.Id);
                if (existing is null)
                {
                    return false;
                }

                existing.Title = todo.Title;
                existing.Completed = todo.Completed;

                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
