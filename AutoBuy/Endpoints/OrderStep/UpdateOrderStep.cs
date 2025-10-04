using Data.Contracts;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace AutoBuy;

public record UpdateOrderStepRequest
{
    public required string StepName { get; set; }
    public string? StepInJs { get; set; }
    public required Guid ProductEntityId { get; set; }
}

public class UpdateOrderStep : Endpoint<UpdateOrderStepRequest, OrderStepDto>
{
    private readonly IAutoBuyDbContext _context;

    public UpdateOrderStep(IAutoBuyDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Put("/order-steps/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateOrderStepRequest req, CancellationToken ct)
    {
        var id = Route<Guid>("id");
        var orderStep = await _context.OrderSteps.FirstOrDefaultAsync(os => os.Id == id, ct);

        if (orderStep == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        // Verify that the product exists
        var productExists = await _context.Products.AnyAsync(p => p.Id == req.ProductEntityId, ct);
        if (!productExists)
        {
            AddError("ProductEntityId", "Product not found");
            await Send.ErrorsAsync(cancellation: ct);
            return;
        }

        orderStep.StepName = req.StepName;
        orderStep.StepInJs = req.StepInJs;
        orderStep.ProductEntityId = req.ProductEntityId;

        await _context.SaveChangesAsync(ct);

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
