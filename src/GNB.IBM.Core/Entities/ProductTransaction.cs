using GNB.IBM.Core.Entities.Base;

namespace GNB.IBM.Core.Entities
{
    public class ProductTransaction : Entity
    {
        public string SKU { get; set; } = string.Empty;
        public float Amount { get; set; }
        public string Currency { get; set; } = string.Empty;
    }
}
