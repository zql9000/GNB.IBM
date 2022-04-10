using GNB.IBM.Application.Models;

namespace GNB.IBM.Application.Interfaces
{
    public interface IConversionRateService
    {
        Task<IEnumerable<ConversionRateModel>> GetConversionRateListAsync();
    }
}
