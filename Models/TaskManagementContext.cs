using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace  TaskManagementSystem.Models
{
  public class TaskManagementContext : DbContext
  {
      public TaskManagementContext(DbContextOptions<TaskManagementContext> options)
          : base(options)
      {
      }

      public DbSet<Task> Tasks { get; set; }
      public DbSet<User> Users { get; set; }
      public DbSet<TaskDependency> TaskDependencies { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
          modelBuilder.Entity<Task>()
              .HasMany(t => t.Dependencies)
              .WithOne(d => d.Task)
              .HasForeignKey(d => d.TaskId);

          modelBuilder.Entity<TaskDependency>()
              .HasOne(td => td.DependentTask)
              .WithMany()
              .HasForeignKey(td => td.DependentTaskId);
        
         modelBuilder.Entity<User>()
            .HasMany(u => u.Tasks)
            .WithOne(t => t.Assignee)
            .HasForeignKey(t => t.AssigneeId);
      }
  }
}