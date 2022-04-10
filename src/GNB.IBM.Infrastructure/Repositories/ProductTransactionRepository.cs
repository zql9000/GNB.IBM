using GNB.IBM.Core.Configuration;
using GNB.IBM.Core.Entities;
using GNB.IBM.Core.Interfaces;
using GNB.IBM.Core.Repositories;
using GNB.IBM.Infrastructure.Repositories.Base;
using Microsoft.Extensions.Options;

namespace GNB.IBM.Infrastructure.Repositories
{
    public class ProductTransactionRepository : Repository<ProductTransaction>, IProductTransactionRepository
    {
        protected readonly IHttpHandler<ProductTransaction> _httpHandler;
        protected readonly ExternalServicesSettings _settings;

        public ProductTransactionRepository(IHttpHandler<ProductTransaction> httpHandler, IOptionsSnapshot<ExternalServicesSettings> settings)
        {
            _httpHandler = httpHandler;
            _settings = settings.Value;
        }

        public async Task<IReadOnlyList<ProductTransaction>> GetAllAsync()
        {
            string productTransactionsURI = _settings.ProductTransactionsURI;
            List<ProductTransaction>? objects = null;

            try
            {
                objects = await _httpHandler.GetAsync(productTransactionsURI);
            }
            catch (Exception)
            {
                // Get data from database
                objects = new List<ProductTransaction>();
            }

            if (objects is null)
            {
                // Get data from database
                objects = new List<ProductTransaction>();
            }

            return objects;
        }

        public async Task<IEnumerable<ProductTransaction>> GetProductTransactionListBySkuAsync(string sku)
        {
            string productTransactionsURI = _settings.ProductTransactionsURI;
            List<ProductTransaction>? objects = null;

            try
            {
                objects = await _httpHandler.GetAsync(productTransactionsURI)
                    ?? new List<ProductTransaction>();
                objects = objects.Where(t => t.SKU == sku).ToList();
            }
            catch (Exception)
            {
                // Get data from database
                objects = new List<ProductTransaction>();
            }

            if (objects is null)
            {
                // Get data from database
                objects = new List<ProductTransaction>();
            }

            return objects;
        }
    }
}
