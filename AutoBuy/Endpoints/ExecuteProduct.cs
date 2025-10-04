using AutobuyerPlayer.Contracts;
using FastEndpoints;

namespace AutoBuy;

#region MyRegion

public record ExecuteProductRequest
{
}

public record ExecuteProductResponse
{
}

#endregion

public class ExecuteProduct : Endpoint<ExecuteProductRequest, ExecuteProductResponse>
{
    private readonly IPlaywrightServerService _playwrightService;

    public ExecuteProduct(IPlaywrightServerService playwrightService)
    {
        _playwrightService = playwrightService;
    }

    public override void Configure()
    {
        Post("/products");
        AllowAnonymous();
    }

    public override async Task HandleAsync(ExecuteProductRequest req, CancellationToken ct)
    {
        // await _playwrightService.Execute(req);
        await Send.OkAsync(new ExecuteProductResponse(), ct);
    }
}