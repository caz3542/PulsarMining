using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pulsar.CoreElements.Api.Models.CoreElements
{
    public class CoreElementViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string MineState { get; set; }
        public string StoreState { get; set; }
        public bool Hazard { get; set; }
        public string Signature { get; set; }
        public decimal Weight { get; set; }
    }
}
