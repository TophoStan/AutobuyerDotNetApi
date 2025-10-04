namespace AutoBuy;

public record OptionDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; } = string.Empty;
    public required List<string> Values { get; set; } = [];
    public required Guid ProductEntityId { get; set; }
}
