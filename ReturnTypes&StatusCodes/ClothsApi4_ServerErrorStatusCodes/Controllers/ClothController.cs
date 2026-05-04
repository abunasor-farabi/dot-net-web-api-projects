using ClothsApi4_ServerErrorStatusCodes.Models;
using ClothsApi4_ServerErrorStatusCodes.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClothsApi4_ServerErrorStatusCodes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClothController : ControllerBase
    {
        // ==============================================
        // 500 INTERNAL SERVER ERROR
        // Generic "catch-all" for unhandled exceptions,
        // database connection failures,
        // or unexpected server conditions.
        // Returned automatically by framework for unhandled exceptions,
        // Can also be manual: StatusCode(500)
        // ==============================================

        // GET /api/Cloth?simulateError=true
        // Returns 500 when database simulation is enabled
        [HttpGet]
        public async Task<ActionResult<List<Cloth>>> GetAllCloths(
            [FromQuery] bool simulateError = false
        )
        {
            if(simulateError)
                ClothService.SimulateDatabaseFailure = true;

            try
            {
                var cloths = await ClothService.GetAllClothsAsync();
                ClothService.SimulateDatabaseFailure = false;
                return Ok(cloths);
            } 
            catch (Exception ex)
            {
                ClothService.SimulateDatabaseFailure = false;
                // 500 Internal Server - unhandled exception
                return StatusCode(500, new { Error = "Internal Server Error", Message = ex.Message });
            }
        }

        // GET /api/Cloth/500-manual/0
        // Manual 500 response (without exception)
        [HttpGet("500-manual/{id}")]
        public async Task<ActionResult<Cloth>> GetClothManual500(int id)
        {
            if (id <= 0)
            {
                return StatusCode(500, new {
                    Error = "Internal Server Error", 
                    Message = "Invalid ID caused server processing error."
                });
            }

            var cloth = await ClothService.GetClothByIdAsync(id);

            if (cloth == null)
            {
                return NotFound($"Cloth with ID {id} not found");
            }

            return Ok(cloth);
        }

        // ===================================================
        // 501 NOT IMPLEMENTED
        // Server does not support the functionality required to fulfill the request,
        // often because the HTTP method is not recognized or feature not implemented.
        // Manual: StatusCode(501)
        // ===================================================

        // POST /api/Cloth/bulk
        // Returns 501 because bulk update is not yet implemented
        [HttpPost("bulk")]
        public IActionResult BulkCreateCloths()
        {
            return StatusCode(501, new
            {
                Error = "Not Implemented",
        Message = "Bulk create operation is not yet implemented. Please use individual POST requests.",
                PlannedRelease = "Q3 2026"
            });
        }

        // PUT /api/Cloth/bulk-update
        // Returns 501 because bulk update is not yet implemented
        [HttpPut("bulk-update")]
        public IActionResult BulkUpdateCloths()
        {
            return StatusCode(501, new
            {
                Error = "Not Implemented",
                Message = "Bulk create operation is currently under development.",
                PlannedRelease = "Q4 2026"
            });
        }

        // ===============================================
        // 502 BAD GATEWAY
        // Returned when a server acting as a gateway or proxy receives
        // as invalid response from an upstream server.
        // Manual: StatusCode(502)
        // ===============================================

        // GET /api/Cloth/upstream?simulateError=true
        // Returns 502 when upstream service returns invalid response
        [HttpGet("upstream")]
        public async Task<IActionResult> GetFromUpstream(
            [FromQuery] bool simulateError = false
        )
        {
            if (simulateError)
                ClothService.SimulateUpstreamFailure = true;
            
            var response = await ClothService.CallUpstreamServiceAsync();
            ClothService.SimulateUpstreamFailure = false;

            if (response != "valid_response")
            {
                // 502 Bad Gateway - Invalid response from upstream
                return StatusCode(502, new
                {
                    Error = "Bad Gateway",
                    Message = "The upstream server returned an invalid response.",
                    UpStreamResponse = response
                });
            }

            return Ok(new
            {
                Message = "Upstream service respond successfully.",
                Data = await ClothService.GetAllClothsAsync()
            });
        }

        // ===============================================
        // 503 SERVICE UNAVAILABLE
        // Server temporarily unable to handle requests,
        // typically due to maintenance or being overloaded.
        // Manual: StatusCode(503)
        // ===============================================

        // GET /api/Cloth/maintenance
        // Check maintenance status
        [HttpGet("maintenance")]
        public IActionResult CheckMaintenanceStatus()
        {
            if (ClothService.IsUnderMaintenance)
            {
                return StatusCode(503, new
                {
                    Error = "Server Unavailable",
                    Message = "The API is currently under maintenance. Please try again later.",
                    EstimatedResumeTime = "2026-04-30T14:00:00Z"
                });
            }

            return Ok(new
            {
                Status = "Operational",
                Message = "API is running normally."
            });
        }
        
        // POST /api/Cloth/maintenance/toggle?enable=true
        // Toggle maintenance mode (for demo purposes)
        [HttpPost("maintenance/toggle")]
        public IActionResult ToggleMaintenanceMode(
            [FromQuery] bool enable
        )
        {
            ClothService.IsUnderMaintenance = enable;
            string status = enable ? "enabled" : "disabled";
            return Ok(new
            {
                Message = $"Maintenance mode {status}",
                MaintenanceMode = ClothService.IsUnderMaintenance
            });
        }

        // GET /api/Cloth/all
        // This endpoint checks maintenance flag before processing
        [HttpGet("all")]
        public async Task<ActionResult<List<Cloth>>> GetAllClothsWithMaintenanceCheck()
        {
            if (ClothService.IsUnderMaintenance)
            {
                return StatusCode(503, new
                {
                    Error = "Service Unavailable",
                    Message = "The API is currently under maintenance. Please try again later."
                });
            }

            var cloths = await ClothService.GetAllClothsAsync();
            return Ok(cloths);
        }

        // ===============================================
        // 504 GATEWAY TIMEOUT
        // A gateway or proxy server did not receive a timely response
        // from an upstream server
        // Manual: StatusCode(504)
        // ===============================================

        // GET /api/Cloth/timeout?simulateTimeout=true
        // Returns 504 when upstream service times out
        [HttpGet("timeout")]
        public async Task<IActionResult> GetWithTimeout(
            [FromQuery] bool simulateTimeout = false
        )
        {
            if (simulateTimeout)
            {
                ClothService.SimulateSlowResponse = true;
            }

            try
            {
                // Set a timeout of 5 seconds for this operation
                using var cts = new 
                    CancellationTokenSource(TimeSpan.FromSeconds(5));
                
                var response = await ClothService.CallSlowUpstreamServiceAsync();
                ClothService.SimulateSlowResponse = false;

                return Ok(new
                {
                    Message = "Upstream service responded in time.",
                    Data = response
                });
            }
            catch (TaskCanceledException)
            {
                ClothService.SimulateSlowResponse = false;
                // 504 Gateway Timeout - Upstream server did not respond in time
                return StatusCode(504, new 
                { 
                    Error = "Gateway Timeout", 
                    Message = "The upstream server did not respond in time. Please try again later.",
                    TimeoutSeconds = 5
                });
            }
        }

        // ============================================================
        // Normal working endpoints (for comparison)
        // ============================================================

        // POST /api/cloth - Create new cloth
        [HttpPost]
        public async Task<ActionResult<Cloth>> CreateCloth([FromBody] Cloth newCloth)
        {
            if (string.IsNullOrWhiteSpace(newCloth.Name))
                return BadRequest("Cloth name is required.");
            
            if (ClothService.IsUnderMaintenance)
                return StatusCode(503, "Service under maintenance. Please try again later.");
            
            var createdCloth = await ClothService.AddClothAsync(newCloth);
            return CreatedAtAction(nameof(GetClothById), new { id = createdCloth.Id }, createdCloth);
        }

        // GET /api/cloth/{id} - Get cloth by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Cloth>> GetClothById(int id)
        {
            if (ClothService.IsUnderMaintenance)
                return StatusCode(503, "Service under maintenance. Please try again later.");
            
            var cloth = await ClothService.GetClothByIdAsync(id);
            if (cloth == null)
                return NotFound($"Cloth with ID {id} not found");
            
            return Ok(cloth);
        }
    }
}



