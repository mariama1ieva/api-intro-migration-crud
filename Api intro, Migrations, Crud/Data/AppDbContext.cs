
using Api_intro__Migrations__Crud.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_intro__Migrations__Crud.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }



    }
}
