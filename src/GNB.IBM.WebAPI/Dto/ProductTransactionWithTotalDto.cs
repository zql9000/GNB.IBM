namespace GNB.IBM.WebAPI.Dto
{
    public class ProductTransactionWithTotalDto
    {
        public decimal Total { get; set; }
        public IEnumerable<ProductTransactionDto> ProductTransactions { get; set; } = new List<ProductTransactionDto>();
    }
}
