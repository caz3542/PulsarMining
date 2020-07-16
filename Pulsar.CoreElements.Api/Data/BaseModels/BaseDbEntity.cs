using System;
using System.ComponentModel.DataAnnotations;

namespace Pulsar.CoreElements.Api.Data.BaseModels
{
    public class BaseDbEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}