using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025TPI.Domain.Entities
{
    public class OrderItem : EntityBase
    {
        public OrderItem(Guid ProductId, int quantity, decimal unitPrice)
        {
            this.ProductId = ProductId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public Guid OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public decimal Subtotal => Quantity * UnitPrice;
    }
}
