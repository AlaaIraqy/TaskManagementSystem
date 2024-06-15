namespace  TaskManagementSystem.Models{
    public class TaskQueryParameters
{
    public string Status { get; set; }
    public int? AssigneeId { get; set; }
    public DateTime? DueDateStart { get; set; }
    public DateTime? DueDateEnd { get; set; }
}

}