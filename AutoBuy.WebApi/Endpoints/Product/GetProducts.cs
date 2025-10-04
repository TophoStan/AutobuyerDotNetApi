using Data.Contracts;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace AutoBuy;

public record GetProductsResponse
{
    public List<ProductSummaryDto> Products { get; set; } = [];
}

public class GetProducts : EndpointWithoutRequest<GetProductsResponse>
{
    private readonly IAutoBuyDbContext _context;

    public GetProducts(IAutoBuyDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Get("/products");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var products = await _context.Products.ToListAsync(ct);

        var response = products.Select(p => new ProductSummaryDto
        {
            Id = p.Id,
            Name = p.Name,
            Url = p.Url.ToString(),
            Description = p.Description,
            Price = p.Price,
            BrandEntityId = p.BrandEntityId
        }).ToList();

        await Send.OkAsync(new GetProductsResponse { Products = response }, ct);
    }
}