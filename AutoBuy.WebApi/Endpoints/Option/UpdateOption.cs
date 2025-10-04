using Data.Contracts;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace AutoBuy;

public record UpdateOptionRequest
{
    public required string Name { get; set; }
    public required List<string> Values { get; set; }
    public required Guid ProductEntityId { get; set; }
}

public class UpdateOption : Endpoint<UpdateOptionRequest, OptionDto>
{
    private readonly IAutoBuyDbContext _context;

    public UpdateOption(IAutoBuyDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Put("/options/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateOptionRequest req, CancellationToken ct)
    {
        var id = Route<Guid>("id");
        var option = await _context.Options.FirstOrDefaultAsync(o => o.Id == id, ct);

        if (option == null)
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

        option.Name = req.Name;
        option.Values = req.Values;
        option.ProductEntityId = req.ProductEntityId;

        await _context.SaveChangesAsync(ct);

        var response = new OptionDto
        {
            Id = option.Id,
            Name = option.Name,
            Values = option.Values,
            ProductEntityId = option.ProductEntityId
        };

        await Send.OkAsync(response, ct);
    }
}
