using Dsw2025TPI.Data.Configurations;
using Dsw2025TPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dsw2025TPI.Data;

public class Dsw2025TpiContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Customer> Customers => Set<Customer>();


    public Dsw2025TpiContext(DbContextOptions<Dsw2025TpiContext> options)
                : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new OrderItemConfiguration());


        base.OnModelCreating(modelBuilder);
    }
}
