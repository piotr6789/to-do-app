using Microsoft.AspNetCore.Mvc;
using ToDoApi.Services;

namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSheetController : ControllerBase
    {
        private readonly ITimeSheetService _timeSheetService;
        private readonly ILogger<TimeSheetController> _logger;

        public TimeSheetController(ITimeSheetService timeSheetService, ILogger<TimeSheetController> logger)
        {
            _timeSheetService = timeSheetService;
            _logger = logger;
        }

        [HttpGet("{assigneeId}/timesheet/{date}")]
        public async Task<IActionResult> GetCompletionTimeForAssignee(string assigneeId, DateTime date)
        {
            try
            {
                var completionTime = await _timeSheetService.GetCompletionTimeForAssignee(assigneeId, date);
                if (completionTime != null)
                {
                    return Ok(completionTime);
                }
                else
                {
                    return StatusCode(500, "Unable to calculate completion date.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting completion time for assignee: {assigneeId}");
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
