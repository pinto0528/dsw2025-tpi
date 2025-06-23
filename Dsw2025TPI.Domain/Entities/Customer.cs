using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025TPI.Domain.Entities
{
    public class Customer : EntityBase
    {
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;

        public List<Order> Orders { get; set; } = new();
    }
}
