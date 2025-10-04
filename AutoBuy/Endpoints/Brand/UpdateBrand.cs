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
        var brand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == id, ct);

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
            MinimumFreeDeliveryPrice = brand.MinimumFreeDeliveryPrice
        };

        await Send.OkAsync(response, ct);
    }
}
