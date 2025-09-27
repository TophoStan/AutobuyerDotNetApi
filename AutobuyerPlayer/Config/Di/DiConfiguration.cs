using AutobuyerPlayer.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace AutobuyerPlayer;

public static class DiConfiguration
{
    public static IServiceCollection AddPlaywrightServerService(this IServiceCollection services)
    {
        services.AddScoped<IPlaywrightServerService, PlaywrightServerService>();
        return services;
    }
}