using ToDoApi.Dto;
using ToDoApi.Repositories;

namespace ToDoApi.Services
{
    public class AssigneeService : IAssigneeService
    {
        private readonly IAssigneeRepository _assigneeRepository;
        private readonly ILogger<TaskService> _logger;

        public AssigneeService(IAssigneeRepository assigneeRepository, ILogger<TaskService> logger)
        {
            _assigneeRepository = assigneeRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<AssigneeDto>> GetAssignees()
        {
            try
            {
                var assignees = await _assigneeRepository.GetAssignees();
                return assignees;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting assignees.");
                throw;
            }
        }
    }
}
