using System.Linq;
using System;
using ExploreDotnet.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ExploreDotnet.Core.Entities;

namespace ExploreDotnet.API.Endpoints.ActionFilters
{
    public class AllowRoleUserAttribute : IActionFilter
    {
        private readonly DatabaseContext _databaseContext;
        public AllowRoleUserAttribute(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var stringTenantId = context.HttpContext.Request.Headers["X-Tenant-Id"];
            var claimUserId = context.HttpContext.User?.Claims.FirstOrDefault(c => c.Type == "userId");
            if (claimUserId is null)
            {
                context.Result = new UnauthorizedObjectResult("");
                return;
            }

            if (string.IsNullOrWhiteSpace(stringTenantId))
            {
                context.Result = new BadRequestObjectResult("Required header \"TenantId\" cannot be empty.");
                return;
            }

            var tenantId = Convert.ToInt64(stringTenantId);
            var userId = Convert.ToInt64(claimUserId.Value);

            var tenantUser = _databaseContext
                .Set<TenantUser>()
                .Where(e => e.UserId == userId && e.TenantId == tenantId)
                .FirstOrDefault();

            if (tenantUser is null || tenantUser.Role != "user")
            {
                context.Result = new StatusCodeResult(403);
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // var stringTenantId = context.HttpContext.Response.Headers["TenantId"];
            // var claimUserId = context.HttpContext.User?.Claims.FirstOrDefault(c => c.Type == "userId");
            // if (claimUserId is null)
            // {
            //     context.Result = new UnauthorizedObjectResult("");
            //     return;
            // }

            // if (string.IsNullOrWhiteSpace(stringTenantId))
            // {
            //     context.Result = new BadRequestObjectResult("Required header \"TenantId\" cannot be empty.");
            //     return;
            // }

            // var tenantId = Convert.ToInt64(stringTenantId);
            // var userId = Convert.ToInt64(claimUserId.Value);

            // var tenantUser = _databaseContext
            //     .Set<TenantUser>()
            //     .Where(e => e.UserId == userId && e.TenantId == tenantId)
            //     .FirstOrDefault();

            // if (tenantUser is null || tenantUser.Role != "user")
            // {
            //     context.Result = new ForbidResult("You don't have access to this resource.");
            // }
        }
    }
}