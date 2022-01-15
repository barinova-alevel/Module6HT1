using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using System.Linq.Expressions;

namespace Catalog.Host.Services;

public class CatalogService : BaseDataService<ApplicationDbContext>, ICatalogService
{
    private readonly ICatalogItemRepository _catalogItemRepository;
    private readonly ICatalogBrandRepository _catalogBrandRepository;
    private readonly ICatalogTypeRepository _catalogTypeRepository;
    private readonly IMapper _mapper;

    public CatalogService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogItemRepository catalogItemRepository,
        ICatalogBrandRepository catalogBrandRepository,
        ICatalogTypeRepository catalogTypeRepository,
        IMapper mapper)
        : base(dbContextWrapper, logger)
    {
        _catalogItemRepository = catalogItemRepository;
        _catalogBrandRepository = catalogBrandRepository;
        _catalogTypeRepository = catalogTypeRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedItemsResponse<CatalogItemDto>?> GetCatalogItemsAsync(int pageSize, int pageIndex)
    {
        return await ExecuteSafe(async () =>
        {
            var result = await _catalogItemRepository.GetByPageAsync(pageIndex, pageSize);
            if (result == null)
            {
                return null;
            }

            return new PaginatedItemsResponse<CatalogItemDto>()
            {
                Count = result.TotalCount,
                Data = result.Data.Select(s => _mapper.Map<CatalogItemDto>(s)).ToList(),
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        });
    }

    public async Task<PaginatedItemsResponse<CatalogItemDto>?> GetCatalogItemByBrandAsync(int brandId, int pageSize, int pageIndex)
    {
        return await ExecuteSafe(async () =>
        {
            var result = await _catalogItemRepository.GetByBrandAsync(brandId, pageIndex, pageSize);
            if (result == null)
            {
                return null;
            }

            return new PaginatedItemsResponse<CatalogItemDto>()
            {
                Count = result.TotalCount,
                Data = result.Data.Select(s => _mapper.Map<CatalogItemDto>(s)).ToList(),
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        });
    }

    public async Task<PaginatedItemsResponse<CatalogItemDto>?> GetCatalogItemByTypeAsync(int typeId, int pageSize, int pageIndex)
    {
        return await ExecuteSafe(async () =>
        {
            var result = await _catalogItemRepository.GetByTypeAsync(typeId, pageIndex, pageSize);
            if (result == null)
            {
                return null;
            }

            return new PaginatedItemsResponse<CatalogItemDto>()
            {
                Count = result.TotalCount,
                Data = result.Data.Select(s => _mapper.Map<CatalogItemDto>(s)).ToList(),
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        });
    }

    public Task<CatalogItemDto?> GetCatalogItemByIdAsync(int id)
    {
        return ExecuteSafe(async () =>
        {
            var result = await _catalogItemRepository.GetById(id);
            if (result == null)
            {
                return null;
            }

            return _mapper.Map<CatalogItemDto>(result);
        });
    }

    public Task<PaginatedItemsResponse<CatalogBrandDto>?> GetCatalogBrandsAsync(int pageSize, int pageIndex)
    {
        return ExecuteSafe(async () =>
        {
            var result = await _catalogBrandRepository.GetByPageAsync(pageIndex, pageSize);
            if (result == null)
            {
                return null;
            }

            return new PaginatedItemsResponse<CatalogBrandDto>()
            {
                Count = result.TotalCount,
                Data = result.Data.Select(s => _mapper.Map<CatalogBrandDto>(s)).ToList(),
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        });
    }

    public Task<PaginatedItemsResponse<CatalogTypeDto>?> GetCatalogTypesAsync(int pageSize, int pageIndex)
    {
        return ExecuteSafe(async () =>
        {
            var result = await _catalogTypeRepository.GetByPageAsync(pageIndex, pageSize);
            if (result == null)
            {
                return null;
            }

            return new PaginatedItemsResponse<CatalogTypeDto>()
            {
                Count = result.TotalCount,
                Data = result.Data.Select(s => _mapper.Map<CatalogTypeDto>(s)).ToList(),
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        });
    }
}