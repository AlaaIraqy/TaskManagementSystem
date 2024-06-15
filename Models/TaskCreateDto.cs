namespace  TaskManagementSystem.Models{
    public class TaskCreateDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public int AssigneeId { get; set; }
}
}