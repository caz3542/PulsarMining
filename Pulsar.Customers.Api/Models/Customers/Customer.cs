using Pulsar.Customers.Api.Data.BaseModels;

namespace Pulsar.Customers.Api.Models.Customers
{
    public class Customer : BaseDbEntity
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public string Email { get; set; }
    }
}