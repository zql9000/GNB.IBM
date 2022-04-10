using GNB.IBM.Core.Configuration;
using GNB.IBM.Core.Entities;
using GNB.IBM.Core.Interfaces;
using GNB.IBM.Core.Repositories;
using GNB.IBM.Infrastructure.Data;
using GNB.IBM.Infrastructure.Repositories.Base;
using Microsoft.Extensions.Options;

namespace GNB.IBM.Infrastructure.Repositories
{
    public class ProductTransactionRepository : Repository<ProductTransaction>, IProductTransactionRepository
    {
        protected readonly ExternalServicesSettings _settings;

        public ProductTransactionRepository(
            IHttpHandler<ProductTransaction> httpHandler, 
            IOptionsSnapshot<ExternalServicesSettings> settings,
            DatabaseContext context) : base(context, httpHandler)
        {
            _settings = settings.Value;
        }

        public async Task<IEnumerable<ProductTransaction>> GetProductTransactionListAsync()
        {
            string productTransactionsURI = _settings.ProductTransactionsURI;
            return await GetAllAsync(productTransactionsURI);
        }

        public async Task<IEnumerable<ProductTransaction>> GetProductTransactionListBySkuAsync(string sku)
        {
            string productTransactionsURI = _settings.ProductTransactionsURI;
            var productTransactions = await GetAllAsync(productTransactionsURI);

            return productTransactions.Where(t => t.SKU == sku).ToList();
        }
    }
}
