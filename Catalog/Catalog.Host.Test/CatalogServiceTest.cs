using AutoMapper;
using Castle.Core.Logging;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services;
using Catalog.Host.Services.Interfaces;
using FluentAssertions;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Catalog.Host.Test
{
    public class CatalogServiceTest
    {
        private readonly ICatalogService _catalogService;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<BaseDataService<ApplicationDbContext>>> _logger;
        private readonly Mock<ICatalogItemRepository> _catalogItemRepository;
        private readonly Mock<ICatalogBrandRepository> _catalogBrandRepository;
        private readonly Mock<ICatalogTypeRepository> _catalogTypeRepository;
        private readonly Mock<IMapper> _mapper;

        public CatalogServiceTest()
        {

            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            _logger = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            _catalogItemRepository = new Mock<ICatalogItemRepository>();
            _catalogBrandRepository = new Mock<ICatalogBrandRepository>();
            _catalogTypeRepository = new Mock<ICatalogTypeRepository>();
            _mapper = new Mock<IMapper>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransaction()).Returns(dbContextTransaction.Object);

            _catalogService = new CatalogService(
                _dbContextWrapper.Object,
                _logger.Object,
                _catalogItemRepository.Object,
                _catalogBrandRepository.Object,
                _catalogTypeRepository.Object,
                _mapper.Object);
        }

        [Fact]
        public async Task GetCatalogItemsAsync_UseValidParameters_ReturnCorrespondigData()
        {
            // Arrange
            var pageIndex = 1;
            var pageSize = 2;
            var catalogItem1 = new CatalogItem()
            {
                Name = "Name1"
            };
            var catalogItem2 = new CatalogItem()
            {
                Name = "Name2"
            };
            _catalogItemRepository
               .Setup(_ => _.GetByPageAsync(It.IsAny<int>(), It.IsAny<int>()))
               .ReturnsAsync(new PaginatedItems<CatalogItem>()
               {
                   TotalCount = 2,
                   Data = new List<CatalogItem>() { catalogItem1, catalogItem2 }
               });
            _mapper
                .Setup(s => s.Map<CatalogItemDto>(It.Is<CatalogItem>(_ => _ == catalogItem1)))
                .Returns(new CatalogItemDto { Name = "Name1"});
            _mapper
                .Setup(s => s.Map<CatalogItemDto>(It.Is<CatalogItem>(_ => _ == catalogItem2)))
                .Returns(new CatalogItemDto { Name = "Name2" });

            // Act
            var result = await _catalogService.GetCatalogItemsAsync(pageSize, pageIndex);

            // Assert
            result.Should().NotBeNull();
            result?.PageIndex.Should().Be(pageIndex);
            result?.PageSize.Should().Be(pageSize);
            result?.Count.Should().Be(2);
            result?.Data.Should().NotBeNull().And.HaveCount(2);
        }

        [Fact]
        public async Task GetCatalogItemsAsync_UseZeroPageIndexPageSize_ReturnEmptyData()
        {
            // Arrange
            _catalogItemRepository
                .Setup(_ => _.GetByPageAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new PaginatedItems<CatalogItem>()
                {
                    Data = new List<CatalogItem>()
                });

            // Act
            var result = await _catalogService.GetCatalogItemsAsync(0, 0);

            // Assert
            result.Should().NotBeNull();
            result?.PageIndex.Should().Be(0);
            result?.PageSize.Should().Be(0);
            result?.Data.Should().NotBeNull().And.BeEmpty();
        }

        [Fact]
        public async Task GetCatalogItemsAsync_InvalidPageIndex_ReturnNull()
        {
            // Arrange
            const int invalidIndex = 1;
            _catalogItemRepository
                .Setup(_ => _.GetByPageAsync(It.Is<int>(i => i == invalidIndex), It.IsAny<int>()))
                .ReturnsAsync((PaginatedItems<CatalogItem>)null!);

            // Act
            var result = await _catalogService.GetCatalogItemsAsync(10, invalidIndex);

            // Assert
            result.Should().BeNull();

        }
    }
}