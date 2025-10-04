using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using ReactWithASP.Server.Models.DTOs;
using ReactWithASP.Server.Data.Consts;
using System.Data;
using System.Security.Claims;

namespace ReactWithASP.Server.Services;

public class AuthService(
    UserManager<IdentityUser> userManager,
    RoleManager<IdentityRole> roleManager)
    : IAuthService
{
    public async Task<(int, string)> Registration(RegistrationDto model)
    {
        var userExists = await userManager.FindByEmailAsync(model.Email ?? string.Empty);
        if (userExists != null)
        {
            return (0, "Email already exists");
        }

        var user = new IdentityUser
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Username
        };

        var createUserResult = await userManager.CreateAsync(user, model?.Password ?? string.Empty);
        if (!createUserResult.Succeeded)
        { 
         var errors = string.Join(", ", createUserResult.Errors.Select(e => e.Description));
            return (0, $"User creation failed! {errors}");
        }

            var role = UserRoles.User;

            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new IdentityRole(role));

            await userManager.AddToRoleAsync(user, role);

            return (1, "User created successfully!");
        }

public async Task<(int, AuthDto)> Login(LoginDto model, HttpContext httpContext)
    {
        var user = await userManager.FindByEmailAsync(model?.Email ?? string.Empty);
        if (user == null)
        {
            return (0, new AuthDto ( IsAuthenticated: false, Message: "Invalid email", UserName: null, Email: null));
        }

        if (!await userManager.CheckPasswordAsync(user, model?.Password ?? string.Empty))
            return (0, new AuthDto ( IsAuthenticated: false, Message: "Invalid password", UserName: null, Email: null));

        var claims = new List<Claim>
    {
        new(type: ClaimTypes.Name, user.UserName ?? string.Empty),
        new(type: ClaimTypes.Email, model?.Email ?? string.Empty)
    };
        var roles = await userManager.GetRolesAsync(user);

        foreach (var role in roles) claims.Add(new Claim(type: ClaimTypes.Role, role));

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true
        };

        await httpContext.SignInAsync(IdentityConstants.ApplicationScheme,
            new ClaimsPrincipal(claimsIdentity), authProperties);

        return (1, new AuthDto(IsAuthenticated: true,
            Message: "Login successful", user.UserName, user.Email, Role: roles[0]));
    }

    public AuthDto CheckSession(HttpContext httpContext)
    {
        var user = httpContext.User;
        var roles = user.Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value)
            .ToList();

        if (user.Identity is not { IsAuthenticated: true })
            return new AuthDto(IsAuthenticated: false,
                Message: "User is not authenticated");

        var userName = user.Identity.Name;
        var userEmail = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

        return new AuthDto(IsAuthenticated: true,
            Message: "User is authenticated",
            UserName: userName,
            Email: userEmail,
            Role: roles[0] );
    }
    public async Task Logout(HttpContext httpContext)
    {
        await httpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
    }
}