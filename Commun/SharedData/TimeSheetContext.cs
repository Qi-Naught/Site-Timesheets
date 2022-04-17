using Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Common.Data
{
    public class TimeSheetContext : DbContext
    {
        public TimeSheetContext(DbContextOptions<TimeSheetContext> options) : base(options)
        {
        }

        public DbSet<TimeSheet> TimeSheets { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TimeSheet>().ToTable("TimeSheet");
        }
    }
}