using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WarsztatAPI.Models;

namespace WarsztatAPI.Data
{
    public class warsztatDbContext : DbContext
    {
        public warsztatDbContext(DbContextOptions<warsztatDbContext> options) : base(options) { }

        public DbSet<Car> Cars { get; set; }
    }
}
