using System.Text.Json.Serialization;
using Data.Contracts;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace AutoBuy;

public record GetBrandsResponse
{
        public List<BrandDto> Brands { get; set; } = [];
}

public class GetBrands : EndpointWithoutRequest<GetBrandsResponse>
{
    private readonly IAutoBuyDbContext _context;

    public GetBrands(IAutoBuyDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Get("/brands");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var brands = await _context.Brands
            .Include(b => b.Products)
                .ThenInclude(p => p.Options)
            .Include(b => b.Products)
                .ThenInclude(p => p.OrderSteps)
            .ToListAsync(ct);

        var response = brands.Select(b => new BrandDto
        {
            Id = b.Id,
            Name = b.Name,
            BaseUrl = b.BaseUrl.ToString(),
            LogoUrl = b.LogoUrl.ToString(),
            MinimumFreeDeliveryPrice = b.MinimumFreeDeliveryPrice,
            Products = b.Products?.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Url = p.Url.ToString(),
                Description = p.Description,
                Price = p.Price,
                BrandEntityId = p.BrandEntityId,
                ImageUrl = p.ImageUrl,
                Options = p.Options?.Select(o => new OptionDto
                {
                    Id = o.Id,
                    Name = o.Name,
                    Values = o.Values,
                    ProductEntityId = o.ProductEntityId
                }).ToList() ?? [],
                OrderSteps = p.OrderSteps?.Select(os => new OrderStepDto
                {
                    Id = os.Id,
                    StepName = os.StepName,
                    StepInJs = os.StepInJs,
                    ProductEntityId = os.ProductEntityId
                }).ToList() ?? []
            }).ToList() ?? []
        }).ToList();

        await Send.OkAsync(new GetBrandsResponse { Brands = response }, ct);
    }
}