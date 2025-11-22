using Data.Contracts;

namespace AutoBuy.ProductConfiguration;

public static class ProductConfigurationMapper
{
    public static ProductConfigurationEntity ToEntity(this ConfiguredProductDto product, string userId)
    {
        var entity = new ProductConfigurationEntity
        {
            UserId = userId,
            ProductId = product.Id,
            RepeatEveryAmount = product.RepeatEveryAmount,
            RepeatEveryTimePeriod = (Data.Contracts.TimePeriod) product.RepeatEveryTimePeriod,
            StartDate = product.StartDate,
        };
        entity.SelectedOptions = product.Options.Select(o => o.ToEntity(entity)).ToList();
        return entity;
    }
    
    public static SelectedOptionEntity ToEntity(this ConfiguredProductOptionsDto option, ProductConfigurationEntity productConfiguration)
    {
        return new SelectedOptionEntity
        {
            Value = option.Value,
            ProductConfiguration = productConfiguration,
            ProductConfigurationId = productConfiguration.Id,
            OptionId = option.Id,
        };
    }
    
    public static ProductConfigurationDto ToDto(this ProductConfigurationEntity entity)
    {
        return new ProductConfigurationDto
        {
            Id = entity.Id,
            Product = entity.Product.ToDto(),
            SelectedOptions = entity.SelectedOptions.Select(o => o.ToDto(entity)).ToArray(),
            RepeatEveryAmount = entity.RepeatEveryAmount,
            RepeatEveryTimePeriod = (TimePeriod) entity.RepeatEveryTimePeriod,
            StartDate = entity.StartDate,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
    
    public static ProductDto ToDto(this ProductEntity entity)
    {
        return new ProductDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Url = entity.Url.ToString(),
            Description = entity.Description,
            Price = entity.Price,
            ImageUrl = entity.ImageUrl.ToString()
        };
    }
    
    public static SelectedOptionDto ToDto(this SelectedOptionEntity entity, ProductConfigurationEntity  productConfiguration)
    {
        return new SelectedOptionDto
        {
            Id = entity.Id,
            ProductKey = entity.Option.Name,
            Value = entity.Value
        };
    }
}