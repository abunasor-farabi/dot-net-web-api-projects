using ClothsApi.Models;
using ClothsApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClothsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClothController : ControllerBase
    {
        // =============================================================
        // CATEGORY 1: SPECIFIC TYPES (Primitive and Complex)
        // ASYNC: Task<int>, Task<Cloth?>, Task<List<Cloth>>, Task<List<string>>
        // LIMITATION: Always returns 200 OK - No control over status codes
        // =============================================================

        // GET /api/cloth/GetClothCount
        // Returns: Task<int> - 200 OK with integer value (number of cloths)
        [HttpGet("GetClothCount")]
        public async Task<int> GetClothCount()
        {
            return await ClothService.GetClothCountAsync();
        }

        // GET /api/cloth/GetClothById/3
        // Returns: Task<Cloth?> - 200 OK with Cloth object OR 204 No Content (if null)
        // ISSUE: Cannot return 404 Not Found for missing items
        [HttpGet("GetClothById/{id}")]
        public async Task<Cloth?> GetClothById(int id)
        {
            return await ClothService.GetClothByIdAsync(id);
        }

        // GET /api/cloth/GetAllCloths
        // Returns: Task<List<Cloth>> - 200 OK with list of cloths
        [HttpGet("GetAllCloths")]
        public async Task<List<Cloth>> GetAllCloths()
        {
            return await ClothService.GetAllClothsAsync();
        }

        // GET /api/cloth/GetAllClothNames
        // Returns: Task<List<string>> - 200 OK with list of names only
        [HttpGet("GetAllClothNames")]
        public async Task<List<string>> GetAllClothNames()
        {
            return await ClothService.GetAllClothNamesAsync();
        }

        // ==============================================================
        // CATEGORY 2: IActionResult - FULL HTTP CONTROL
        // ASYNC: Task<IActionResult>
        // BEST FOR: Real-world APIs with proper error handling
        // ===============================================================

        // GET /api/cloth/GetClothDetails/5
        // Returns: Task<IActionResult> - 200 OK, OR 400 Bad Request, OR
        // 404 NOT Found
        [HttpGet("GetClothDetails/{id}")]
        public async Task<IActionResult> GetClothDetails(int id)
        {
            // Bad Request - Invalid input
            if (id <= 0)
                return BadRequest("Invalid cloth ID. Must be greater than zero.");
            
            var cloth = await ClothService.GetClothByIdAsync(id);

            // Not Found - Resource doesn't exist
            if (cloth == null)
                return NotFound($"Cloth with ID = {id} was not found.");
            
            // Success - Return data with 200 OK
            return Ok(cloth);
        }

        // GET /api/cloth/GetClothsByCategory/men
        // Returns: Task<IActionResult> - 200 OK with filtered list, OR 404 if
        // no items found
        [HttpGet("GetClothsByCategory/{category}")]
        public async Task<IActionResult> GetClothsByCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
                return BadRequest("Category cannot be empty.");

            
            var cloths = await ClothService.GetClothsByCategoryAsync(category);

            if (cloths == null || cloths.Count == 0)
                return NotFound($"No cloths found in category: {category}");
            
            return Ok(cloths);
        }

        // GET /api/cloth/GetAllClothsWithStatus
        // Demonstrates: NoContent (204) when list is empty
        [HttpGet("GetAllClothsWithStatus")]
        public async Task<IActionResult> GetAllClothsWithStatus()
        {
            var cloths = await ClothService.GetAllClothsAsync();

            if (cloths == null || cloths.Count == 0)
                return NoContent();     // 204 No Content
            
            return Ok(cloths);
        }

        // ================================================================
        // CATEGORY 3: ActionResult<T> - BEST PRACTICE
        // ASYNC: Task<ActionResult<T>>
        // Combines Strong Typing + Full HTTP Control
        // Swagger shows exact response schema
        // =================================================================

        // GET /api/cloth/GetClothCountTyped
        // Returns: Task<ActionResult<int>> - Strongly typed integer response
        [HttpGet("GetClothCountTyped")]
        public async Task<ActionResult<int>> GetClothCountTyped()
        {
            int count = await ClothService.GetClothCountAsync();
            return Ok(count);       // 200 OK with integer
        }

        // GET /api/cloth/GetClothByIdTyped/3
        // Returns: Task<ActionResult<Cloth>> - Strongly typed Cloth object
        [HttpGet("GetClothByIdTyped/{id}")]
        public async Task<ActionResult<Cloth>> GetClothByIdTyped(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid cloth ID. Must be greater than zero.");

            var cloth = await ClothService.GetClothByIdAsync(id);

            if (cloth == null)
                return NotFound($"Cloth with ID = {id} was not found.");
            
            return Ok(cloth);
        }

        // GET /api/cloth/GetAllClothsTyped
        // Returns: Task<ActionResult<List<Cloth>>> - Strongly typed list
        [HttpGet("GetAllClothsTyped")]
        public async Task<ActionResult<List<Cloth>>> GetAllClothsTyped()
        {
            var cloths = await ClothService.GetAllClothsAsync();

            if (cloths == null || cloths.Count == 0)
                return NoContent();
            
            return Ok(cloths);
        }

        // GET /api/cloth/GetClothNamesTyped
        // Returns: Task<ActionResult<List<string>>> - Strongly typed string list
        [HttpGet("GetClothNamesTyped")]
        public async Task<ActionResult<List<string>>> GetClothNamesTyped()
        {
            var names = await ClothService.GetAllClothNamesAsync();

            if (names == null || names.Count == 0)
                return NotFound("No cloth names found.");
            
            return Ok(names);
        }

        // GET /api/cloth/GetByCategoryTyped/men
        // Returns: Task<ActionResult<list<Cloth>>> - Filtered by category
        [HttpGet("GetByCategoryTyped/{category}")]
        public async Task<ActionResult<List<Cloth>>> GetByCategoryTyped(string category)
        {
            if(string.IsNullOrWhiteSpace(category))
                return BadRequest("Category cannot be empty.");
             
            var cloths = await ClothService.GetClothsByCategoryAsync(category);

            if (cloths == null || cloths.Count == 0)
                return NotFound($"No cloths found in category: {category}");

            return Ok(cloths);
        }
    }
}

