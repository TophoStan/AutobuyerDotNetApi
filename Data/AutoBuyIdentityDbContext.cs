using Data.Contracts;
using Environment;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class AutoBuyIdentityDbContext : IdentityDbContext<AutoBuyIdentityUser>, IAutoBuyIdentityContext
{



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