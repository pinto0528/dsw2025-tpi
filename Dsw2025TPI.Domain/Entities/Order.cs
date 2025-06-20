using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dsw2025TPI.Domain.Enums;

namespace Dsw2025TPI.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }
        public string ShippingAddress { get; set; } = null!;
        public string BillingAddress { get; set; } = null!;
        public string? Notes { get; set; }

        public decimal TotalAmount { get; set; }

        public OrderStatus Status { get; set; }

        public Guid CustomerId { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new();
    }
}
