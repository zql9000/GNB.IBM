using GNB.IBM.Core.Entities;
using GNB.IBM.Core.Repositories.Base;

namespace GNB.IBM.Core.Repositories
{
    public interface IProductTransactionRepository : IRepository<ProductTransaction>
    {
        Task<IEnumerable<ProductTransaction>> GetProductTransactionListAsync();
        Task<IEnumerable<ProductTransaction>> GetProductTransactionListBySkuAsync(string sku);
    }
}
