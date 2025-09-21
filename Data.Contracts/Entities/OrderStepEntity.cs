using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Contracts;

[Table("order_steps")]
public class OrderStepEntity: BaseEntity
{
    public string StepName {get; set;} 
    
    public string? StepInJs {get; set;}
    
    public ProductEntity ProductEntity {get; set;}
    
    public Guid ProductEntityId {get; set;}
    
}