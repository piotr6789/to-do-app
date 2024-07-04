using ToDoApi.Dto;

namespace ToDoApi.Services
{
    public class TimeSheetService : ITimeSheetService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ITaskService _taskService;
        private readonly ILogger<TimeSheetService> _logger;

        public TimeSheetService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ITaskService taskService, ILogger<TimeSheetService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _taskService = taskService;
            _logger = logger;
        }

        public async Task<DateTime> GetCompletionTimeForAssignee(string assigneeId, DateTime startDate)
        {
            try
            {
                var assigneeTasks = await _taskService.GetTasksByAssigneeIdAsync(assigneeId);
                var totalMinutes = assigneeTasks.Sum(t => t.EstimateTime);

                var date = startDate;
                var timeSheetDate = startDate;
                while (totalMinutes > 0)
                {
                    var timeSheet = await GetTimeSheetForAssignee(assigneeId, timeSheetDate.ToString("yyyy-MM"));
                    if (timeSheet != null)
                    {
                        foreach (var entry in timeSheet)
                        {
                            var workHours = entry.Hours.Split(',').Select(h => h.Split('-')).ToList();
                            foreach (var hourRange in workHours)
                            {
                                if (hourRange.Length == 2)
                                {
                                    var startHour = int.Parse(hourRange[0]);
                                    var endHour = int.Parse(hourRange[1]);
                                    var availableMinutes = (endHour - startHour + 1) * 60;

                                    if (availableMinutes >= totalMinutes)
                                    {
                                        date = date.AddHours(startHour).AddMinutes(totalMinutes);
                                        return date;
                                    }

                                    totalMinutes -= availableMinutes;
                                    date = date.AddDays(1);
                                }
                            }
                        }
                    }
                }

                return date;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while getting completion time for assignee: {assigneeId}", ex);
                throw new Exception($"An error occurred: {ex.Message}", ex);
            }
        }


        public async Task<IEnumerable<TimeSheetDto>> GetTimeSheetForAssignee(string assigneeId, string date)
        {
            try
            {
                var apiKey = _configuration["ToDoAppApiKey"];
                var apiUrl = _configuration["ToDoAppUrl"];

                var formattedDate = DateTime.ParseExact(date, "yyyy-MM", null);

                var url = $"{apiUrl}/{assigneeId}/timesheet/{formattedDate:yyyy-MM}?apikey={apiKey}";

                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var timeSheets = await response.Content.ReadFromJsonAsync<List<TimeSheetDto>>();
                    return timeSheets;
                }
                else
                {
                    _logger.LogError($"External API request failed with status code: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving time sheets: {ex.Message}", ex);
                throw new Exception($"Error while retrieving time sheets: {ex.Message}", ex);
            }
        }
    }
}
