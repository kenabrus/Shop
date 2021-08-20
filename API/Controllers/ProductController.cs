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
        [HttpGet("GetUsers")]
        public IEnumerable<Product> Products()
        {
            return new List<Product>()
            {
                new Product(){Id = 1, Name = "Jack" },
                new Product(){Id = 1, Name = "Paul" },
                new Product(){Id = 1, Name = "Olaf" }
            };
        }
    }
}