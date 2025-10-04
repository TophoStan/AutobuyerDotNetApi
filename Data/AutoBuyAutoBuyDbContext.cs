using Data.Contracts;
using Environment;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class AutoBuyAutoBuyDbContext : DbContext, IAutoBuyDbContext
{
    public DbSet<OrderStepEntity> OrderSteps { get; set; }
    public DbSet<BrandEntity> Brands { get; set; }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<OptionEntity> Options { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;
        Console.WriteLine(EnvironmentExtensions.GetPostgresConnectionString());
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

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AutoBuyAutoBuyDbContext).Assembly);


        base.OnModelCreating(modelBuilder);
    }
}