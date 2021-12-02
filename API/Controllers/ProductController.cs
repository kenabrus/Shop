using System.Collections.Generic;
using Core.Entities;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace API.Controllers
{
    public class ProductController : BaseApiController
    {
        public ProductController()
        {

        }
        [HttpGet("GetProducts")]
        public IEnumerable<Product> Products()
        {
            return new List<Product>()
            {
                new Product(){Id = new Guid(), Name = "Jack" },
                new Product(){Id = new Guid(), Name = "Paul" },
                new Product(){Id = new Guid(), Name = "Olaf" }
            };
        }
    }
}