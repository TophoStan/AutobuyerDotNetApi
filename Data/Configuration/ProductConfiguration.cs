using Data.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data;

public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
{
    public static Guid WheyProteinGuid = new Guid("393d15343f8e4b47bdaeed974c609982");
    
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.HasData(new ProductEntity
        {
            BrandEntityId = BrandConfiguration.UpfrontGuid,
            Name = "Why protein",
            Url = new Uri("https://upfront.nl/products/whey"),
            Description = "Proteine enzo",
            Price = 24,
            Id = WheyProteinGuid,
        });
    }
}