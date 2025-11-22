using Data.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data;

public class ProductConfigurationConfiguration : IEntityTypeConfiguration<ProductConfigurationEntity>
{
    public void Configure(EntityTypeBuilder<ProductConfigurationEntity> builder)
    {
        builder.Property(x=> x.RepeatEveryTimePeriod)
            .HasConversion<string>();
    }
}