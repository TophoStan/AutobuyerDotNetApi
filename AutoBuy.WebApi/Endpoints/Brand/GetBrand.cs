using Data.Contracts;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace AutoBuy;

public record GetBrandRequest
{
    public Guid Id { get; set; }
}

public class GetBrand : Endpoint<GetBrandRequest, BrandDto>
{
    private readonly IAutoBuyDbContext _context;

    public GetBrand(IAutoBuyDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Get("/brands/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetBrandRequest req, CancellationToken ct)
    {
        var brand = await _context.Brands
            .Include(b => b.Products)
                .ThenInclude(p => p.Options)
            .Include(b => b.Products)
                .ThenInclude(p => p.OrderSteps)
            .FirstOrDefaultAsync(b => b.Id == req.Id, ct);

        if (brand == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        var response = new BrandDto
        {
            Id = brand.Id,
            Name = brand.Name,
            BaseUrl = brand.BaseUrl.ToString(),
            LogoUrl = brand.LogoUrl.ToString(),
            MinimumFreeDeliveryPrice = brand.MinimumFreeDeliveryPrice,
            Products = brand.Products?.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Url = p.Url.ToString(),
                Description = p.Description,
                Price = p.Price,
                BrandEntityId = p.BrandEntityId,
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
        };

        await Send.OkAsync(response, ct);
    }
}