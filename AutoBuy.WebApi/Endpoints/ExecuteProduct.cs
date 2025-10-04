using AutobuyerPlayer.Contracts;
using FastEndpoints;

namespace AutoBuy;

#region MyRegion

public record ExecuteProductRequest
{
    public string ProductName { get; set; }
}

public record ExecuteProductResponse
{
    public string ProductName { get; set; }
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
        Post("/exe/products");
        AllowAnonymous();
    }

    public override async Task HandleAsync(ExecuteProductRequest req, CancellationToken ct)
    {
        // await _playwrightService.Execute(req);
        await Send.OkAsync(new ExecuteProductResponse(), ct);
    }
}