using GNB.IBM.Core.Configuration;
using GNB.IBM.Core.Entities;
using GNB.IBM.Core.Interfaces;
using GNB.IBM.Core.Repositories;
using GNB.IBM.Infrastructure.Repositories.Base;
using Microsoft.Extensions.Options;

namespace GNB.IBM.Infrastructure.Repositories
{
    public class ConversionRateRepository : Repository<ConversionRate>, IConversionRateRepository
    {
        protected readonly IHttpHandler<ConversionRate> _httpHandler;
        protected readonly ExternalServicesSettings _settings;

        public ConversionRateRepository(IHttpHandler<ConversionRate> httpHandler, IOptionsSnapshot<ExternalServicesSettings> settings)
        {
            _httpHandler = httpHandler;
            _settings = settings.Value;
        }

        public async Task<IEnumerable<ConversionRate>> GetConversionRateListAsync()
        {
            string conversionRatesURI = _settings.ConversionRatesURI;
            List<ConversionRate>? conversionRates = null;

            try
            {
                conversionRates = await _httpHandler.GetAsync(conversionRatesURI);
            }
            catch (Exception)
            {
                // Get data from database
                conversionRates = new List<ConversionRate>();
            }
            
            if (conversionRates is null)
            {
                // Get data from database
                conversionRates = new List<ConversionRate>();
            }

            return conversionRates;
        }
    }
}
