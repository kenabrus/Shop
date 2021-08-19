using System.Collections.Generic;
using Core.Entities;

namespace API.Controllers
{
    public class ProductController : BaseApiController
    {
        public ProductController()
        {

        }

        public IEnumerable<Product> Products()
        {
            return new List<Product>();
        }
    }
}