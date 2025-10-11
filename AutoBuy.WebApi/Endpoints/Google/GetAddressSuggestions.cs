namespace AutoBuy.Google;

using FastEndpoints;
using GooglePlacesApi.Contracts;


public record GetAddressSuggestionsRequest
{
    public required string Input { get; set; }
    
    public required string Region { get; set; } 
}

public class GetAddressSuggestions : Endpoint<GetAddressSuggestionsRequest, PlacesResponseDto>
{
    private readonly IGooglePlacesApi _googlePlacesApi;

    public GetAddressSuggestions(IGooglePlacesApi googlePlacesApi)
    {
        _googlePlacesApi = googlePlacesApi;
    }

    public override void Configure()
    {
        Post("/google/places/suggestions");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetAddressSuggestionsRequest req, CancellationToken ct)
    {
        var result = await _googlePlacesApi.GetPlaceSuggestions(input: req.Input, region: req.Region);
        await Send.OkAsync(result, ct);
    }
}