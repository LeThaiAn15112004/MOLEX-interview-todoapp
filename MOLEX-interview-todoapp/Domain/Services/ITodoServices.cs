using MOLEX_interview_todoapp.Domain.Models;

namespace MOLEX_interview_todoapp.Domain.Services
{
    public interface ITodoServices
    {
        Task<IReadOnlyList<Todo>> GetAllAsync();
        Task<Todo?> GetByIdAsync(int id);
        Task<Todo> AddAsync(Todo todo);
        Task<bool> UpdateAsync(Todo todo);
        Task<bool> DeleteAsync(int id);
    }
}
