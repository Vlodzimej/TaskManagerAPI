using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagerAPI.Models
{
    public class TaskManagerContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }
        public TaskManagerContext(DbContextOptions<TaskManagerContext> options) : base(options) { }
    }
}
