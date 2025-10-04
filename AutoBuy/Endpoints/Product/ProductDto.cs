namespace AutoBuy;

public record ProductDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; } = string.Empty;
    public required string Url { get; set; } = string.Empty;
    public required string Description { get; set; } = string.Empty;
    public required decimal Price { get; set; }
    public required Guid BrandEntityId { get; set; }
    public List<OptionDto> Options { get; set; } = [];
    public List<OrderStepDto> OrderSteps { get; set; } = [];
}

public record ProductSummaryDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; } = string.Empty;
    public required string Url { get; set; } = string.Empty;
    public required string Description { get; set; } = string.Empty;
    public required decimal Price { get; set; }
    public required Guid BrandEntityId { get; set; }
}

