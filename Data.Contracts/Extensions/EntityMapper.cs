using Domain;

namespace Data.Contracts.Extensions;

public static class EntityMapper
{
    // Brand mappings
    public static BrandEntity ToEntity(this Brand brand)
    {
        return new BrandEntity
        {
            Name = brand.Name,
            BaseUrl = brand.BaseUrl,
            LogoUrl = brand.LogoUrl,
            MinimumFreeDeliveryPrice = brand.MinimumFreeDeliveryPrice,
            Products = brand.Products.Select(p => p.ToEntity()).ToList()
        };
    }

    public static Brand ToDomain(this BrandEntity entity)
    {
        return new Brand
        {
            Name = entity.Name,
            BaseUrl = entity.BaseUrl,
            LogoUrl = entity.LogoUrl,
            MinimumFreeDeliveryPrice = entity.MinimumFreeDeliveryPrice,
            Products = entity.Products?.Select(p => p.ToDomain()).ToList() ?? []
        };
    }

    // Product mappings
    public static ProductEntity ToEntity(this Product product)
    {
        return new ProductEntity
        {
            Name = product.Name,
            Url = product.Url,
            Description = product.Description,
            Price = product.Price,
            Options = product.Options.Select(o => o.ToEntity()).ToList(),
            OrderSteps = product.OrderSteps.Select(os => os.ToEntity()).ToList(),
            BrandEntityId = Guid.Empty // This should be set by the caller
        };
    }

    public static Product ToDomain(this ProductEntity entity)
    {
        return new Product
        {
            Name = entity.Name,
            Url = entity.Url,
            Description = entity.Description,
            Price = entity.Price,
            Options = entity.Options?.Select(o => o.ToDomain()).ToList() ?? [],
            OrderSteps = entity.OrderSteps?.Select(os => os.ToDomain()).ToList() ?? []
        };
    }

    // Option mappings
    public static OptionEntity ToEntity(this Option option)
    {
        return new OptionEntity
        {
            Name = option.Name,
            Values = option.Values,
            ProductEntityId = Guid.Empty // This should be set by the caller
        };
    }

    public static Option ToDomain(this OptionEntity entity)
    {
        return new Option
        {
            Name = entity.Name,
            Values = entity.Values
        };
    }

    // OrderStep mappings
    public static OrderStepEntity ToEntity(this OrderStep orderStep)
    {
        return new OrderStepEntity
        {
            StepName = orderStep.StepName,
            StepInJs = orderStep.StepInJs,
            ProductEntityId = Guid.Empty // This should be set by the caller
        };
    }

    public static OrderStep ToDomain(this OrderStepEntity entity)
    {
        return new OrderStep
        {
            StepName = entity.StepName,
            StepInJs = entity.StepInJs
        };
    }
}