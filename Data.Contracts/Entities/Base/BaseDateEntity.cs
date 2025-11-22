namespace Data.Contracts;

public class BaseDateEntity : BaseEntity
{
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
}