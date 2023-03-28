using ExploreDotnet.Infrastructure.Data.Context;

namespace ExploreDotnet.Infrastructure.Data.Initializer
{
    public static class DbInitializer
    {
        public static void SeedData(DatabaseContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}