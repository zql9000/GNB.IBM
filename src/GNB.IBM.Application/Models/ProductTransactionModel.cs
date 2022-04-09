namespace GNB.IBM.Application.Models
{
    public class ProductTransactionModel
    {
        public string SKU { get; set; } = string.Empty;
        public float Amount { get; set; }
        public string Currency { get; set; } = string.Empty;
    }
}
