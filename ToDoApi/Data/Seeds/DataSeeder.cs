using ToDoApi.Constants;
using ToDoApi.Infrastructure.Data;

namespace ToDoApi.DataSeeder
{
    public class DatabaseSeeder
    {
        private readonly ToDoDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public DatabaseSeeder(ToDoDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task SeedAsync()
        {
            var apiUrl = _configuration["ToDoAppUrl"];
            var apiKey = _configuration["ToDoAppApiKey"];
            var fullUrl = $"{apiUrl}?apiKey={apiKey}";

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(fullUrl);

                if (response.IsSuccessStatusCode)
                {
                    var assignees = await response.Content.ReadFromJsonAsync<List<Data.Models.Assignee>>();

                    _dbContext.Tasks.RemoveRange(_dbContext.Tasks);
                    _dbContext.Assignees.RemoveRange(_dbContext.Assignees);
                    await _dbContext.SaveChangesAsync();

                    _dbContext.Assignees.AddRange(assignees);
                    await _dbContext.SaveChangesAsync();

                    var tasks = new List<Data.Models.Task>();
                    var random = new Random();
                    var numbersToGenerateEstimateTime = Enumerable.Range(30, 2880 / 30)
                                                                  .Select(x => x * 30)
                                                                  .ToList();
                    foreach (var assignee in assignees)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            var status = random.Next(0, 2) == 0 ? Status.TODO : Status.DONE;
                            var randomEstimateNumbersIndex = random.Next(numbersToGenerateEstimateTime.Count);
                            var task = new Data.Models.Task
                            {
                                AssigneeId = assignee.Id,
                                Title = $"Task {i + 1} for {assignee.Name}",
                                Description = $"Task description {i + 1} for {assignee.Name}",
                                Status = status,
                                EstimateTime = numbersToGenerateEstimateTime[randomEstimateNumbersIndex]
                            };
                            tasks.Add(task);
                        }
                    }

                    _dbContext.Tasks.AddRange(tasks);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    Console.WriteLine($"Failed to fetch assignees. Status code: {response.StatusCode}");
                }
            }
        }
    }
}
