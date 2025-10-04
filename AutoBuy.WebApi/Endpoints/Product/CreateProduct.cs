using Data.Contracts;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace AutoBuy;

public record CreateProductRequest
{
    public required string Name { get; set; }
    public required string Url { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public required Guid BrandEntityId { get; set; }
}

public class CreateProduct : Endpoint<CreateProductRequest, ProductSummaryDto>
{
    private readonly IAutoBuyDbContext _context;

    public CreateProduct(IAutoBuyDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Post("/products");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateProductRequest req, CancellationToken ct)
    {
        // Verify that the brand exists
        var brandExists = await _context.Brands.AnyAsync(b => b.Id == req.BrandEntityId, ct);
        if (!brandExists)
        {
            AddError("BrandEntityId", "Brand not found");
            await Send.ErrorsAsync(cancellation: ct);
            return;
        }

        var product = new ProductEntity
        {
            Id = Guid.NewGuid(),
            Name = req.Name,
            Url = new Uri(req.Url),
            Description = req.Description,
            Price = req.Price,
            BrandEntityId = req.BrandEntityId
        };

        _context.Products.Add(product);
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

        await Send.CreatedAtAsync<GetProduct>(new { id = product.Id }, response, cancellation: ct);
    }
}