using Microsoft.EntityFrameworkCore;

namespace Pulsar.Admin.Api.Data.DbContexts
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
    }
}