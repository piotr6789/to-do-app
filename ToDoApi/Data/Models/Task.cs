using ToDoApi.Constants;

namespace ToDoApi.Data.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }

        public string AssigneeId { get; set; }
        public Assignee Assignee { get; set; }
    }
}
