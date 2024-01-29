using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _ctx;

        public GenericRepository(StoreContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
            => await _ctx.Set<T>().ToListAsync();
    }
}
