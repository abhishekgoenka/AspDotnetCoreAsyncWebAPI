using DotNetCoreAsysnSample.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreAsysnSample.Repository
{
    /// <summary>
    ///     Build dbContext using EF core
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
    }
}