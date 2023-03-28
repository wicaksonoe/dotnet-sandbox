using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace ExploreDotnet.API.Endpoints.Resources
{
    public class List : EndpointBaseAsync.WithoutRequest.WithActionResult
    {
        [HttpGet("api/resources")]
        public override async Task<ActionResult> HandleAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}