namespace AutoBuy.Dtos;

public record ConfiguredProductDto
{
    public required Guid Id { get; init; }
    
    public required ConfiguredProductOptionsDto[] Options { get; init; }
    
    public required int RepeatEveryAmount { get; init; }
    
    public required TimePeriod RepeatEveryTimePeriod { get; init; }
    
    public required DateTime StartDate { get; init; }
}

public enum TimePeriod
{
    NONE = -1,
    Day = 0,
    Week = 1,
    Month = 2,
    Year = 3
}


public record ConfiguredProductOptionsDto
{
    public required Guid Id { get; init; }
    
    public required string Value { get; init; }
}