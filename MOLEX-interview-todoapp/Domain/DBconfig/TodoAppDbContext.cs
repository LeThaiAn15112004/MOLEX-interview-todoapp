using Microsoft.EntityFrameworkCore;
using MOLEX_interview_todoapp.Domain.Models;

namespace MOLEX_interview_todoapp.Domain.DBconfig
{
    public class TodoAppDbContext : DbContext
    {
        public TodoAppDbContext(DbContextOptions<TodoAppDbContext> options)
            : base(options) { }

        public DbSet<Todo> Todos => Set<Todo>();
        public DbSet<Item> Items => Set<Item>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var ConnectionString = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build()
                    .GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }
    }
}
