using GNB.IBM.Core.Configuration;
using GNB.IBM.Core.Entities;
using GNB.IBM.Core.Interfaces;
using GNB.IBM.Core.Repositories;
using GNB.IBM.Infrastructure.Data;
using GNB.IBM.Infrastructure.Repositories.Base;
using Microsoft.Extensions.Options;

namespace GNB.IBM.Infrastructure.Repositories
{
    public class ConversionRateRepository : Repository<ConversionRate>, IConversionRateRepository
    {
        protected readonly ExternalServicesSettings _settings;

        public ConversionRateRepository(
            IHttpHandler<ConversionRate> httpHandler, 
            IOptionsSnapshot<ExternalServicesSettings> settings,
            DatabaseContext context) : base(context, httpHandler)
        {
            _settings = settings.Value;
        }

        public async Task<IEnumerable<ConversionRate>> GetConversionRateListAsync()
        {
            string conversionRatesURI = _settings.ConversionRatesURI;
            return await GetAllAsync(conversionRatesURI);
        }
    }
}
