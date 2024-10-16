using Microsoft.EntityFrameworkCore;
using WarsztatAPI.Models;

namespace warsztat.Data
{
    public class warsztatDbContext : DbContext
    {
        public warsztatDbContext(DbContextOptions<warsztatDbContext> options) : base(options) { }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Worker> Workers { get; set; }

        public DbSet<Workshop> Workshops { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>().ToTable("Cars");
            modelBuilder.Entity<Worker>().ToTable("Worker");
            modelBuilder.Entity<Workshop>().ToTable("Workshop");
        }
    }
}
