using TaskManagementSystem.Models;
using TaskManagementSystem.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task = TaskManagementSystem.Models.Task;

public interface IAuthService
{
    string Authenticate(string username, string password);
}
