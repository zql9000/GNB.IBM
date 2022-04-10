using AutoMapper;
using GNB.IBM.Application.Interfaces;
using GNB.IBM.Application.Models;
using GNB.IBM.Core.Repositories;

namespace GNB.IBM.Application.Services
{
    public class ProductTransactionService : IProductTransactionService
    {
        private readonly IProductTransactionRepository _productTransactionRepository;
        private readonly IMapper _mapper;

        public ProductTransactionService(IProductTransactionRepository ProductTransactionRepository, IMapper mapper)
        {
            _productTransactionRepository = ProductTransactionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductTransactionModel>> GetProductTransactionListAsync()
        {
            var productTransaction = await _productTransactionRepository.GetProductTransactionListAsync();
            var mapped = _mapper.Map<IEnumerable<ProductTransactionModel>>(productTransaction);
            return mapped;
        }

        public async Task<IEnumerable<ProductTransactionModel>> GetProductTransactionListBySkuAsync(string sku)
        {
            var productTransaction = await _productTransactionRepository.GetProductTransactionListBySkuAsync(sku);
            var mapped = _mapper.Map<IEnumerable<ProductTransactionModel>>(productTransaction);
            return mapped;
        }
    }
}
