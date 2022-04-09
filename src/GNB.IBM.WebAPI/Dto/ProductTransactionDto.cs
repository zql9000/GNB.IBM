namespace GNB.IBM.WebAPI.Dto
{
    public class ProductTransactionDto
    {
        public string SKU { get; set; } = string.Empty;
        public float Amount { get; set; }
        public string Currency { get; set; } = string.Empty;
    }
}
