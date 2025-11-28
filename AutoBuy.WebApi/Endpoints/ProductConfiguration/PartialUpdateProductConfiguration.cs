using AutoBuy.Dtos;
using Data.Contracts;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace AutoBuy.ProductConfiguration;

public record UpdateProductConfigurationRequest
{
    public Guid Id { get; set; }

    public UpdateProductConfigurationDto Product { get; set; }
}

public record UpdateProductConfigurationDto{
    public int? RepeatEveryAmount { get; set; }
    public Dtos.TimePeriod? RepeatEveryTimePeriod { get; set; }
    public DateTime? StartDate { get; set; }
    public ConfiguredProductOptionsDto[]? Options { get; set; }
}

public record UpdateProductConfigurationResponse
{
    public required ProductConfigurationDto Product { get; set; }
}

public class PartialUpdateProductConfiguration : Endpoint<UpdateProductConfigurationRequest, UpdateProductConfigurationResponse>
{
    private readonly IAutoBuyDbContext _context;

    public PartialUpdateProductConfiguration(IAutoBuyDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Patch("/productconfiguration/{Id}");
        AuthSchemes("Bearer");
    }

    public override async Task HandleAsync(UpdateProductConfigurationRequest req, CancellationToken ct)
    {
        var productConfiguration = await _context.ProductConfigurations.FirstOrDefaultAsync(pc => pc.Id == req.Id, ct);
        if (productConfiguration == null){
            AddError("Id", "Product configuration not found");
            await Send.ErrorsAsync(cancellation: ct);
            return;
        }
        // Is users
        if (productConfiguration.UserId != User.FindFirst("id")?.Value){
            AddError("Id", "You are not the owner of this product configuration");
            await Send.ErrorsAsync(cancellation: ct);
            return;
        }
        // Update the product configuration
        if (req.Product.RepeatEveryAmount != null) productConfiguration.RepeatEveryAmount = req.Product.RepeatEveryAmount.Value;
        if (req.Product.RepeatEveryTimePeriod != null) productConfiguration.RepeatEveryTimePeriod = (Data.Contracts.TimePeriod) req.Product.RepeatEveryTimePeriod.Value;
        if (req.Product.StartDate != null) productConfiguration.StartDate = req.Product.StartDate.Value;
        if (req.Product.Options != null) productConfiguration.SelectedOptions = req.Product.Options.Select(o => o.ToEntity(productConfiguration)).ToList();
        await _context.SaveChangesAsync(ct);
        var response = new UpdateProductConfigurationResponse
        {
            Product = productConfiguration.ToDto(),
        };
        await Send.OkAsync(response, cancellation: ct);
    }
}