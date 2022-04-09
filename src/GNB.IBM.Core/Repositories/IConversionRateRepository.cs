using GNB.IBM.Core.Entities;
using GNB.IBM.Core.Repositories.Base;

namespace GNB.IBM.Core.Repositories
{
    public interface IConversionRateRepository : IRepository<ConversionRate>
    {
        Task<IReadOnlyList<ConversionRate>> GetAllAsync();
    }
}
