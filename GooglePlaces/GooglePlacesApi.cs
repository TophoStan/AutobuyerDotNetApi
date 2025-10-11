using System.Net.Http.Json;
using Environment;
using GooglePlacesApi.Contracts;

namespace GooglePlaces;

public class GooglePlacesApi : IGooglePlacesApi
{
    private readonly string _apiKey;
    private readonly HttpClient _httpClient;

    public GooglePlacesApi(HttpClient httpClient)
    {
        _apiKey = EnvironmentExtensions.GetGooglePlacesApiKey();
        _httpClient = httpClient;
    }

    public async Task<PlacesResponseDto> GetPlaceSuggestions(string input, string region)
    {
        return await _httpClient.GetFromJsonAsync<PlacesResponseDto>(
            $"https://maps.googleapis.com/maps/api/place/autocomplete/json?input={input}a&key={_apiKey}&region={region}",
            CancellationToken.None) ?? new PlacesResponseDto();
    }

    public async Task<PlaceDetailsResponseDto> GetAddress(string placeId)
    {
        return await _httpClient.GetFromJsonAsync<PlaceDetailsResponseDto>(
            $"https://maps.googleapis.com/maps/api/place/details/json?place_id={placeId}&key={_apiKey}&fields=address_component",
            CancellationToken.None) ?? new PlaceDetailsResponseDto();
    }
}