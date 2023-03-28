using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using ExploreDotnet.Infrastructure.Data.Context;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ExploreDotnet.Core.Entities;

namespace ExploreDotnet.API.Policies
{
    public static class AllowRoleUserPolicy
    {
        public class AllowRoleUserRequirement : IAuthorizationRequirement
        {
            public async Task<bool> Pass(DatabaseContext context, long userId)
            {
                var userRoles = await context
                    .Set<TenantUser>()
                    .Where(e => e.UserId == userId)
                    .ToListAsync();

                return userRoles.Any(e => e.Role == "user");
            }
        }

        public class AllowRoleUserHandler : AuthorizationHandler<AllowRoleUserRequirement>
        {
            private readonly DatabaseContext _dbContext;

            public AllowRoleUserHandler(DatabaseContext dbContext)
            {
                _dbContext = dbContext;
            }

            protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AllowRoleUserRequirement requirement)
            {
                var userIdClaim = context.User?.Claims.FirstOrDefault(c => c.Type == "userId");
                if (userIdClaim is null)
                {
                    context.Fail();
                    return;
                }

                var userId = Convert.ToInt64(userIdClaim.Value);

                var isAuthorized = await requirement.Pass(_dbContext, userId);

                if (!isAuthorized)
                {
                    context.Fail();
                    return;
                }

                context.Succeed(requirement);
                return;
            }
        }
    }
}