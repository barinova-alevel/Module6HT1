namespace Catalog.Host.Models.Requests
{
    public class ByBrandRequest: PaginatedItemsRequest
    {
        public int BrandId { get; set; }
    }
}
