using Data.Contracts;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace AutoBuy;

public record GetOrderStepsResponse
{
    public List<OrderStepDto> OrderSteps { get; set; } = [];
}

public class GetOrderSteps : EndpointWithoutRequest<GetOrderStepsResponse>
{
    private readonly IAutoBuyDbContext _context;

    public GetOrderSteps(IAutoBuyDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Get("/order-steps");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var orderSteps = await _context.OrderSteps.ToListAsync(ct);

        var response = orderSteps.Select(os => new OrderStepDto
        {
            Id = os.Id,
            StepName = os.StepName,
            StepInJs = os.StepInJs,
            ProductEntityId = os.ProductEntityId
        }).ToList();

        await Send.OkAsync(new GetOrderStepsResponse { OrderSteps = response }, ct);
    }
}
