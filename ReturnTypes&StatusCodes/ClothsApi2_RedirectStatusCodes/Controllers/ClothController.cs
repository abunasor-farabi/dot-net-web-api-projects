using ClothsApi2_RedirectStatusCodes.Models;
using ClothsApi2_RedirectStatusCodes.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClothsApi2_RedirectStatusCodes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClothController : ControllerBase
    {
        // ===============================================================
        // 301 Moved Permanently - GET /api/Cloth/redirect-permanent/{oldId}
        // Old product URL permanently moved to new URL
        // Use RedirectPermanent() helper method
        // ===============================================================
        [HttpGet("redirect-permanent/{oldId}")]
        public async Task<IActionResult> GetClothPermanentRedirect(int oldId)
        {
            var cloth = await ClothService.GetClothByOldIdAsync(oldId);

            if(cloth == null)
                return NotFound($"Cloth with ID {oldId} not found");
            
            // Returns 301 Moved Permanently with Location header
            // Search engines will transfer SEO ranking to new URL
            return RedirectPermanent($"/api/Cloth/v2/{cloth.Id}");
        }

        // ===============================================================
        // 301 Moved Permanently - GET /api/cloth/v1/{id}
        // API version changed from v1 to v2 (demonstration)
        // ===============================================================
        [HttpGet("v1/{id}")]
        public async Task<IActionResult> GetClothV1(int id)
        {
            var cloth = await ClothService.GetClothByIdAsync(id);

            if (cloth == null)
                return NotFound($"Cloth with ID {id} not found");
            
            // API version permanently changed to v2
            return RedirectPermanent($"/api/Cloth/v2/{id}");
        }

        // ==============================================================
        // New API endpoint (v2) - Actual resource location
        // ==============================================================
        [HttpGet("v2/{id}")]
        public async Task<ActionResult<Cloth>> GetClothV2(int id)
        {
            var cloth = await ClothService.GetClothByIdAsync(id);
            
            if (cloth == null)
                return NotFound($"Cloth with ID {id} not found");
            
            return Ok(cloth);
        }

        // ===============================================================
        // 302 FOund (Temporary Redirect) - GET /api/Cloth/offer/summer
        // Seasonal offers redirect to temporary URL
        // Use Redirect() helper method
        // ===============================================================
        [HttpGet("offer/summer")]
        public IActionResult GetSummerOffer()
        {
            // Temporary redirect for seasonal campaign
            // Customers should still use original URL next season
            return Redirect("/api/Cloth/offer/summer-2026");
        }

        // ===============================================================
        // Temporary offer page (actual content for summer 2026)
        // ===============================================================
        [HttpGet("offer/summer-2026")]
        public IActionResult GetSummerOffer2026()
        {
            return Ok(new
            {
                Offer = "Summer Sale 2026",
                Discount = "30% off on all cotton shirts",
                ValidUntill = "2026-18-31"
            });
        }

        // ===============================================================
        // 302 Found - GET /api/Cloth/category/men
        // Redirect no filtered view (temporary URL structure)
        // ===============================================================
        [HttpGet("category/men")]
        public IActionResult GetMenCategoryRedirect()
        {
            // Temporary redirect while restructuring categories
            return Redirect("/api/Cloth/filter?category=Men");
        }

        // ===============================================================
        // Filter endpoint (actual resource location)
        // ===============================================================
        [HttpGet("filter")]
        public async Task<ActionResult<List<Cloth>>> GetClothsByFilter
        ([FromQuery] string category)
        {
            var cloths = await ClothService.GetAllClothsAsync();
            var filtered = cloths.Where(c => c.Category == category).ToList();

            if (!filtered.Any())
            {
                return NotFound($"No cloths found in category: {category}");
            }
            return Ok(filtered);
        }

        // ================================================================
        // 304 Not Modified - GET /api/Cloth/{id}/cache
        // Returns 304 if ETag matches (resource not changed)
        // Client can use cached version
        // ================================================================
        [HttpGet("{id}/cache")]
        public async Task<ActionResult<Cloth>> GetClothWithCache(int id)
        {
            var cloth = await ClothService.GetClothByIdAsync(id);

            if (cloth == null)
                return NotFound($"Cloth with ID {id} not found");
            
            // Get Etag from service (simulates database version)
            var currentETag = await ClothService.GetClothETagAsync(id);

            // Check If-None-Match header from client
            var clientETag = Request.Headers["If-None-Match"].ToString();

            if (!string.IsNullOrEmpty(clientETag) && clientETag == currentETag)
            {
                // Resource not changed - return 304 Not Modified
                // No response body, client uses cached version
                return StatusCode(304);
            }

            // Set: ETag in response header for future requsts
            Response.Headers.Append("ETag", currentETag);

            return Ok(cloth);
        }

        // ===============================================================
        // LocalRedirect - GET /api/cloth/local-redirect
        // Safer redirect (prevents open redirect attacks)
        // ===============================================================
        [HttpGet("local-redirect")]
        public IActionResult LocalRedirectExample()
        {
            // Only allows local URLs (prevents redirect to malicious external sites)
            return LocalRedirect("/api/Cloth");
        }

        // ================================================================
        // RedirectToAction - GET /api/Cloth/redirect-to-action
        // Redirect to another action within same controller
        // ================================================================
        [HttpGet("redirect-to-action")]
        public IActionResult RedirectToGetAllCloths()
        {
            // Redirects to GetAllCloths action (GET /api/cloth)
            return RedirectToAction(nameof(GetAllCloths));
        }

        // ================================================================
        // Helper action for RedirectToAction demonstration
        // ================================================================
        [HttpGet]
        public async Task<ActionResult<List<Cloth>>> GetAllCloths()
        {
            var cloths = await ClothService.GetAllClothsAsync();
            return Ok(cloths);
        }
    }
}

