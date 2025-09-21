using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Contracts;

[Table("brands")]
public class BrandEntity : BaseEntity
{
    public required string Name { get; set; }

    public required Uri BaseUrl { get; set; }

    public required Uri LogoUrl { get; set; }

    public required double MinimumFreeDeliveryPrice { get; set; }

    public List<ProductEntity> Products { get; set; } = [];
}