using ClothsApi3_ClientErrorStatusCodes.Models;
using ClothsApi3_ClientErrorStatusCodes.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClothsApi3_ClientErrorStatusCodes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClothController : ControllerBase
    {
        // =========================================================
        // 400 Bad Request -- Invalid input from client
        // Helper: BadRequest()
        // =========================================================

        // POST /api/Cloth
        // Returns 400 if Name is missing
        [HttpPost]
        public async Task<ActionResult<Cloth>> CreateCloth(
            [FromBody] Cloth newCloth
        )
        {
            // 400: Name is required
            if (string.IsNullOrWhiteSpace(newCloth.Name))
                return BadRequest("Cloth name is required. Please Provide a valid name.");
            
            // 400: Price must be greater than 0
            if (newCloth.Price <= 0)
                return BadRequest("Price must be greater than zero.");
            
            // 400: Category is required
            if (string.IsNullOrWhiteSpace(newCloth.Category))
                return BadRequest("Category is required. (Men, Women, Kids, Accessories)");
            
            var createdCloth = await ClothService.AddClothAsync(newCloth);

            return CreatedAtAction(
                nameof(GetClothById), 
                new {id = createdCloth.Id}, 
                createdCloth
            );
        }

        // =========================================================
        // 401 Unauthorized -- Authentication failed or not provided
        // Helper: Unauthorized()
        // =========================================================

        // GET /api/Cloth/admin/all
        // Returns 401 if API key is missing or invalid
        [HttpGet("admin/all")]
        public async Task<ActionResult<List<Cloth>>> GetAdminAllCloths(
            [FromHeader(Name = "X-API-Key")] string? apiKey
        )
        {
            // 401: API key is missing
            if (string.IsNullOrEmpty(apiKey))
                return Unauthorized("API key is missing. Please provide a valid API key.");
            
            // 401: API Key is invalid
            if (!ClothService.IsValidApiKey(apiKey))
                return Unauthorized("Invalid API key. Authentication failed.");
            
            var cloths = await ClothService.GetAllClothsAsync();
            return Ok(cloths);
        }

        // =========================================================
        // 403 Forbidden -- Authenticated but not authorized
        // Helper: Forbid()
        // =========================================================

        // GET /api/Cloth/admin/delete/{id}
        // Returns 401 if no API key, 403 if API key valid but not admin role
        [HttpGet("admin/delete/{id}")]
        public async Task<IActionResult> DeleteClothAdmin(
            int id,
            [FromHeader(Name ="X-API-Key")] string? apiKey,
            [FromHeader(Name ="X-User-Role")] string? userRole
        )
        {
            // 401: API key is missing
            if (string.IsNullOrEmpty(apiKey))
                return Unauthorized("API key is missing. Please provide a valid API key.");
            
            // 401: API key is invalid
            if (!ClothService.IsValidApiKey(apiKey))
                return Unauthorized("Invalid API key. Authentication failed.");
            
            // 403: Valid API key but not admin role
            if (!ClothService.IsAdminUser(userRole))
            {
                // return Forbid("Only admin users can delete products.");
                // Forbid() dont response with 403 direclty,
                // Use can observe the response but cannot show 403 directly
                // we are going to use,
                return StatusCode(403, "You do not have admin privileges. Only admin users can delete products.");
            }
            
            var cloth = await ClothService.GetClothByIdAsync(id);
            if(cloth == null)
                return NotFound($"Cloth with ID {id} not found");
            
            return Ok($"Cloth '{cloth.Name}' deleted successfully (simulated)");
        }

        // ===========================================================
        // 404 Not Found -- Resource does not exist
        // Helper: NotFound()
        // ===========================================================

        // GET /api/Cloth/{id}
        // Returns 404 if cloth with given ID does not exist
        [HttpGet("{id}")]
        public async Task<ActionResult<Cloth>> GetClothById(int id)
        {
            var cloth = await ClothService.GetClothByIdAsync(id);

            // 404: Cloth not found
            if (cloth == null)
                return NotFound($"Cloth with ID {id} was not found.");
            
            return Ok(cloth);
        }

        // GET /api/Cloth (all cloths -- works fine)
        [HttpGet]
        public async Task<ActionResult<List<Cloth>>> GetAllCloths()
        {
            var cloths = await ClothService.GetAllClothsAsync();
            return Ok(cloths);
        }

        // ==========================================================
        // 405 Method Not Allowed -- HTTP method not supported
        // Handled automatically by framework
        // ==========================================================

        // Note: This endpoint only supports GET
        // If you send POST request to /api/Cloth/5, you will get 405
        [HttpGet("{id}/details")]
        public async Task<ActionResult<Cloth>> GetClothDetails(int id)
        {
            var cloth = await ClothService.GetClothByIdAsync(id);
            if (cloth == null)
                return NotFound($"Cloth with ID {id} not found");
            
            return Ok(cloth);
        }
    }
}

