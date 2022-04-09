using GNB.IBM.Application.Models;

namespace GNB.IBM.Application.Interfaces
{
    public interface IProductTransactionService
    {
        Task<IEnumerable<ProductTransactionModel>> GetProductTransactionList();
    }
}
