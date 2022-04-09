using GNB.IBM.Core.Entities.Base;
using GNB.IBM.Core.Repositories.Base;

namespace GNB.IBM.Infrastructure.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
    }
}
