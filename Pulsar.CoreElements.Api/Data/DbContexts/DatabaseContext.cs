using Microsoft.EntityFrameworkCore;
using Pulsar.CoreElements.Api.Models.CoreElements;

namespace Pulsar.CoreElements.Api.Data.DbContexts
{
    public class DatabaseContext : DbContext
    {
        public DbSet<CoreElement> CoreElements { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
    }
}