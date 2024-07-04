using Microsoft.AspNetCore.Mvc;
using ToDoApi.Dto;
using ToDoApi.Services;

namespace ToDoApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class AssigneeController : Controller
    {
        private readonly IAssigneeService _assigneeService;
        private readonly ILogger<TasksController> _logger;

        public AssigneeController(IAssigneeService assigneeService, ILogger<TasksController> logger)
        {
            _assigneeService = assigneeService;
            _logger = logger;
        }

        [HttpGet("assignees")]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetAssignees()
        {
            try
            {
                var assignees = await _assigneeService.GetAssignees();

                if (assignees == null || !assignees.Any())
                {
                    _logger.LogWarning($"No assignees found");
                    return NotFound("No assignees found");
                }

                return Ok(assignees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting assignees.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
