using Data.Contracts;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace AutoBuy;

public record CreateOrderStepRequest
{
    public required string StepName { get; set; }
    public string? StepInJs { get; set; }
    public required Guid ProductEntityId { get; set; }
}

public class CreateOrderStep : Endpoint<CreateOrderStepRequest, OrderStepDto>
{
    private readonly IAutoBuyDbContext _context;

    public CreateOrderStep(IAutoBuyDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Post("/order-steps");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateOrderStepRequest req, CancellationToken ct)
    {
        // Verify that the product exists
        var productExists = await _context.Products.AnyAsync(p => p.Id == req.ProductEntityId, ct);
        if (!productExists)
        {
            AddError("ProductEntityId", "Product not found");
            await Send.ErrorsAsync(cancellation: ct);
            return;
        }

        var orderStep = new OrderStepEntity
        {
            Id = Guid.NewGuid(),
            StepName = req.StepName,
            StepInJs = req.StepInJs,
            ProductEntityId = req.ProductEntityId
        };

        _context.OrderSteps.Add(orderStep);
        await _context.SaveChangesAsync(ct);

        var response = new OrderStepDto
        {
            Id = orderStep.Id,
            StepName = orderStep.StepName,
            StepInJs = orderStep.StepInJs,
            ProductEntityId = orderStep.ProductEntityId
        };

        await Send.CreatedAtAsync<GetOrderStep>(new { id = orderStep.Id }, response, cancellation: ct);
    }
}
