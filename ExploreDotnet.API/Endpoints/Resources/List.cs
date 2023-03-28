using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExploreDotnet.API.Endpoints.Resources
{
    public class List : EndpointBaseAsync.WithoutRequest.WithActionResult
    {
        [HttpGet("api/resources")]
        [Authorize(Policy = "AllowRoleUser")]
        public override async Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default)
        {
            return Ok("Success");
        }
    }
}