using DentalShop.Areas.Admin.Model;
using DentalShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsCategorysController : Controller
    {
        private readonly DentalShopDbContext _context;

        public ProductsCategorysController(DentalShopDbContext context)
        {
            _context = context;
        }
        // GET: api/ProductsCategory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Product>>> ProductsCategory(int id)
        {
            var products = await _context.Products
                     .Where(x => x.CategoryId == id)
                     .Include(x => x.Images)
                     .Select(x => new Product
                     {
                         Id = x.Id,
                         Images = x.Images,
                         CoverImage = x.CoverImage,
                         Price = x.Price,
                         Description = x.Description,
                         Category = x.Category,
                         IsActive = x.IsActive,
                         Color = x.Color,
                         Link = x.Link,
                         Currency = x.Currency,
                         Title = x.Title,
                         CategoryId = x.CategoryId
                     }).ToListAsync();

            return products;
        }
    }
}
