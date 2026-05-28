using Microsoft.EntityFrameworkCore;
using MOLEX_interview_todoapp.Domain.DBconfig;
using MOLEX_interview_todoapp.Domain.Repositories;
using MOLEX_interview_todoapp.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<TodoAppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowReactApp",
        policy =>
        {
            policy.WithOrigins("https://localhost:51262").AllowAnyHeader().AllowAnyMethod();
        }
    );
});

builder.Services.AddScoped<ITodoServices, TodoServices>();
builder.Services.AddScoped<ITodoRepository, TodoRepository>();

var app = builder.Build();

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowReactApp");
app.UseAuthorization();

app.MapControllers();

app.Run();
