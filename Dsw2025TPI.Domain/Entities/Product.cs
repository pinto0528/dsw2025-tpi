using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025TPI.Domain.Entities
{
    public class Product : EntityBase
    {
        public Product() { }
        public Product(Guid guid, string sku, string internalCode, string name, string? description, decimal currentUnitPrice, int stockQuantity)
        {
            Sku = sku;
            InternalCode = internalCode;
            Name = name;
            Description = description;
            CurrentUnitPrice = currentUnitPrice;
            StockQuantity = stockQuantity;
        }

        public string Sku { get; set; } = null!;
        public string InternalCode { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal CurrentUnitPrice { get; set; }
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; } = true;

        public void Deactivate()
        {
            IsActive = false;
        }

        public void DecreaseStock(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("La cantidad a descontar debe ser mayor a cero.");

            if (StockQuantity < quantity)
                throw new InvalidOperationException($"Stock insuficiente. Disponible: {StockQuantity}, solicitado: {quantity}.");

            StockQuantity -= quantity;
        }


        public List<OrderItem> OrderItems { get; set; } = new();
    }
}
