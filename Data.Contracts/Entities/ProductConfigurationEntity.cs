namespace Data.Contracts;

public class ProductConfigurationEntity : BaseDateEntity
{
    public string UserId { get; set; }
    
    public ProductEntity Product { get; set; }
    
    public Guid ProductId { get; set; }
    
    public List<SelectedOptionEntity> SelectedOptions { get; set; }
    
    public int RepeatEveryAmount { get; set; }
    
    public TimePeriod RepeatEveryTimePeriod { get; set; }
    
    public DateTime StartDate { get; set; }
}

public enum TimePeriod
{
    NONE = -1,
    Day = 0,
    Week = 1,
    Month = 2,
    Year = 3
}