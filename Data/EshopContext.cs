using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ncorep.Models;

namespace ncorep.Data;

public class EshopContext : DbContext
{
    public EshopContext(DbContextOptions<EshopContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }

    public DbSet<AppUser> AppUsers { get; set; }

    public DbSet<OrderDetail> OrderDetails { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<ShoppingCartRecord> ShoppingCartRecords { get; set; }

    public DbSet<Address> Addresses { get; set; }

    public DbSet<Image> Images { get; set; }

    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Fluent API settings


        builder.Entity<Product>(entity =>
        {
            entity.Property(e => e.UnitCost).HasColumnType("money");
            entity.Property(e => e.CurrentPrice).HasColumnType("money");
        });

        builder.Entity<Category>().HasOne(x => x.Parent).WithMany(z => z.Childrens)
            .HasForeignKey(u => u.ParentId);

        builder.Entity<ProductCategory>().HasOne(ur => ur.Category).WithMany(p => p.ProductCategories)
            .HasForeignKey(pt => pt.CategoryId);

        builder.Entity<ProductCategory>().HasOne(ur => ur.Product).WithMany(p => p.ProductCategories)
            .HasForeignKey(pt => pt.ProductId);
    }

    public override int SaveChanges()
    {
        OnBeforeSaving();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        OnBeforeSaving();
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void OnBeforeSaving()
    {
        var entities = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity &&
                        (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entityEntry in entities)
        {
            ((BaseEntity) entityEntry.Entity).ModifiedOn = DateTime.Now;

            if (entityEntry.State == EntityState.Added) ((BaseEntity) entityEntry.Entity).CreatedOn = DateTime.Now;
        }
    }
}