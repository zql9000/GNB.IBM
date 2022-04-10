using AutoMapper;
using GNB.IBM.Application.Interfaces;
using GNB.IBM.WebAPI.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GNB.IBM.WebAPI.Controllers
{
    [Route("conversion-rates")]
    [ApiController]
    public class ConversionRatesController : ControllerBase
    {
        private readonly IConversionRateService _conversionRateService;
        private readonly IMapper _mapper;

        public ConversionRatesController(IConversionRateService conversionRateService, IMapper mapper)
        {
            _conversionRateService = conversionRateService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ConversionRateDto>> Get()
        {
            var list = await _conversionRateService.GetConversionRateListAsync();
            var mapped = _mapper.Map<IEnumerable<ConversionRateDto>>(list);
            return mapped;
        }
    }
}
