using Catalog.Host.Data;
using Catalog.Host.Data.Enums;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogTypeRepository
{
    Task<PaginatedItems<CatalogType>> GetByPageAsync(int pageIndex, int pageSize);
}
