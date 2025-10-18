using System.Net.Http.Json;
using System.Web;
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

        var urlbuilder = new UriBuilder("https://maps.googleapis.com/maps/api/place/autocomplete/json");
        var query = HttpUtility.ParseQueryString(urlbuilder.Query);      
        query["input"] = input;
        query["region"] = region;
        query["key"] = _apiKey;
        
        urlbuilder.Query = query.ToString();
        var finalUri = urlbuilder.Uri;
        return await _httpClient.GetFromJsonAsync<PlacesResponseDto>(
            finalUri,
            CancellationToken.None) ?? new PlacesResponseDto();
    }

    public async Task<PlaceDetailsResponseDto> GetAddress(string placeId)
    {
        return await _httpClient.GetFromJsonAsync<PlaceDetailsResponseDto>(
            $"https://maps.googleapis.com/maps/api/place/details/json?place_id={placeId}&key={_apiKey}&fields=address_component",
            CancellationToken.None) ?? new PlaceDetailsResponseDto();
    }
}