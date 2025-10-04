using Data.Contracts;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace AutoBuy;

public record DeleteOrderStepRequest
{
    public Guid Id { get; set; }
}

public class DeleteOrderStep : Endpoint<DeleteOrderStepRequest>
{
    private readonly IAutoBuyDbContext _context;

    public DeleteOrderStep(IAutoBuyDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Delete("/order-steps/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteOrderStepRequest req, CancellationToken ct)
    {
        var orderStep = await _context.OrderSteps.FirstOrDefaultAsync(os => os.Id == req.Id, ct);

        if (orderStep == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        _context.OrderSteps.Remove(orderStep);
        await _context.SaveChangesAsync(ct);

        await Send.NoContentAsync(ct);
    }
}
