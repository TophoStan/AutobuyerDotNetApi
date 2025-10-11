using GooglePlacesApi.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace GooglePlaces;

public static class GooglePlacesConfig
{
    public static IServiceCollection AddGooglePlacesApi(this IServiceCollection serviceColection)
    {
        serviceColection.AddScoped<IGooglePlacesApi, GooglePlacesApi>();
        return serviceColection;
    }
}