using ToDoApi.Dto;

namespace ToDoApi.Services
{
    public interface IAssigneeService
    {
        Task<IEnumerable<AssigneeDto>> GetAssignees();
    }
}
