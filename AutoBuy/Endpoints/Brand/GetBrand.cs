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
        var brand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == req.Id, ct);

        if (brand == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        var response = new BrandDto()
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
