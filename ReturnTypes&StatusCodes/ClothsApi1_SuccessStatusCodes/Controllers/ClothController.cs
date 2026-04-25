using ClothsApi1_SuccessStatusCodes.Models;
using ClothsApi1_SuccessStatusCodes.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClothsApi1_SuccessStatusCodes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClothController : ControllerBase
    {
        // =============================================================
        // 200 OK - GET /api/cloth/5
        // Returns cloth data with 200 OK status
        // Use Ok() helper method
        // =============================================================
        [HttpGet("{id}")]
        public async Task<ActionResult<Cloth>> GetClothById(int id)
        {
            var cloth = await ClothService.GetClothByIdAsync(id);

            if(cloth == null)
                return NotFound($"Cloth with {id} not found");
            
            
            // Returns 200 OK with cloth data in response body
            return Ok(cloth);
        }

        // =============================================================
        // 200 OK - GET /api/cloth
        // Returns all cloths with 200 status
        // =============================================================
        [HttpGet]
        public async Task<ActionResult<List<Cloth>>> GetAllCloths()
        {
            var cloths = await ClothService.GetAllClothsAsync();

            // Returns 200 OK with list of cloths
            return Ok(cloths);
        }

        // =============================================================
        // 201 Created - POST /api/cloth
        // Creates new cloth, returns 201 with Location header
        // Use CreatedAtAction() helper method
        // =============================================================
        [HttpPost]
        public async Task<ActionResult<Cloth>> CreateCloth
        ([FromBody] Cloth newCloth)
        {
            if (string.IsNullOrEmpty(newCloth.Name))
                return BadRequest("Cloth name is required");
            
            var createdCloth = await ClothService.AddClothAsync(newCloth);

            // Returns 201 Created with:
            // - Location header pointing to GetClothById endpoint
            // - Created cloth data in response body
            return CreatedAtAction(
                actionName: nameof(GetClothById),
                routeValues: new { id = createdCloth.Id},
                value: createdCloth
            );
        }

        // =============================================================
        // 202 Accepted - POST /api/cloth/bulk-order
        // Accepts async operation, returns 202 with status message
        // Use Accepted() helper method
        // =============================================================
        [HttpPost("bulk-order")]
        public async Task<ActionResult> ProcessBulkOrder()
        {
            // Simulate async background processing
            await ClothService.ProcessBulkOrderAsync();

            // Returns 202 Accepted with status message
            return Accepted(new
            {
                Message = "Bulk order accepted. Processing in background.",
                EstimatedCompletion = "5 minutes",
                TrackingId = Guid.NewGuid().ToString()
            });
        }

        // =============================================================
        // 204 No Content - DELETE /api/cloth/5
        // Delets cloth, returns empty body with 204
        // Use NoContent() helper method
        // =============================================================
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCloth(int id)
        {
            var deleted = await ClothService.DeleteClothAsync(id);

            if (!deleted)
                return NotFound($"Cloth with ID {id} not found");
            
            // Returns 204 No Content (empty response body)
            return NoContent();
        }
    }
}

