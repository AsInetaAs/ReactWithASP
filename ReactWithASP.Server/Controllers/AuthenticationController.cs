using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using ReactWithASP.Server.Controllers;
using ReactWithASP.Server.Models.DTOs;
using ReactWithASP.Server.Services;

[Route(template: "api/[controller]")]
[ApiController]
public class AuthenticationController(IAuthService authService, ILogger<AuthenticationController> logger) : ControllerBase
{
    [HttpPost(template: "signin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginDto model)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(error: "Invalid payload");
            var (status, authDto) = await authService.Login(model, HttpContext);
            if (status == 0)
                return BadRequest(authDto.Message);
            return Ok(authDto);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    [HttpPost(template: "signup")]
    public async Task<IActionResult> Register(RegistrationDto model)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(error: "Invalid payload");
            var (status, message) = await authService.Registration(model);
            if (status == 0) return BadRequest(message);
            return CreatedAtAction(nameof(Register), model);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet(template: "check-session")]
    public IActionResult CheckSession()
        => Ok(authService.CheckSession(HttpContext));

    [HttpPost(template: "logout")]
    public async Task<IActionResult> Logout()
    {
        await authService.Logout(HttpContext);
        return Ok(new { message = "Logout successful" });
    }

    [HttpGet(template: "csrf-token")]
    public IActionResult GetCsrfToken()
    {
        var antiforgery = HttpContext.RequestServices.GetRequiredService<IAntiforgery>();
        var tokens = antiforgery.GetAndStoreTokens(HttpContext);
        return Ok(new { token = tokens.RequestToken });
    }
}
