using Catalog.Host.Data;
using Catalog.Host.Data.Enums;
using Catalog.Host.Repositories.Interfaces;

namespace Catalog.Host.Repositories
{
    public class CatalogTypeRepository : ICatalogTypeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CatalogTypeRepository(IDbContextWrapper<ApplicationDbContext> dbContext)
        {
            this._dbContext = dbContext.DbContext;
        }

        public async Task<PaginatedItems<CatalogType>> GetByPageAsync(int pageIndex, int pageSize)
        {
            var totalItems = await _dbContext.CatalogTypes
                .LongCountAsync();

            var itemsOnPage = await _dbContext.CatalogTypes
                .OrderBy(c => c.Type)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedItems<CatalogType>() { TotalCount = totalItems, Data = itemsOnPage };
        }
    }
}
