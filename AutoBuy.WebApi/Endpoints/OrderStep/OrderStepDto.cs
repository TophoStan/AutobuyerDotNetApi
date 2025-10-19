using System.Text.Json.Serialization;

namespace AutoBuy;

public record OrderStepDto
{
        public required Guid Id { get; set; }
        public required string StepName { get; set; } = string.Empty;
        public string? StepInJs { get; set; }
        public required Guid ProductEntityId { get; set; }
}
