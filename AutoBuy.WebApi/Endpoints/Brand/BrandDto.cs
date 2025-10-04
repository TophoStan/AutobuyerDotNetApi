namespace AutoBuy;

public record BrandDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; } = string.Empty;
    public required string BaseUrl { get; set; } = string.Empty;
    public required string LogoUrl { get; set; } = string.Empty;
    public required double MinimumFreeDeliveryPrice { get; set; }
}