using AutoMapper;
using GNB.IBM.Application.Interfaces;
using GNB.IBM.Application.Models;
using GNB.IBM.Application.Support;
using GNB.IBM.Core.Repositories;

namespace GNB.IBM.Application.Services
{
    public class ProductTransactionService : IProductTransactionService
    {
        private readonly IProductTransactionRepository _productTransactionRepository;
        private readonly IMapper _mapper;
        private readonly IConversionRateRepository _conversionRateRepository;
        private const string _euroCurrency = "EUR";

        public ProductTransactionService(
                IProductTransactionRepository productTransactionRepository,
                IMapper mapper,
                IConversionRateRepository conversionRateRepository
            )
        {
            _productTransactionRepository = productTransactionRepository;
            _mapper = mapper;
            _conversionRateRepository = conversionRateRepository;
        }

        public async Task<IEnumerable<ProductTransactionModel>> GetProductTransactionListAsync()
        {
            var productTransactions = await _productTransactionRepository.GetProductTransactionListAsync();
            var mapped = _mapper.Map<IEnumerable<ProductTransactionModel>>(productTransactions);
            return mapped;
        }

        public async Task<IEnumerable<ProductTransactionModel>> GetProductTransactionListBySkuAsync(string sku)
        {
            var productTransactions = await _productTransactionRepository.GetProductTransactionListBySkuAsync(sku);

            var conversionRates = await _conversionRateRepository.GetConversionRateListAsync();
            var currencyConversion = new CurrencyConversion(conversionRates);
            
            productTransactions = productTransactions.Select(productTransaction =>
            {
                try
                {
                    productTransaction.Amount = currencyConversion.Convert(productTransaction.Currency, _euroCurrency, productTransaction.Amount);
                    productTransaction.Currency = _euroCurrency;
                }
                catch (Exception ex)
                {
                    productTransaction.Currency = ex.Message;
                }
                return productTransaction;
            });
            
            var mapped = _mapper.Map<IEnumerable<ProductTransactionModel>>(productTransactions);
            return mapped;
        }
    }
}
