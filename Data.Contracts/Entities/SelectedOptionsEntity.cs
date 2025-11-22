namespace Data.Contracts;

public class SelectedOptionEntity : BaseEntity
{
    public Guid ProductConfigurationId { get; set; }
    public ProductConfigurationEntity  ProductConfiguration { get; set; }
    
    public Guid OptionId { get; set; }
    
    public OptionEntity Option { get; set; }
    
    public string Value { get; set; }
}