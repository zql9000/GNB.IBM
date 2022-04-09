using GNB.IBM.Core.Entities.Base;

namespace GNB.IBM.Core.Entities
{
    public class ConversionRate : Entity
    {
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public float Rate { get; set; }
    }
}
