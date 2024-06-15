using TaskManagementSystem.Models;
using TaskManagementSystem.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task = TaskManagementSystem.Models.Task;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;


public class AuthService : IAuthService
{
    private readonly TaskManagementContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(TaskManagementContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public string Authenticate(string username, string password)
    {
        var user = _context.Users.SingleOrDefault(u => u.Username == username && u.Password == password);
        if (user == null)
            return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
