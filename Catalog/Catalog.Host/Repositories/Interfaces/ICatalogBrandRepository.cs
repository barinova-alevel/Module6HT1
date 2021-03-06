using Catalog.Host.Data;
using Catalog.Host.Data.Enums;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogBrandRepository
{
    Task<PaginatedItems<CatalogBrand>> GetByPageAsync(int pageIndex, int pageSize);
    Task<int?> Add(string brand);

    Task<int?> Update(int id, string name);

    Task<int?> Remove(int id);
}