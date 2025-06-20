using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025TPI.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Sku { get; set; } = null!;
        public string InternalCode { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal CurrentUnitPrice { get; set; }
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; } = true;

        public List<OrderItem> OrderItems { get; set; } = new();
    }
}
