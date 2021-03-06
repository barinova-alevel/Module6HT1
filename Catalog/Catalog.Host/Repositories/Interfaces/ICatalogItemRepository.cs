using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogItemRepository
{
    Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<int?> Update(int id, string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName);
    Task<int?> Remove(int id);
    Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize);
    Task<CatalogItem?> GetById(int id);
    Task<PaginatedItems<CatalogItem>> GetByBrandAsync(int brandId, int pageIndex, int pageSize);
    Task<PaginatedItems<CatalogItem>> GetByTypeAsync(int typeId, int pageIndex, int pageSize);
}