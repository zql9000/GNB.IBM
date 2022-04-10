using AutoMapper;
using GNB.IBM.Application.Interfaces;
using GNB.IBM.Application.Models;
using GNB.IBM.Core.Repositories;

namespace GNB.IBM.Application.Services
{
    public class ConversionRateService : IConversionRateService
    {
        private readonly IConversionRateRepository _conversionRateRepository;
        private readonly IMapper _mapper;

        public ConversionRateService(IConversionRateRepository conversionRateRepository, IMapper mapper)
        {
            _conversionRateRepository = conversionRateRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ConversionRateModel>> GetConversionRateListAsync()
        {
            var conversionRates = await _conversionRateRepository.GetConversionRateListAsync();
            var mapped = _mapper.Map<IEnumerable<ConversionRateModel>>(conversionRates);
            return mapped;
        }
    }
}
