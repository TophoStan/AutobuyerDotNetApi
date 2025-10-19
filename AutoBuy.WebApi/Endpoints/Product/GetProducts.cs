using Data.Contracts;
using Data.Contracts.Extensions;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace AutoBuy;

public record GetProductsRequest
{
    [QueryParam]
    public Guid? BrandId { get; set; }
}

public record GetProductsResponse
{
    public List<ProductDto> Products { get; set; } = [];
}

public class GetProducts : Endpoint<GetProductsRequest, GetProductsResponse>
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

    public override async Task HandleAsync(GetProductsRequest req, CancellationToken ct)
    {
        var query = _context.Products.AsQueryable().Include(x=> x.Options);

        if (req.BrandId.HasValue)
        {
            query = query.Where(p => p.BrandEntityId == req.BrandId.Value).Include(x=> x.Options);
        }

        var products = await query.ToListAsync(ct);

        var response = products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Url = p.Url.ToString(),
            Description = p.Description,
            Price = p.Price,
            BrandEntityId = p.BrandEntityId,
            ImageUrl = p.ImageUrl,
            Options = p.Options.Select(o=> new OptionDto
            {
                Id = o.Id,
                Name = o.Name,
                Values = o.Values,
                ProductEntityId = o.ProductEntityId
            }).ToList()
            
        }).ToList();

        await Send.OkAsync(new GetProductsResponse { Products = response }, ct);
    }
}