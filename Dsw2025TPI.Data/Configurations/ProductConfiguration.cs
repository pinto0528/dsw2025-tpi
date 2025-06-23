using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dsw2025TPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dsw2025TPI.Data.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
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
        }
    }
}
