using AutoBuy.Dtos;
using Data.Contracts;
using FastEndpoints;

namespace AutoBuy.ProductConfiguration;

public record CreateProductConfigurationRequest
{
    public required ConfiguredProductDto[] Products { get; init; }
}

public record CreateProductConfigurationResponse
{
    public required string Id { get; init; }
}


public class CreateProductConfiguration : Endpoint<CreateProductConfigurationRequest, CreateProductConfigurationResponse>
{
    private readonly IAutoBuyDbContext _context;

    public CreateProductConfiguration(IAutoBuyDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Post("/productconfiguration");
        
        AuthSchemes("Bearer");
    }

    public override async Task HandleAsync(CreateProductConfigurationRequest req, CancellationToken ct)
    {
        var userId = User.FindFirst("id")?.Value;

        if (userId is null)
        {
            AddError("User", "User not found");
            await Send.ErrorsAsync(cancellation: ct);
            return;
        }

        var productConfigurations = req.Products.Select(p => p.ToEntity(userId));
        _context.ProductConfigurations.AddRange(productConfigurations);
        await _context.SaveChangesAsync(ct);

        var response = new CreateProductConfigurationResponse
        {
            Id = "",
        };

        await Send.OkAsync(response, cancellation: ct);
    }
}