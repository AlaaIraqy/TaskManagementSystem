 namespace  TaskManagementSystem.Models
{
  public class Task
  {
      public int Id { get; set; }
      public string Title { get; set; }
      public string Description { get; set; }
      public DateTime DueDate { get; set; }
      public int AssigneeId { get; set; }
      public string Status { get; set; }
      public User Assignee { get; set; }
      public List<TaskDependency> Dependencies { get; set; }
  }
}