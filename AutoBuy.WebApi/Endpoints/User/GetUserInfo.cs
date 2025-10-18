using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json.Serialization;
using Data.Contracts;
using Environment;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace AutoBuy;

public record GetUserInfoRequest
{
    [FromHeader("Authorization")] public required string JwtToken { get; set; }
}

public record GetUserInfoResponse
{
    [JsonPropertyName("first_name")] public required string FirstName { get; set; }

    [JsonPropertyName("last_name")] public required string LastName { get; set; }

    [JsonPropertyName("email")] public required string Email { get; set; }

    [JsonPropertyName("phone_number")] public required string PhoneNumber { get; set; }

    [JsonPropertyName("address")] public required string Address { get; set; }

    [JsonPropertyName("city")] public required string City { get; set; }

    [JsonPropertyName("postal_code")] public required string PostalCode { get; set; }

    [JsonPropertyName("country")] public required string Country { get; set; }
}

public class GetUserInfo : Endpoint<GetUserInfoRequest, GetUserInfoResponse>
{
    private readonly UserManager<AutoBuyIdentityUser> _userManager;

    public override void Configure()
    {
        Get("/users/");
        AuthSchemes(JwtBearerDefaults.AuthenticationScheme);
        AllowAnonymous();
    }


    public GetUserInfo(UserManager<AutoBuyIdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public override async Task HandleAsync(GetUserInfoRequest req, CancellationToken ct)
    {
        var hasEmail = User.Claims.Any(x=> x.Type.ToLowerInvariant().Equals("email"));
        if (!hasEmail)
        {
            await Send.ErrorsAsync(401, ct);
            return;
        }

        var email =  User.Claims.Single(x=> x.Type.ToLowerInvariant().Equals("email")).Value;
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            await Send.ErrorsAsync(401, ct);
            return;
        }

        await Send.OkAsync(new GetUserInfoResponse
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Address = user.Address,
            City = user.City,
            PostalCode = user.PostalCode,
            Country = user.Country
        },ct);
    }
}