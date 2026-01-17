using Microsoft.EntityFrameworkCore;
using webtest.Models;

namespace webtest.DAL.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
            
        }

        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Speciality> Specialities { get; set; }
    }
}
