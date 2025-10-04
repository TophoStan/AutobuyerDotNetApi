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
        var brands = await _context.Brands.ToListAsync(ct);

        var response = brands.Select(b => new BrandDto
        {
            Id = b.Id,
            Name = b.Name,
            BaseUrl = b.BaseUrl.ToString(),
            LogoUrl = b.LogoUrl.ToString(),
            MinimumFreeDeliveryPrice = b.MinimumFreeDeliveryPrice
        }).ToList();

        await Send.OkAsync(new GetBrandsResponse{Brands = response}, ct);
    }
}
