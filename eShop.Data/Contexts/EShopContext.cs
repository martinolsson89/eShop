using eShop.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace eShop.Data.Contexts;

public class EShopContext(DbContextOptions<EShopContext> builder) : DbContext(builder)
{

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Filter> Filters => Set<Filter>();
    public DbSet<Color> Colors => Set<Color>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Brand> Brands => Set<Brand>();
    public DbSet<Fuel> Fuels => Set<Fuel>();
    public DbSet<CategoryFilter> CategoryFilters => Set<CategoryFilter>();
    public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();
    public DbSet<ProductColor> ProductColors => Set<ProductColor>();
    public DbSet<ProductFuel> ProductFuels => Set<ProductFuel>();
    // public DbSet<FilterType> FilterTypes => Set<FilterType>();
    // public DbSet<Option> Options => Set<Option>();


    

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        #region Composite Keys
        builder.Entity<ProductColor>()
            .HasKey(pc => new { pc.ProductId, pc.ColorId });
        builder.Entity<ProductFuel>()
            .HasKey(pf => new { pf.ProductId, pf.FuelId });
        builder.Entity<ProductCategory>()
            .HasKey(pc => new { pc.ProductId, pc.CategoryId });
        builder.Entity<CategoryFilter>()
            .HasKey(cf => new { cf.CategoryId, cf.FilterId });
        #endregion

        #region CategoryFilter Many-to-Many Relationship

        builder.Entity<Category>()
            .HasMany(c => c.Filters)
            .WithMany(f => f.Categories)
            .UsingEntity<CategoryFilter>();
        #endregion

        #region ProductCategory Many-to-Many Relationship
        builder.Entity<Product>()
            .HasMany(p => p.Categories)
            .WithMany(c => c.Products)
            .UsingEntity<ProductCategory>();
        #endregion

        #region ProductColor Many-to-Many Relationship
        builder.Entity<Product>()
            .HasMany(p => p.Colors)
            .WithMany(c => c.Products)
            .UsingEntity<ProductColor>();
        #endregion

        #region CarFuel Many-to-Many Relationship

        builder.Entity<Product>()
            .HasMany(p => p.Fuels)
            .WithMany(f => f.Products)
            .UsingEntity<ProductFuel>();

        #endregion

        #region CarBrand One-to-Many Relationship
        builder.Entity<Product>()
            .HasOne(b => b.Brand)
            .WithMany(c => c.Products)
            .HasForeignKey(b => b.BrandId);
        #endregion
        
       //  #region OptionFilter One-to-Many Relationship
       // builder.Entity<Filter>()
       //     .HasMany(c => c.Options)
       //     .WithOne(f => f.Filter);
       // #endregion
       //
       // #region FilterType One-to-Many Relationship
       // builder.Entity<FilterType>()
       //     .HasMany(ft => ft.Filters)
       //     .WithOne(f => f.FilterType);
       // #endregion
    }

       


}