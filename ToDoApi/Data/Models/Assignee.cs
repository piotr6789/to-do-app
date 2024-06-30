namespace ToDoApi.Data.Models
{
    public class Assignee
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public ICollection<Task> Tasks { get; set; }
    }
}
