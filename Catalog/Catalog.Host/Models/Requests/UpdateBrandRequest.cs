namespace Catalog.Host.Models.Requests
{
    public class UpdateBrandRequest
    {        
        public int BrandId { get; set; }
        public string BrandName { get; set; } = null!;
    }
}
