using Microsoft.EntityFrameworkCore;

namespace Data.Contracts;

public interface IAutoBuyDbContext
{
    
    public DbSet<OrderStepEntity> OrderSteps { get; set; }
    
    public DbSet<BrandEntity> Brands { get; set; }
    
    public DbSet<ProductEntity> Products { get; set; }
    
    public DbSet<OptionEntity> Options { get; set; }
    
    public DbSet<ProductConfigurationEntity> ProductConfigurations { get; set; }
    
    public DbSet<SelectedOptionEntity> SelectedOptions { get; set; }
    
    public Task<int> SaveChangesAsync(CancellationToken ct);
}