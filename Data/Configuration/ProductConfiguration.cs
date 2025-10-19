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
            ImageUrl = new Uri("https://cdn.shopify.com/s/files/1/0254/4667/8590/files/nw_WheyMilkshake_Vanille_still_final_v1_0b704c6c-7f7b-484d-bb65-7d12e1c7834a.jpg")
        });
    }
}