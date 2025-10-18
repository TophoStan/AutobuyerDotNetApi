using System.Text.Json.Serialization;
using Data.Contracts;
using Environment;
using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Identity;

namespace AutoBuy;

public record RegisterUserRequest
{
    [JsonPropertyName("first_name")]
    public required string FirstName { get; set; }
    
    [JsonPropertyName("last_name")]
    public required string LastName { get; set; }
    [JsonPropertyName("email")] public required string Email { get; set; }
    [JsonPropertyName("password")] public required string Password { get; set; }
    
    [JsonPropertyName("phone_number")] public required string PhoneNumber { get; set; }

    [JsonPropertyName("address")] public required string Address { get; set; }

    [JsonPropertyName("city")] public required string City { get; set; }

    [JsonPropertyName("country")] public required string Country { get; set; }

    [JsonPropertyName("postal_code")] public required string PostalCode { get; set; }
}

public record RegisterUserResponse
{
    [JsonPropertyName("email")] public required string Email { get; set; }
    [JsonPropertyName("token")] public required string Token { get; set; }
}

public class RegisterUser : Endpoint<RegisterUserRequest, RegisterUserResponse>
{
    private readonly UserManager<AutoBuyIdentityUser> _userManager;

    public RegisterUser(UserManager<AutoBuyIdentityUser> userManager)
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
        var user = new AutoBuyIdentityUser
        {
            UserName = req.Email, Email = req.Email, Address = req.Address, City = req.City, Country = req.Country,
            PostalCode = req.PostalCode,
            FirstName = req.FirstName,
            LastName = req.LastName,
            PhoneNumber = req.PhoneNumber
        };
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
            o.User.Claims.Add(("email", req.Email));
        });

        await Send.OkAsync(new RegisterUserResponse
        {
            Email = req.Email,
            Token = jwtToken
        }, ct);
    }
}