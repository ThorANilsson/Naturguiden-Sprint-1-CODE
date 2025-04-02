using NaturguidenServerPrototype.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace NaturguidenServerPrototype.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthenticationController : ControllerBase {
    private readonly CustomAuthenticationService _authenticationService;

    public AuthenticationController(CustomAuthenticationService authenticationService) {
        _authenticationService = authenticationService;
    }

    [Authorize]
    [HttpGet("check-auth")]
    public IActionResult CheckAuth() {
        // The user must be authenticated to reach this point
        return Ok(new { Message = "User is authenticated and authorized" });
    }

    [HttpPost("log-in")]
    public async Task<IActionResult> Login([FromBody] LoginRequest login) {
        if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Password)) {
            return BadRequest("Email and password are required.");
        }

        var user = await _authenticationService.AuthenticateAsync(login.Email, login.Password);

        if (user == null) {
            return Unauthorized(new { Message = "Invalid email or password" });
        }

        // handle successful login
        var claims = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) // asign the user's ID to the session
        };
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal); // creates session cookie
        return Ok(new { Message = "Login successful" });
    }

    [HttpPost("log-out")]
    public async Task<IActionResult> Logout() {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return Ok(new { Message = "Logout successful" });
    }

}