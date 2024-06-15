using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskManagementSystem.Models;
using TaskManagementSystem.Controllers;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// DB configuration
builder.Services.AddDbContext<TaskManagementContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
options.TokenValidationParameters = new TokenValidationParameters
{
ValidateIssuer = true,
ValidateAudience = true,
ValidateLifetime = true,
ValidateIssuerSigningKey = true,
ValidIssuer = builder.Configuration["Jwt"],
ValidAudience = builder.Configuration["Jwt"],
IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt"]))
};
});

builder.Services.AddAuthorization(options =>
{
options.AddPolicy("ManagerOnly", policy => policy.RequireRole("Manager"));
options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
});

void SeedDatabase(TaskManagementContext context)
{
    if (!context.Users.Any())
    {
        var user = new User
        {
            Username = "alaairaqy",
            Role = "Manager",
            Password = "123456789"
        };
        
        context.Users.Add(user);
        context.SaveChanges();
    }
}


// Register Swagger
builder.Services.AddSwaggerGen();

// Add your services to the container.
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
app.UseSwagger();
app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();