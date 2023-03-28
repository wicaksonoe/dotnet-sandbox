using System.Collections.Generic;
using ExploreDotnet.Core.Entities;
using ExploreDotnet.Infrastructure.Data.Context;

namespace ExploreDotnet.Infrastructure.Data.Initializer
{
    public static class DbInitializer
    {
        public static void SeedData(DatabaseContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var users = new List<User>
            {
                new ("user1", "user1"),
            };
            context.Users.AddRange(users);

            var tenants = new List<Tenant>
            {
                new ("tenant1"),
                new ("tenant2"),
                new ("tenant3"),
            };
            context.Tenants.AddRange(tenants);

            var tenantUsers = new List<TenantUser>();
            foreach (var user in users)
            {
                tenantUsers.AddRange(new List<TenantUser>
                {
                    new (tenants[0].Id, user.Id, "admin"),
                    new (tenants[1].Id, user.Id, "user"),
                    new (tenants[2].Id, user.Id, "finance"),
                });
            }
            context.TenantUsers.AddRange(tenantUsers);

            context.SaveChanges();
        }
    }
}