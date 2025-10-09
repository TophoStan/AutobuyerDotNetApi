using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Data.Contracts;

public interface IAutoBuyIdentityContext
{
    

    Task<string> GenerateTokenAsync(IdentityUser user, CancellationToken cancellationToken);
}