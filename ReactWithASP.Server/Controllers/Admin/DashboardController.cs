using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReactWithASP.Server.Data.Consts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace ReactWithASP.Server.Controllers.Admin;

[ApiController]
[Route(template: "api/admin/[controller]")]
[Authorize(Roles = UserRoles.Admin)]
public class DashboardController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;

    public DashboardController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }
    public IActionResult Show()
        => Ok(new { text = "You logged to dashboard!" });

    [HttpGet("users")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userManager.Users.ToListAsync();
        var list = new List<object>(users.Count);
        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            list.Add(new
            {
                user.Id,
                user.UserName,
                user.Email,
                Role = roles.FirstOrDefault() ?? string.Empty
            });
        }

        return Ok(list);
    }

}
