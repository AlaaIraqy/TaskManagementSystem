using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers{
    [ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [Authorize(Policy = "ManagerOnly")]
    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] TaskCreateDto taskDto)
    {
        var task = await _taskService.CreateTask(taskDto);
        return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetTasks([FromQuery] TaskQueryParameters queryParameters)
    {
        var tasks = await _taskService.GetTasks(queryParameters);
        return Ok(tasks);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTaskById(int id)
    {
        var task = await _taskService.GetTaskById(id);
        if (task == null)
            return NotFound();

        return Ok(task);
    }

    [Authorize(Policy = "ManagerOnly")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskUpdateDto taskDto)
    {
        var updatedTask = await _taskService.UpdateTask(id, taskDto);
        if (updatedTask == null)
            return NotFound();

        return Ok(updatedTask);
    }

    [Authorize(Policy = "UserOnly")]
    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateTaskStatus(int id, [FromBody] TaskStatusUpdateDto statusDto)
    {
        var updatedTask = await _taskService.UpdateTaskStatus(id, statusDto);
        if (updatedTask == null)
            return NotFound();

        return Ok(updatedTask);
    }
}
}