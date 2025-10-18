using Data.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Data;

public static class DatabaseDiConfiguration
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<AutoBuyDbContext>();
        services.AddDbContext<AutoBuyIdentityDbContext>();
        services.AddScoped<IAutoBuyDbContext, AutoBuyDbContext>();
        services.AddScoped<IAutoBuyIdentityContext, AutoBuyIdentityDbContext>();

        services.AddIdentityCore<AutoBuyIdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            })
            .AddEntityFrameworkStores<AutoBuyIdentityDbContext>()
            .AddSignInManager<SignInManager<AutoBuyIdentityUser>>() // 👈 This line adds SignInManager
            .AddDefaultTokenProviders(); // optional, if you need password reset / confirmation tokens


        return services;
    }
}