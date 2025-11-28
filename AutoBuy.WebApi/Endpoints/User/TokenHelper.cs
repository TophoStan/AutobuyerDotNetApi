using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Environment;
using Microsoft.IdentityModel.Tokens;

namespace AutoBuy;

public static class TokenHelper
{
    public static JwtSecurityToken DecodeToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var decodedToken = tokenHandler.ReadJwtToken(token);
        return decodedToken;
    }

    public static bool IsTokenExpired(this JwtSecurityToken token)
    {
        return token.ValidTo < DateTime.UtcNow;
    }

    public static bool VerifyTokenSignature(string tokenString)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false, // Don't validate expiry here - check separately
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(EnvironmentExtensions.GetJwtSigningKey())),
            };
            
            // This will throw if the signature is invalid
            tokenHandler.ValidateToken(tokenString, validationParameters, out var validatedToken);
            return true;
        }
        catch (SecurityTokenException)
        {
            // Token signature is invalid or token is malformed
            return false;
        }
        catch (Exception)
        {
            // Other validation errors
            return false;
        }
    }
}