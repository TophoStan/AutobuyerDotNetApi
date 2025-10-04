using Data.Contracts;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace AutoBuy;

public record UpdateProductRequest
{
    public required string Name { get; set; }
    public required string Url { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public required Guid BrandEntityId { get; set; }
}

public class UpdateProduct : Endpoint<UpdateProductRequest, ProductSummaryDto>
{
    private readonly IAutoBuyDbContext _context;

    public UpdateProduct(IAutoBuyDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Put("/products/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateProductRequest req, CancellationToken ct)
    {
        var id = Route<Guid>("id");
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id, ct);

        if (product == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        // Verify that the brand exists
        var brandExists = await _context.Brands.AnyAsync(b => b.Id == req.BrandEntityId, ct);
        if (!brandExists)
        {
            AddError("BrandEntityId", "Brand not found");
            await Send.ErrorsAsync(cancellation: ct);
            return;
        }

        product.Name = req.Name;
        product.Url = new Uri(req.Url);
        product.Description = req.Description;
        product.Price = req.Price;
        product.BrandEntityId = req.BrandEntityId;

        await _context.SaveChangesAsync(ct);

        var response = new ProductSummaryDto
        {
            Id = product.Id,
            Name = product.Name,
            Url = product.Url.ToString(),
            Description = product.Description,
            Price = product.Price,
            BrandEntityId = product.BrandEntityId
        };

        await Send.OkAsync(response, ct);
    }
}