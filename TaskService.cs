using TaskManagementSystem.Models;
using TaskManagementSystem.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task = TaskManagementSystem.Models.Task;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementSystem.Models;

public class TaskService : ITaskService
{
    private readonly TaskManagementContext _context;

    public TaskService(TaskManagementContext context)
    {
        _context = context;
    }

    public async Task<Task> CreateTask(TaskCreateDto taskDto)
    {
        var task = new Task
        {
            Title = taskDto.Title,
            Description = taskDto.Description,
            DueDate = taskDto.DueDate,
            AssigneeId = taskDto.AssigneeId,
            Status = "Pending"
        };

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        return task;
    }

    public async Task<IEnumerable<Task>> GetTasks(TaskQueryParameters queryParameters)
    {
        var query = _context.Tasks.AsQueryable();

        if (queryParameters.Status != null)
            query = query.Where(t => t.Status == queryParameters.Status);

        if (queryParameters.AssigneeId != null)
            query = query.Where(t => t.AssigneeId == queryParameters.AssigneeId);

        if (queryParameters.DueDateStart != null)
            query = query.Where(t => t.DueDate >= queryParameters.DueDateStart);

        if (queryParameters.DueDateEnd != null)
            query = query.Where(t => t.DueDate <= queryParameters.DueDateEnd);

        return await query.ToListAsync();
    }

    public async Task<Task> GetTaskById(int id)
    {
        return await _context.Tasks.Include(t => t.Dependencies)
                                   .ThenInclude(d => d.DependentTask)
                                   .SingleOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Task> UpdateTask(int id, TaskUpdateDto taskDto)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
            return null;

        task.Title = taskDto.Title;
        task.Description = taskDto.Description;
        task.DueDate = taskDto.DueDate;
        task.AssigneeId = taskDto.AssigneeId;

        await _context.SaveChangesAsync();

        return task;
    }

    public async Task<Task> UpdateTaskStatus(int id, TaskStatusUpdateDto statusDto)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
            return null;

        task.Status = statusDto.Status;
        await _context.SaveChangesAsync();

        return task;
    }
}
