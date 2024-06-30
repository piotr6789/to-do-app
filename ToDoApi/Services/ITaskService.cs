using ToDoApi.Constants;
using ToDoApi.Dto;

namespace ToDoApi.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetTasksByAssigneeIdAsync(string assigneeId);
        Task<TaskDto> CreateTaskAsync(TaskDto task);
        Task<bool> UpdateTaskAsync(TaskDto task);
        Task<bool> DeleteTaskAsync(int id);
        Task<List<TaskDto>> GetTasksByStatusAsync(Status status);
        bool TaskExists(int id);
    }
}
