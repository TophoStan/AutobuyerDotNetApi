using Data.Contracts;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace AutoBuy.ProductConfiguration;

public record GetUserProductConfigurationsResponse
{
    public required ProductConfigurationDto[] Configurations { get; init; }
}

public record ProductConfigurationDto
{
    public required Guid Id { get; init; }
    
    public required ProductDto Product { get; init; }
    
    public required SelectedOptionDto[] SelectedOptions { get; init; }
    
    public required int RepeatEveryAmount { get; init; }
    
    public required TimePeriod RepeatEveryTimePeriod { get; init; }
    
    public required DateTime StartDate { get; init; }
    
    public required DateTime CreatedAt { get; init; }
    
    public required DateTime UpdatedAt { get; init; }
}

public record ProductDto
{
    public required Guid Id { get; init; }
    
    public required string Name { get; init; }
    
    public required string Url { get; init; }
    
    public required string Description { get; init; }
    
    public required decimal Price { get; init; }
    
    public required string ImageUrl { get; init; }
}

public record SelectedOptionDto
{
    public required Guid Id { get; init; }
    public required string ProductKey { get; init; }
    
    public required string Value { get; init; }
}

public class GetUserProductConfigurations : EndpointWithoutRequest<GetUserProductConfigurationsResponse>
{
    private readonly IAutoBuyDbContext _context;

    public GetUserProductConfigurations(IAutoBuyDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Get("/productconfiguration");
        AuthSchemes("Bearer");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var userId = User.FindFirst("id")?.Value;

        if (userId is null)
        {
            AddError("User", "User not found");
            await Send.ErrorsAsync(cancellation: ct);
            return;
        }

        var configurations = await _context.ProductConfigurations
            .Include(pc => pc.Product)
            .ThenInclude(p => p.Options)
            .Include(pc => pc.SelectedOptions)
            .ThenInclude(so => so.Option)
            .Where(pc => pc.UserId == userId)
            .ToListAsync(ct);

        var response = new GetUserProductConfigurationsResponse
        {
            Configurations = configurations.Select(c => c.ToDto()).ToArray()
        };

        await Send.OkAsync(response, cancellation: ct);
    }
}