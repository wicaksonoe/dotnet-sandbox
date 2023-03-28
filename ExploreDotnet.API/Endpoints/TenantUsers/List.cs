using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using ExploreDotnet.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExploreDotnet.API.Endpoints.TenantUsers
{
    public class List : EndpointBaseAsync.WithoutRequest.WithActionResult
    {
        private readonly DatabaseContext _context;

        public List(DatabaseContext context)
        {
            _context = context;
        }
        
        [HttpGet("api/tenantUsers")]
        public override async Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default)
        {
            var tenantUsers = await _context.TenantUsers
                .Include(e => e.Tenant)
                .Include(e => e.User)
                .ToListAsync(cancellationToken);

            var parsed = tenantUsers.Select(e => new
            {
                UserId = e.User.Id,
                Email = e.User.Email,
                Tenant = e.Tenant.Name,
                Role = e.Role,
            }).ToList();

            return Ok(parsed);
        }
    }
}