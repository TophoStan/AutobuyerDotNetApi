using Data.Contracts;
using Environment;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class AutoBuyDbContext : DbContext, IAutoBuyDbContext
{
    public DbSet<OrderStepEntity> OrderSteps { get; set; }
    public DbSet<BrandEntity> Brands { get; set; }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<OptionEntity> Options { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;
        optionsBuilder.UseNpgsql(EnvironmentExtensions.GetPostgresConnectionString(), npgsqlOptions =>
        {
            npgsqlOptions.MigrationsAssembly("Data");
            npgsqlOptions.CommandTimeout(30);
        });
        optionsBuilder.EnableSensitiveDataLogging();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure all entities that inherit from BaseEntity to use PostgreSQL UUID generation
        foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                     .Where(e => typeof(BaseEntity).IsAssignableFrom(e.ClrType)))
        {
            modelBuilder.Entity(entityType.ClrType)
                .Property(nameof(BaseEntity.Id))
                .HasDefaultValueSql("gen_random_uuid()");
        }

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AutoBuyDbContext).Assembly);


        base.OnModelCreating(modelBuilder);
    }
}