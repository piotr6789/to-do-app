using Microsoft.EntityFrameworkCore;
using ToDoApi.Data.Models;

namespace ToDoApi.Infrastructure.Data
{
    public class ToDoDbContext : DbContext
    {
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options)
        : base(options)
        {
        }

        public DbSet<ToDoApi.Data.Models.Task> Tasks { get; set; }
        public DbSet<Assignee> Assignees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoApi.Data.Models.Task>()
                .HasOne(t => t.Assignee)
                .WithMany(a => a.Tasks)
                .HasForeignKey(t => t.AssigneeId)
                .IsRequired();
        }
    }
}
