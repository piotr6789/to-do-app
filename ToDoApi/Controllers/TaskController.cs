using Microsoft.AspNetCore.Mvc;
using ToDoApi.Constants;
using ToDoApi.Dto;
using ToDoApi.Services;

namespace ToDoApi.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly ILogger<TasksController> _logger;

        public TasksController(ITaskService taskService, ILogger<TasksController> logger)
        {
            _taskService = taskService;
            _logger = logger;
        }

        [HttpGet("assignee/{assigneeId}")]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasksByAssigneeId(string assigneeId)
        {
            try
            {
                var tasks = await _taskService.GetTasksByAssigneeIdAsync(assigneeId);

                if (tasks == null || !tasks.Any())
                {
                    _logger.LogWarning($"No tasks found for assigneeId: {assigneeId}");
                    return NotFound("No tasks found");
                }

                return Ok(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting tasks for assigneeId: {assigneeId}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("tasksByStatus/{status}")]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasksByStatus(Status status)
        {
            try
            {
                var tasks = await _taskService.GetTasksByStatusAsync(status);
                if (tasks == null || !tasks.Any())
                {
                    _logger.LogWarning($"No tasks found for status: {status}");
                    return NotFound("No tasks found");
                }

                return Ok(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting tasks for status: {status}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TaskDto>> CreateTask(TaskDto task)
        {
            try
            {
                var createdTask = await _taskService.CreateTaskAsync(task);
                return CreatedAtAction(nameof(GetTasksByAssigneeId), createdTask);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the task");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, TaskDto task)
        {
            try
            {
                var success = await _taskService.UpdateTaskAsync(task);
                if (!success)
                {
                    return NotFound("Task not found");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating task with Id: {id}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            try
            {
                var success = await _taskService.DeleteTaskAsync(id);
                if (!success)
                {
                    return NotFound("Task not found");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting task with Id: {id}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
