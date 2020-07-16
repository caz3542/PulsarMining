using Microsoft.EntityFrameworkCore;

namespace Pulsar.Inventory.Api.Data.DbContexts
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
    }
}