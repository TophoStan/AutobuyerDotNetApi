namespace Data.Contracts;

public class ProductEntity : BaseEntity
{
    public required string Name { get; set; } 

    public required Uri Url { get; set; } 
    
    public required string Description { get; set; }

    public required decimal Price { get; set; }
    
    public List<OptionEntity> Options {get; set;} 

    public List<OrderStepEntity> OrderSteps { get; set; } 
    
    public BrandEntity BrandEntity { get; set; }
    
    public Guid BrandEntityId { get; set; }
    
    public required Uri ImageUrl { get; set; }
}