using ToDoApi.Constants;
using ToDoApi.Dto;

namespace ToDoApi.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskDto>> GetTasksByAssigneeIdAsync(string assigneeId);
        Task<TaskDto> AddTaskAsync(TaskDto task);
        Task<TaskDto> UpdateTaskAsync(TaskDto task);
        Task<TaskDto> DeleteTaskAsync(int id);
        Task<List<TaskDto>> GetTasksByStatusAsync(Status status);
        bool TaskExists(int id);
    }
}
