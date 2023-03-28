using Microsoft.EntityFrameworkCore;

namespace ExploreDotnet.Infrastructure.Data.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) 
        {
        }
    }
}