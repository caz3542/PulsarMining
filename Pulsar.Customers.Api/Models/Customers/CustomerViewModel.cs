using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pulsar.Customers.Api.Models.Customers
{
    public class CustomerViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public string Email { get; set; }
    }
}
