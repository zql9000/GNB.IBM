using GNB.IBM.Application.Models;

namespace GNB.IBM.Application.Interfaces
{
    public interface IProductTransactionService
    {
        Task<IEnumerable<ProductTransactionModel>> GetProductTransactionListAsync();
        Task<IEnumerable<ProductTransactionModel>> GetProductTransactionListBySkuAsync(string sku);
    }
}
