namespace Catalog.Host.Models.Requests
{
    public class ByTypeRequest: PaginatedItemsRequest
    {
        public int TypeId { get; set; }
    }
}
