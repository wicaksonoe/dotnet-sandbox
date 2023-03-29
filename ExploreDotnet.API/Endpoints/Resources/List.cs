using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using ExploreDotnet.API.Endpoints.ActionFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExploreDotnet.API.Endpoints.Resources
{
    public class List : EndpointBaseAsync.WithoutRequest.WithActionResult
    {
        [HttpGet("api/resources")]
        [Authorize]
        [ServiceFilter(typeof(AllowRoleUserAttribute))]
        public override async Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default)
        {
            return Ok("Success");
        }
    }
}