using Microsoft.EntityFrameworkCore;
using MOLEX_interview_todoapp.Domain.DBconfig;
using MOLEX_interview_todoapp.Domain.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TodoAppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddScoped<ITodoRepository, TodoRepository>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
