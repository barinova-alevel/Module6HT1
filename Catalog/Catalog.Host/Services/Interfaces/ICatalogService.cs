using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogService
{
    Task<PaginatedItemsResponse<CatalogItemDto>?> GetCatalogItemByBrandAsync(int brandId, int pageSize, int pageIndex);
    Task<PaginatedItemsResponse<CatalogItemDto>?> GetCatalogItemByTypeAsync(int typeId, int pageSize, int pageIndex);
    Task<PaginatedItemsResponse<CatalogItemDto>?> GetCatalogItemsAsync(int pageSize, int pageIndex);
    Task<CatalogItemDto?> GetCatalogItemByIdAsync(int id);

    Task<PaginatedItemsResponse<CatalogBrandDto>?> GetCatalogBrandsAsync(int pageSize, int pageIndex);
    Task<PaginatedItemsResponse<CatalogTypeDto>?> GetCatalogTypesAsync(int pageSize, int pageIndex);
}