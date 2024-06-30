namespace ToDoApi.Dto
{
    public class AssigneeDto
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public List<TaskDto> Tasks { get; set; }
    }
}
