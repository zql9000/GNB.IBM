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

        public ConversionRateService(IConversionRateRepository ConversionRateRepository, IMapper mapper)
        {
            _conversionRateRepository = ConversionRateRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ConversionRateModel>> GetConversionRateList()
        {
            var conversionRate = await _conversionRateRepository.GetAllAsync();
            var mapped = _mapper.Map<IEnumerable<ConversionRateModel>>(conversionRate);
            return mapped;
        }
    }
}
