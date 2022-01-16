#pragma warning disable CS8618
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Data.Enums;

public class CatalogBrand: IBaseEntity
{
    public int Id { get; set; }

    public string Brand { get; set; }
}