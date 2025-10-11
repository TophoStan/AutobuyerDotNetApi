namespace GooglePlacesApi.Contracts;

public interface IGooglePlacesApi
{
    public Task<PlacesResponseDto> GetPlaceSuggestions(string input, string region);
    
    public Task<PlaceDetailsResponseDto> GetAddress(string placeId);
    
    
}