using AutoMapper;
using GNB.IBM.Application.Interfaces;
using GNB.IBM.WebAPI.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GNB.IBM.WebAPI.Controllers
{
    [Route("product-transactions")]
    [ApiController]
    public class ProductTransactionsController : ControllerBase
    {
        private readonly IProductTransactionService _productTransactionService;
        private readonly IMapper _mapper;

        public ProductTransactionsController(IProductTransactionService productTransactionService, IMapper mapper)
        {
            _productTransactionService = productTransactionService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductTransactionDto>> Get()
        {
            var list = await _productTransactionService.GetProductTransactionList();
            var mapped = _mapper.Map<IEnumerable<ProductTransactionDto>>(list);
            return mapped;
        }

        [Route("products/{sku}")]
        [HttpGet]
        public async Task<IEnumerable<ProductTransactionDto>> Get(string sku)
        {
            var list = await _productTransactionService.GetProductTransactionListBySkuAsync(sku);
            var mapped = _mapper.Map<IEnumerable<ProductTransactionDto>>(list);
            return mapped;
        }
    }
}
