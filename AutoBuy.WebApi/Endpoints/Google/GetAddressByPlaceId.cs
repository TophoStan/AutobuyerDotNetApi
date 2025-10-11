using FastEndpoints;
using GooglePlacesApi.Contracts;

namespace AutoBuy.Google;

public record GetAddressByPlaceIdRequest
{
    public  required string PlaceId { get; set; }
}

public class GetAddressByPlaceId : Endpoint<GetAddressByPlaceIdRequest, PlaceDetailsResponseDto>
{
    private readonly IGooglePlacesApi _googlePlacesApi;

    public GetAddressByPlaceId(IGooglePlacesApi googlePlacesApi)
    {
        _googlePlacesApi = googlePlacesApi;
    }

    public override void Configure()
    {
        Get("/google/places/{placeId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetAddressByPlaceIdRequest req, CancellationToken ct)
    {
        var result = await _googlePlacesApi.GetAddress(placeId: req.PlaceId);
        await Send.OkAsync(result, ct);
    }
}