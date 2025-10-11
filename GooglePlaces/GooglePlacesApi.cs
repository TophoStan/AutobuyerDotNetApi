using GooglePlacesApi.Contracts;

namespace GooglePlaces;

public class GooglePlacesApi : IGooglePlacesApi
{
    private readonly string _apiKey;
    private readonly HttpClient _httpClient;

    public GooglePlacesApi(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PlacesResponseDto> GetPlaceSuggestions(string input, string region)
    {
        throw new NotImplementedException();
    }

    public async Task<PlaceDetailsResponseDto> GetAddress(string placeId)
    {
        throw new NotImplementedException();
    }
}