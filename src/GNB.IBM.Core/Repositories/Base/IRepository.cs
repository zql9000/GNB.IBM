using GNB.IBM.Core.Entities.Base;

namespace GNB.IBM.Core.Repositories.Base
{
    public interface IRepository<T> where T : Entity
    {
        void AddRange(IEnumerable<T> entities);
        public void Clear();
    }
}
