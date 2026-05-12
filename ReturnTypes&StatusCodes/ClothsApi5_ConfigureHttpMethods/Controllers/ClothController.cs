using Microsoft.AspNetCore.Mvc;
using ClothsApi5_ConfigureHttpMethods.Models;
using ClothsApi5_ConfigureHttpMethods.Services;

namespace ClothsApi5_ConfigureHttpMethods.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClothController : ControllerBase
    {
        // GET /api/cloth - Allowed (GET)
        [HttpGet]
        public async Task<ActionResult<List<Cloth>>> GetAllCloths()
        {
            var cloths = await ClothService.GetAllClothsAsync();
            return Ok(cloths);
        }

        // GET /api/cloth/3 - Allowed (GET)
        [HttpGet("{id}")]
        public async Task<ActionResult<Cloth>> GetClothById(int id)
        {
            var cloth = await ClothService.GetClothByIdAsync(id);
            if (cloth == null)
                return NotFound($"Cloth with ID {id} not found");
            return Ok(cloth);
        }

        // POST /api/cloth - Allowed (POST)
        [HttpPost]
        public async Task<ActionResult<Cloth>> CreateCloth([FromBody] Cloth newCloth)
        {
            // This is just a demo - not full implementation
            return Ok(new { Message = "POST request received", Data = newCloth });
        }

        // PUT /api/cloth/5 - NOT Allowed (PUT not in allowed list)
        [HttpPut("{id}")]
        public async Task<ActionResult<Cloth>> UpdateCloth()
        {
            return Ok(new { 
                Message = "PUT request received (should be blocked by Middleware/ActionFilter)" 
            });
        }

        // PATCH /api/cloth/5 - NOT Allowed (PATCH not in allowed list)
        [HttpPatch("{id}")]
        public async Task<ActionResult<Cloth>> PatchCloth()
        {
            return Ok(new { 
                Message = "PATCH request received (should be blocked by Middleware/ActionFilter)" 
            });
        }

        // DELETE /api/cloth/5 - Allowed (DELETE)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCloth(int id)
        {
            return Ok(new { Message = $"DELETE request received for ID {id}" });
        }
    }
}

