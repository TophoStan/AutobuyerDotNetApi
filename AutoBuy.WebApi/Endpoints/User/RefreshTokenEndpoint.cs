using Environment;
using FastEndpoints;
using FastEndpoints.Security;

namespace AutoBuy;

public record RefreshTokenRequest
{
    public required string Token { get; set; }
}

public record RefreshTokenResponse
{
    public required string Token { get; set; }
}

public class RefreshTokenEndpoint : Endpoint<RefreshTokenRequest, RefreshTokenResponse>
{
    public override void Configure(){
        Post("/users/refresh-token");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RefreshTokenRequest req, CancellationToken ct)
    {
        // Verify the token was signed by this application
        if (!TokenHelper.VerifyTokenSignature(req.Token))
        {
            AddError("Token", "Invalid token signature");
            await Send.ErrorsAsync(401, ct);
            return;
        }
        
        var decodedToken = TokenHelper.DecodeToken(req.Token);
        
        // Check if token is expired (only refresh valid tokens)
        if (decodedToken.IsTokenExpired())
        {
            AddError("Token", "Token is expired");
            await Send.ErrorsAsync(401, ct);
            return;
        }

        // Create new token with same claims but extended expiry
        var newToken = JwtBearer.CreateToken(o =>
        {
            o.SigningKey = EnvironmentExtensions.GetJwtSigningKey();
            o.ExpireAt = DateTime.UtcNow.AddDays(1);
            o.User.Claims.Add(("email", decodedToken.Claims.First(c => c.Type == "email").Value));
            o.User.Claims.Add(("id", decodedToken.Claims.First(c => c.Type == "id").Value));
        });

        var response = new RefreshTokenResponse
        {
            Token = newToken
        };
        await Send.OkAsync(response, ct);
    }
}