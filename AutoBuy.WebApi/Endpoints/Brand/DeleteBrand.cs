using Data.Contracts;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace AutoBuy;

public record DeleteBrandRequest
{
    public Guid Id { get; set; }
}

public class DeleteBrand : Endpoint<DeleteBrandRequest>
{
    private readonly IAutoBuyDbContext _context;

    public DeleteBrand(IAutoBuyDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Delete("/brands/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteBrandRequest req, CancellationToken ct)
    {
        var brand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == req.Id, ct);

        if (brand == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        _context.Brands.Remove(brand);
        await _context.SaveChangesAsync(ct);

        await Send.NoContentAsync(ct);
    }
}