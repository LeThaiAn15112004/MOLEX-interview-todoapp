using MOLEX_interview_todoapp.Domain.Models;
using MOLEX_interview_todoapp.Domain.Repositories;

namespace MOLEX_interview_todoapp.Domain.Services
{
    public class TodoServices : ITodoServices
    {
        private readonly ITodoRepository _repository;

        public TodoServices(ITodoRepository repository)
        {
            _repository = repository;
        }

        public Task<Todo> AddAsync(Todo todo)
        {
            return _repository.AddAsync(todo);
        }

        public Task<bool> DeleteAsync(int id)
        {
            return _repository.DeleteAsync(id);
        }

        public Task<IReadOnlyList<Todo>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task<Todo?> GetByIdAsync(int id)
        {
            return _repository.GetByIdAsync(id);
        }

        public Task<bool> UpdateAsync(Todo todo)
        {
            return _repository.UpdateAsync(todo);
        }
    }
}
