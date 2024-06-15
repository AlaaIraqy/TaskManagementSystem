using TaskManagementSystem.Models;
using TaskManagementSystem.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task = TaskManagementSystem.Models.Task;

public interface ITaskService
{
    Task<Task> CreateTask(TaskCreateDto taskDto);
    Task<IEnumerable<Task>> GetTasks(TaskQueryParameters queryParameters);
    Task<Task> GetTaskById(int id);
    Task<Task> UpdateTask(int id, TaskUpdateDto taskDto);
    Task<Task> UpdateTaskStatus(int id, TaskStatusUpdateDto statusDto);
}
