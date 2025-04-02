using NaturguidenServerPrototype.Models;
using NaturguidenServerPrototype.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace NaturguidenServerPrototype.Controllers;

[ApiController]
[Route("api/[controller]")]

public class NatureSpotsController : ControllerBase {
    private readonly NatureSpotsService _natureSpotsService;

    public NatureSpotsController(NatureSpotsService natureSpotsService) {
        _natureSpotsService = natureSpotsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllNatureSpots() {
        var natureSpots = await _natureSpotsService.GetAllAsync();
        return Ok(natureSpots);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) {
        var natureSpot = await _natureSpotsService.GetAsync(id);
        if (natureSpot == null) {
            return NotFound();
        }

        bool userHasVisited = false;
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!string.IsNullOrEmpty(userIdString) && int.TryParse(userIdString, out int userId)) {
            userHasVisited = await _natureSpotsService.HasUserVisitedAsync(userId, id);
        }
        if (userIdString != null) {
            return Ok(new {
                NatureSpot = natureSpot,
                UserHasVisited = userHasVisited
            });
        }
        else {
            return Ok(new { NatureSpot = natureSpot });
        }
    }

    [Authorize]
    [HttpPost("mark-as-visited")]
    public async Task<IActionResult> MarkAsVisited([FromBody] MarkAsVisitedRequest markAsVisitedRequest) {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int userId = Int32.Parse(userIdString);
        if (await _natureSpotsService.HasUserVisitedAsync(userId, markAsVisitedRequest.PlaceId)) {
            return Conflict(new { Message = "User has already visited this place." });
        }
        await _natureSpotsService.AddVisitAsync(userId, markAsVisitedRequest.PlaceId);
        return Ok();
    }
}