using GNB.IBM.Core.Entities.Base;
using GNB.IBM.Core.Interfaces;
using GNB.IBM.Core.Repositories.Base;
using GNB.IBM.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GNB.IBM.Infrastructure.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly DatabaseContext _dbContext;
        private readonly IHttpHandler<T> _httpHandler;

        public Repository(DatabaseContext dbContext, IHttpHandler<T> httpHandler)
        {
            _dbContext = dbContext;
            _httpHandler = httpHandler;
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().AddRange(entities);
        }

        public void Clear()
        {
            _dbContext.Set<T>().RemoveRange(_dbContext.Set<T>());
        }

        private async void UpdateValuesStored(List<T>? list)
        {
            if (list is null) return;

            Clear();
            AddRange(list);
            await _dbContext.SaveChangesAsync();
        }

        protected async Task<IEnumerable<T>> GetAllAsync(string url)
        {
            List<T>? list;

            try
            {
                list = await _httpHandler.GetAsync(url);
                UpdateValuesStored(list);
            }
            catch (Exception)
            {
                list = await _dbContext.Set<T>().ToListAsync();
            }

            return list ?? new List<T>();
        }
    }
}
