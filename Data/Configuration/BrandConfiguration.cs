using Data.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data;

public class BrandConfiguration : IEntityTypeConfiguration<BrandEntity>
{
    public static Guid UpfrontGuid = new Guid("4542dfea86e44fce9446c1b840fbefa0");

    public void Configure(EntityTypeBuilder<BrandEntity> builder)
    {
        builder.HasData(new BrandEntity
        {
            Id = UpfrontGuid,
            Name = "Upfront",
            BaseUrl = new Uri("https://www.upfront.nl/"),
            LogoUrl = new Uri("https://upfront.nl/cdn/shop/files/LOGO-INVOICE.svg?v=1687087217&width=80"),
            MinimumFreeDeliveryPrice = 49
        });
    }
}