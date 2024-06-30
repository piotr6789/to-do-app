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
        public async Task<IActionResult> GetTimeSheetForAssignee(string assigneeId, string date)
        {
            try
            {
                var timeSheets = await _timeSheetService.GetTimeSheetForAssignee(assigneeId, date);

                if (timeSheets != null)
                {
                    return Ok(timeSheets);
                }
                else
                {
                    return StatusCode(500, "Error while retrieving time sheets.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
