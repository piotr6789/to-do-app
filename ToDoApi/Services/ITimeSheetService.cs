using ToDoApi.Dto;

namespace ToDoApi.Services
{
    public interface ITimeSheetService
    {
        Task<IEnumerable<TimeSheetDto>> GetTimeSheetForAssignee(string assigneeId, string date);
    }
}
