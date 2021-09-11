using DentalShop.Areas.ViewModels;
using DentalShop.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentalShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GaimeController : Controller
    {
        private readonly DentalShopDbContext _context;

        public GaimeController(DentalShopDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var gaimeler = _context.ProductOrders
                .GroupBy(x => new { x.AppUserId , x.Date})
                .Select(g => new GaimeViewModel { DateTime = g.Key.Date });
            return View(gaimeler);
        }
    }
}
