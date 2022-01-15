using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Data.Enums;
using Catalog.Host.Repositories.Interfaces;

namespace Catalog.Host.Repositories
{
    public class CatalogBrandRepository : ICatalogBrandRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CatalogBrandRepository(IDbContextWrapper<ApplicationDbContext> dbContext)
        {
            this._dbContext = dbContext.DbContext;
        }

        public async Task<PaginatedItems<CatalogBrand>> GetByPageAsync(int pageIndex, int pageSize)
        {
            var totalItems = await _dbContext.CatalogBrands
                .LongCountAsync();

            var itemsOnPage = await _dbContext.CatalogBrands
                .OrderBy(c => c.Brand)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedItems<CatalogBrand>() { TotalCount = totalItems, Data = itemsOnPage };
        }
    }
}
