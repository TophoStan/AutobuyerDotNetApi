using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Contracts;

[Table("options")]
public class OptionEntity: BaseEntity
{
    public string Name { get; set; } 
    
    public List<string> Values { get; set; }
    
    public ProductEntity ProductEntity {get; set;}
    
    public Guid ProductEntityId {get; set;}
}