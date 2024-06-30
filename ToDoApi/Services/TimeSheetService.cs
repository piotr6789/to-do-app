using ToDoApi.Dto;

namespace ToDoApi.Services
{
    public class TimeSheetService : ITimeSheetService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<TimeSheetService> _logger;

        public TimeSheetService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<TimeSheetService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
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
