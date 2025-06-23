using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025TPI.Application.DTOs
{
    public class OrderRequest
    {
        public Guid CustomerId { get; set; }
        public string ShippingAddress { get; set; } = null!;
        public string BillingAddress { get; set; } = null!;
        public List<OrderItemRequest> Items { get; set; } = new();
    }
}

