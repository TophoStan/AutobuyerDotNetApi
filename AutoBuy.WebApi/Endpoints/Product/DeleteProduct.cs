using Data.Contracts;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace AutoBuy;

public record DeleteProductRequest
{
    public Guid Id { get; set; }
}

public class DeleteProduct : Endpoint<DeleteProductRequest>
{
    private readonly IAutoBuyDbContext _context;

    public DeleteProduct(IAutoBuyDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Delete("/products/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteProductRequest req, CancellationToken ct)
    {
        var product = await _context.Products
            .Include(p => p.Options)
            .Include(p => p.OrderSteps)
            .FirstOrDefaultAsync(p => p.Id == req.Id, ct);

        if (product == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        // Remove related options and order steps (cascade delete)
        if (product.Options != null)
        {
            _context.Options.RemoveRange(product.Options);
        }

        if (product.OrderSteps != null)
        {
            _context.OrderSteps.RemoveRange(product.OrderSteps);
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync(ct);

        await Send.NoContentAsync(ct);
    }
}
