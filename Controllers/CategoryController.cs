using DentalShop.Areas.Admin.Model;
using DentalShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly DentalShopDbContext _context;

        public CategoryController(DentalShopDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetParentCategory(int id)
        {
            var categories = _context.Categories.Where(x => x.Id == id).ToList();
            return categories;
        }
    }
}
