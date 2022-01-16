using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;

namespace Catalog.Host.Services.Interfaces
{
    public class CatalogBrandService : BaseDataService<ApplicationDbContext>,
        ICatalogBrandService
    {
        private readonly ICatalogBrandRepository _catalogBrandRepository;
        public CatalogBrandService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogBrandRepository catalogBrandRepository)
        : base(dbContextWrapper, logger)
        {
            _catalogBrandRepository = catalogBrandRepository;
        }

        public Task<int?> AddAsync(string brand)
        {
            return ExecuteSafe(() => _catalogBrandRepository.Add(brand));
        }

        public Task<int?> UpdateAsync(int id, string name)
        {
            return ExecuteSafe(() => _catalogBrandRepository.Update(id, name));
        }

        public Task<int?> RemoveAsync(int id)
        {
            return ExecuteSafe(() => _catalogBrandRepository.Remove(id));
        }
    }
}
