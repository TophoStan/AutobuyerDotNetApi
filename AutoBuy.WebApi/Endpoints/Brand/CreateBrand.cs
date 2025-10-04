using Data.Contracts;
using FastEndpoints;

namespace AutoBuy;

public record CreateBrandRequest
{
    public required string Name { get; set; }
    public required string BaseUrl { get; set; }
    public required string LogoUrl { get; set; }
    public required double MinimumFreeDeliveryPrice { get; set; }
}

public class CreateBrand : Endpoint<CreateBrandRequest, BrandDto>
{
    private readonly IAutoBuyDbContext _context;

    public CreateBrand(IAutoBuyDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Post("/brands");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateBrandRequest req, CancellationToken ct)
    {
        var brand = new BrandEntity
        {
            Id = Guid.NewGuid(),
            Name = req.Name,
            BaseUrl = new Uri(req.BaseUrl),
            LogoUrl = new Uri(req.LogoUrl),
            MinimumFreeDeliveryPrice = req.MinimumFreeDeliveryPrice
        };

        _context.Brands.Add(brand);
        await _context.SaveChangesAsync(ct);

        var response = new BrandDto
        {
            Id = brand.Id,
            Name = brand.Name,
            BaseUrl = brand.BaseUrl.ToString(),
            LogoUrl = brand.LogoUrl.ToString(),
            MinimumFreeDeliveryPrice = brand.MinimumFreeDeliveryPrice,
            Products = [] // New brand has no products initially
        };

        await Send.CreatedAtAsync<GetBrand>(new { id = brand.Id }, response, cancellation: ct);
    }
}