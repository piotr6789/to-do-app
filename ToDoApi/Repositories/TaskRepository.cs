using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Constants;
using ToDoApi.Dto;
using ToDoApi.Infrastructure.Data;

namespace ToDoApi.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ToDoDbContext _dbContext;
        private readonly IMapper _mapper;

        public TaskRepository(ToDoDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskDto>> GetTasksByAssigneeIdAsync(string assigneeId)
        {
            var tasks = await _dbContext.Tasks
                .Where(t => t.AssigneeId == assigneeId)
                .Include(t => t.Assignee)
                .ToListAsync();

            return _mapper.Map<IEnumerable<TaskDto>>(tasks);
        }

        public async Task<TaskDto> AddTaskAsync(TaskDto taskDto)
        {
            var task = _mapper.Map<Data.Models.Task>(taskDto);
            _dbContext.Tasks.Add(task);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<TaskDto>(task);
        }

        public async Task<TaskDto> UpdateTaskAsync(TaskDto taskDto)
        {
            var task = _mapper.Map<Data.Models.Task>(taskDto);
            _dbContext.Entry(task).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<TaskDto>(task);
        }

        public async Task<TaskDto> DeleteTaskAsync(int id)
        {
            var task = await _dbContext.Tasks.FindAsync(id);
            if (task != null)
            {
                _dbContext.Tasks.Remove(task);
                await _dbContext.SaveChangesAsync();
            }
            return _mapper.Map<TaskDto>(task);
        }

        public async Task<List<TaskDto>> GetTasksByStatusAsync(string assigneeId, Status status)
        {
            var tasks = await _dbContext.Tasks
                .Where(t => t.Status == status)
                .Include(t => t.Assignee)
                .Where(a => a.Assignee.Id == assigneeId)
                .ToListAsync();

            return _mapper.Map<List<TaskDto>>(tasks);
        }

        public bool TaskExists(int id)
        {
            return _dbContext.Tasks.Any(e => e.Id == id);
        }
    }
}
