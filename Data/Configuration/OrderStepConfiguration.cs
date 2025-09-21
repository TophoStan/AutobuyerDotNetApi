using Data.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data;

public class OrderStepConfiguration : IEntityTypeConfiguration<OrderStepEntity>
{
    public static Guid OpenFlavorsStepGuid = new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890");
    public static Guid SelectFlavorStepGuid = new Guid("b2c3d4e5-f6a7-8901-bcde-f23456789012");
    public static Guid AddToCartStepGuid = new Guid("c3d4e5f6-a7b8-9012-cdef-345678901234");

    public void Configure(EntityTypeBuilder<OrderStepEntity> builder)
    {
        builder.HasData(
            [
                new OrderStepEntity
                {
                    Id = OpenFlavorsStepGuid,
                    StepName = "Open smaken",
                    ProductEntityId = ProductConfiguration.WheyProteinGuid,
                    StepInJs = "await page.click('xo-toggle-trigger[xo-name=\"variant-flavour-toggle-main-product\"]')"
                },
                new OrderStepEntity
                {
                    Id = SelectFlavorStepGuid,
                    StepName = "Klik op smaak",
                    ProductEntityId = ProductConfiguration.WheyProteinGuid,
                    StepInJs =
                        "await page\n            .locator('div.variant-flavour-selector__popover-inner')\n            .locator('span')\n            .filter({ hasText: 'Vanille' })\n            .first()\n            .click();"
                },new OrderStepEntity
                {
                    Id = AddToCartStepGuid,
                    StepName = "Voeg toe aan mandje",
                    ProductEntityId = ProductConfiguration.WheyProteinGuid,
                    StepInJs =
                        "await page.getByText('Voeg toe').first().click();"
                },
            ]
        );
    }
}