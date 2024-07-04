using ToDoApi.Constants;
using ToDoApi.Dto;
using ToDoApi.Repositories;

namespace ToDoApi.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ILogger<TaskService> _logger;

        public TaskService(ITaskRepository taskRepository, ILogger<TaskService> logger)
        {
            _taskRepository = taskRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<TaskDto>> GetTasksByAssigneeIdAsync(string assigneeId)
        {
            try
            {
                var tasks = await _taskRepository.GetTasksByAssigneeIdAsync(assigneeId);
                return tasks;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting tasks for assigneeId: {assigneeId}");
                throw;
            }
        }

        public async Task<TaskDto> CreateTaskAsync(TaskDto task)
        {
            try
            {
                var createdTask = await _taskRepository.AddTaskAsync(task);
                _logger.LogInformation($"Task created with Id: {createdTask.Id}");
                return createdTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the task");
                throw;
            }
        }

        public async Task<bool> UpdateTaskAsync(TaskDto task)
        {
            try
            {
                var success = await _taskRepository.UpdateTaskAsync(task);
                if (success == null)
                {
                    _logger.LogWarning($"Task not found: {task.Id}");
                    return false;
                }

                _logger.LogInformation($"Task updated with Id: {task.Id}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating task with Id: {task.Id}");
                throw;
            }
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            try
            {
                var deletedTask = await _taskRepository.DeleteTaskAsync(id);
                if (deletedTask == null)
                {
                    _logger.LogWarning($"Task not found: {id}");
                    return false;
                }

                _logger.LogInformation($"Task deleted with Id: {id}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting task with Id: {id}");
                throw;
            }
        }

        public bool TaskExists(int id)
        {
            return _taskRepository.TaskExists(id);
        }

        public Task<List<TaskDto>> GetTasksByStatusAsync(string assigneeId, Status status)
        {
            return _taskRepository.GetTasksByStatusAsync(assigneeId, status);
        }
    }
}
