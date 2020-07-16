using Pulsar.CoreElements.Api.Data.BaseModels;

namespace Pulsar.CoreElements.Api.Models.CoreElements
{
    public class CoreElement : BaseDbEntity
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string MineState { get; set; }
        public string StoreState { get; set; }
        public bool Hazard { get; set; }
        public string Signature { get; set; }
        public decimal Weight { get; set; }

    }
}