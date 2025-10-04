using Data.Contracts;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace AutoBuy;

public record GetProductRequest
{
    public Guid Id { get; set; }
}

public class GetProduct : Endpoint<GetProductRequest, ProductDto>
{
    private readonly IAutoBuyDbContext _context;

    public GetProduct(IAutoBuyDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Get("/products/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetProductRequest req, CancellationToken ct)
    {
        var product = await _context.Products
            .Include(p => p.Options)
            .Include(p => p.OrderSteps)
            .FirstOrDefaultAsync(p => p.Id == req.Id, ct);

        if (product == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        var response = new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Url = product.Url.ToString(),
            Description = product.Description,
            Price = product.Price,
            BrandEntityId = product.BrandEntityId,
            Options = product.Options?.Select(o => new OptionDto
            {
                Id = o.Id,
                Name = o.Name,
                Values = o.Values,
                ProductEntityId = o.ProductEntityId
            }).ToList() ?? [],
            OrderSteps = product.OrderSteps?.Select(os => new OrderStepDto
            {
                Id = os.Id,
                StepName = os.StepName,
                StepInJs = os.StepInJs,
                ProductEntityId = os.ProductEntityId
            }).ToList() ?? []
        };

        await Send.OkAsync(response, ct);
    }
}
