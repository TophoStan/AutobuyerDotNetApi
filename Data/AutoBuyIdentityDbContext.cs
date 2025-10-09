using Data.Contracts;
using Environment;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class AutoBuyIdentityDbContext : IdentityDbContext, IAutoBuyIdentityContext
{
    public async Task<IdentityResult> CreateAsync(IdentityUser user, string password, CancellationToken cancellationToken)
    {
        try
        {
            var passwordHasher = new PasswordHasher<IdentityUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, password);

            await base.Users.AddAsync(user, cancellationToken);
            await SaveChangesAsync(cancellationToken);
            return IdentityResult.Success;
        }
        catch (Exception e)
        {
            return IdentityResult.Failed(new IdentityError { Code = "CreateUserFailed", Description = e.Message });
        }
    }

    public Task<string> GenerateTokenAsync(IdentityUser user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;
        optionsBuilder.UseNpgsql(EnvironmentExtensions.GetIdentityPostgresConnectionString(), npgsqlOptions =>
        {
            npgsqlOptions.MigrationsAssembly("Data");
            npgsqlOptions.CommandTimeout(30);
        });
        optionsBuilder.EnableSensitiveDataLogging();
        base.OnConfiguring(optionsBuilder);
    }
}