  namespace  TaskManagementSystem.Models
{
  public class TaskDependency
  {
      public int Id { get; set; }
      public int TaskId { get; set; }
      public int DependentTaskId { get; set; }
      public Task Task { get; set; }
      public Task DependentTask { get; set; }
  }
}