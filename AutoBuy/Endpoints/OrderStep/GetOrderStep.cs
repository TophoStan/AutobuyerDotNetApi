using Data.Contracts;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace AutoBuy;

public record GetOrderStepRequest
{
    public Guid Id { get; set; }
}

public class GetOrderStep : Endpoint<GetOrderStepRequest, OrderStepDto>
{
    private readonly IAutoBuyDbContext _context;

    public GetOrderStep(IAutoBuyDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Get("/order-steps/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetOrderStepRequest req, CancellationToken ct)
    {
        var orderStep = await _context.OrderSteps.FirstOrDefaultAsync(os => os.Id == req.Id, ct);

        if (orderStep == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        var response = new OrderStepDto
        {
            Id = orderStep.Id,
            StepName = orderStep.StepName,
            StepInJs = orderStep.StepInJs,
            ProductEntityId = orderStep.ProductEntityId
        };

        await Send.OkAsync(response, ct);
    }
}
