using System;
using System.ComponentModel.DataAnnotations;

namespace Pulsar.Customers.Api.Data.BaseModels
{
    public class BaseDbEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}