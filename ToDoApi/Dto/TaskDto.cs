using ToDoApi.Constants;

namespace ToDoApi.Dto
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public int EstimateTime { get; set; }

        public string AssigneeId { get; set; }
    }
}
