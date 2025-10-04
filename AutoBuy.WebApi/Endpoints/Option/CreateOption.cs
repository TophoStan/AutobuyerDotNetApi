using Data.Contracts;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace AutoBuy;

public record CreateOptionRequest
{
    public required string Name { get; set; }
    public required List<string> Values { get; set; }
    public required Guid ProductEntityId { get; set; }
}

public class CreateOption : Endpoint<CreateOptionRequest, OptionDto>
{
    private readonly IAutoBuyDbContext _context;

    public CreateOption(IAutoBuyDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Post("/options");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateOptionRequest req, CancellationToken ct)
    {
        // Verify that the product exists
        var productExists = await _context.Products.AnyAsync(p => p.Id == req.ProductEntityId, ct);
        if (!productExists)
        {
            AddError("ProductEntityId", "Product not found");
            await Send.ErrorsAsync(cancellation: ct);
            return;
        }

        var option = new OptionEntity
        {
            Id = Guid.NewGuid(),
            Name = req.Name,
            Values = req.Values,
            ProductEntityId = req.ProductEntityId
        };

        _context.Options.Add(option);
        await _context.SaveChangesAsync(ct);

        var response = new OptionDto
        {
            Id = option.Id,
            Name = option.Name,
            Values = option.Values,
            ProductEntityId = option.ProductEntityId
        };

        await Send.CreatedAtAsync<GetOption>(new { id = option.Id }, response, cancellation: ct);
    }
}