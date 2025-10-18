using System.Text.Json.Serialization;
using Data.Contracts;
using Environment;
using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Identity;

namespace AutoBuy;

public record LoginUserRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public record LoginUserResponse
{
    [JsonPropertyName("token")]
    public required string Token { get; set; }
}

public class LoginUser : Endpoint<LoginUserRequest, LoginUserResponse>
{
    private readonly SignInManager<AutoBuyIdentityUser> _signInManager;

    public LoginUser(SignInManager<AutoBuyIdentityUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public override void Configure()
    {
        Post("/users/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginUserRequest req, CancellationToken ct)
    {
        var user = await _signInManager.UserManager.FindByEmailAsync(req.Email.Trim());
        
        if (user is null)
        {
            AddError("Email", "User not found");
            await Send.ErrorsAsync(cancellation: ct);
            return;
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, req.Password, lockoutOnFailure: false);
        if (!result.Succeeded)
        {
            AddError("Password", "Invalid password");
            await Send.ErrorsAsync(cancellation: ct);
            return;
        }

        var jwtToken = JwtBearer.CreateToken(o =>
        {
            o.SigningKey = EnvironmentExtensions.GetJwtSigningKey();
            o.ExpireAt = DateTime.UtcNow.AddDays(1);
            o.User.Claims.Add(("Email", req.Email));
        });
        await Send.OkAsync(new LoginUserResponse { Token = jwtToken }, ct);
    }
}