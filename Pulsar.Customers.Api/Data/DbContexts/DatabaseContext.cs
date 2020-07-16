using Microsoft.EntityFrameworkCore;
using Pulsar.Customers.Api.Models.Customers;

namespace Pulsar.Customers.Api.Data.DbContexts
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
                
        }
    }
}