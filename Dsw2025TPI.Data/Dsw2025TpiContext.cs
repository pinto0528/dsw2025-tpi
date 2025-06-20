using Dsw2025TPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dsw2025TPI.Data;

public class Dsw2025TpiContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();

    public Dsw2025TpiContext(DbContextOptions<Dsw2025TpiContext> options)
                : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Sku)
                .IsRequired();

            entity.Property(p => p.InternalCode)
                .IsRequired();

            entity.Property(p => p.Name)
                .IsRequired();

            entity.Property(p => p.CurrentUnitPrice)
                .HasPrecision(18, 2)
                .IsRequired();

            entity.Property(p => p.StockQuantity)
                .IsRequired();
        });

        base.OnModelCreating(modelBuilder);
    }
}
