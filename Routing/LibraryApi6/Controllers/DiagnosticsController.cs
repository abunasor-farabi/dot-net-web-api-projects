using Microsoft.AspNetCore.Mvc;

namespace LibraryApi6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiagnosticsController : ControllerBase
    {
        // ASP.NET Core injects all registered
        // EndpointDataSource instances
        private readonly IEnumerable<EndpointDataSource> 
            _endpointDataSources;
        public DiagnosticsController
        (IEnumerable<EndpointDataSource> endpointDataSources)
        {
            _endpointDataSources = endpointDataSources;
        }

        // GET: /api/diagnostics/endpoints
        // Returns all RouteEndpoint objects from 
        // CompositeEndpointDataSurce
        [HttpGet("endpoints")]
        public IActionResult GetAllEndpoints()
        {
            var results = new List<object>();

            foreach (var dataSource in _endpointDataSources)
            {
                foreach (var endpoint in dataSource.Endpoints)
                {
                    results.Add(new
                    {
                        DisplyName = endpoint.DisplayName,
                        IsRouteEndpoint = endpoint is RouteEndpoint,
                        RoutePattern = (endpoint as RouteEndpoint)?
                            .RoutePattern?.RawText,
                        HttpMethods = endpoint.Metadata
                            .GetMetadata<HttpMethodMetadata>()?
                            .HttpMethods
                    });
                }
            }

            return Ok(results);
        }
    }
}

