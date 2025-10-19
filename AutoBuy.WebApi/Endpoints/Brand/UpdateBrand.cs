using Data.Contracts;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace AutoBuy;

public record UpdateBrandRequest
{
    public required string Name { get; set; }
    public required string BaseUrl { get; set; }
    public required string LogoUrl { get; set; }
    public required double MinimumFreeDeliveryPrice { get; set; }
}

public class UpdateBrand : Endpoint<UpdateBrandRequest, BrandDto>
{
    private readonly IAutoBuyDbContext _context;

    public UpdateBrand(IAutoBuyDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Put("/brands/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateBrandRequest req, CancellationToken ct)
    {
        var id = Route<Guid>("id");
        var brand = await _context.Brands
            .Include(b => b.Products)
                .ThenInclude(p => p.Options)
            .Include(b => b.Products)
                .ThenInclude(p => p.OrderSteps)
            .FirstOrDefaultAsync(b => b.Id == id, ct);

        if (brand == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        brand.Name = req.Name;
        brand.BaseUrl = new Uri(req.BaseUrl);
        brand.LogoUrl = new Uri(req.LogoUrl);
        brand.MinimumFreeDeliveryPrice = req.MinimumFreeDeliveryPrice;

        await _context.SaveChangesAsync(ct);

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
        };

        await Send.OkAsync(response, ct);
    }
}