using Data.Contracts;
using Environment;
using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Identity;

namespace AutoBuy;

public record RegisterUserRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public record RegisterUserResponse
{
    public required string Email { get; set; }
    public required string Token { get; set; }
}

public class RegisterUser : Endpoint<RegisterUserRequest, RegisterUserResponse>
{
    private readonly UserManager<IdentityUser> _userManager;

    public RegisterUser(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public override void Configure()
    {
        Post("/users/register");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterUserRequest req, CancellationToken ct)
    {
        var user = new IdentityUser { UserName = req.Email, Email = req.Email };
        var result = await _userManager.CreateAsync(user, req.Password);
        if (!result.Succeeded)
        {
            AddError(result.Errors.First().Code, result.Errors.First().Description);
            await Send.ErrorsAsync(cancellation: ct);
            return;
        }

        var jwtToken = JwtBearer.CreateToken(o =>
        {
            o.SigningKey = EnvironmentExtensions.GetJwtSigningKey();
            o.ExpireAt = DateTime.UtcNow.AddDays(1);
            o.User.Claims.Add(("Email", req.Email));
        });

        await Send.OkAsync(new RegisterUserResponse
        {
            Email = req.Email,
            Token = jwtToken
        }, ct);
    }
}