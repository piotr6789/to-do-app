using ToDoApi.Dto;

namespace ToDoApi.Repositories
{
    public interface IAssigneeRepository
    {
        Task<IEnumerable<AssigneeDto>> GetAssignees();
    }
}
