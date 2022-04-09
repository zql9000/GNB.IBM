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

        public async Task<IReadOnlyList<ConversionRate>> GetAllAsync()
        {
            string conversionRatesURI = _settings.ConversionRatesURI;
            List<ConversionRate>? objects = null;

            try
            {
                objects = await _httpHandler.GetAsync(conversionRatesURI);
            }
            catch (Exception)
            {
                // Get data from database
                objects = new List<ConversionRate>();
            }
            
            if (objects is null)
            {
                // Get data from database
                objects = new List<ConversionRate>();
            }

            return objects;
        }
    }
}
