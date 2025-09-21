using Data.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data;

public class OptionConfiguration : IEntityTypeConfiguration<OptionEntity>
{
    public static Guid WheyFlavorOptionGuid = new Guid("d4e5f6a7-b8c9-0123-def4-56789012345a");

    public void Configure(EntityTypeBuilder<OptionEntity> builder)
    {
        builder.HasData(
        
        new OptionEntity
        {
            Id = WheyFlavorOptionGuid,
            Name = "Whey_flavour",
            ProductEntityId = ProductConfiguration.WheyProteinGuid,
            Values = ["Banaan", "Vanille", "Speculaas", "Chocolade", "Aardbei", "Framboos"]
        }
     );
    }
}