using Catalog.Host.Models;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogController : ControllerBase
    {
        private static readonly Product[] products = new[]
        {
            new Product() { Id = 1, Name = "OOOOOOOppo A53s", Price = 7990, Avatar = "https://cdn.comfy.ua/media/cms/cat_2/smartfony-xiaomi.png"},
            new Product() { Id = 2, Name = "Apple iPhone 13 Pro", Price = 38499, Avatar = "https://cdn.comfy.ua/media/cms/cat_2/smartfony-apple.png"},
            new Product() { Id = 3, Name = "Apple iPhone 11 64Gb Black", Price = 17999, Avatar = "https://cdn.comfy.ua/media/x/img/file_2.png"},
            new Product() { Id = 4, Name = "Oppo A53s", Price = 7990, Avatar = "https://cdn.comfy.ua/media/cms/cat_2/smartfony-xiaomi.png"},
        };

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return products;
        }
    }
}
