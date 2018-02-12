using DotNetCoreAsysnSample.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreAsysnSample.Repository
{
    /// <summary>
    /// Build dbContext using EF core
    /// </summary>
    public class CustomersDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        public CustomersDbContext(DbContextOptions<CustomersDbContext> options) : base(options) { }
    }
}
