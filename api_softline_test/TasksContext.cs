using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace api_softline_test
{
    public class TasksContext : DbContext
    {
        public DbSet<TaskDb> Tasks { get; set; }
        public DbSet<Status> Statuses { get; set; }

        public TasksContext()
        {
            //Database.EnsureCreated();
        }

        public TasksContext(DbContextOptions<TasksContext> options)
          : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Status>().HasData(
                new Status { Status_ID = 1, Status_name = "Создана" },
                new Status { Status_ID = 2, Status_name = "В работе" },
                new Status { Status_ID = 3, Status_name = "Завершена" }
            );

            base.OnModelCreating(modelBuilder);
        }

    }
}
