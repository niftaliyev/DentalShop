using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DentalShop.Models;
using DentalShop.Areas.ViewModels;
using DentalShop.Models.Identity;
using Microsoft.AspNetCore.Authorization;

namespace DentalShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductOrdersController : Controller
    {
        private readonly DentalShopDbContext _context;

        public ProductOrdersController(DentalShopDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ProductOrders
        public async Task<IActionResult> Index()
        {
            var dentalShopDbContext = _context.ProductOrders
                .Where(x => x.Delivery == Model.Delivery.YENI)
                .Include(p => p.AppUser)
                .GroupBy(x => new { x.AppUser.Id, x.AppUser.Name, x.AppUser.Email })
                .Select(o => new OrderUserViewModel
                {
                    Id = o.Key.Id,
                    Email = o.Key.Email,
                    Name = o.Key.Name,

                })
                .ToList();



            return View(dentalShopDbContext);
        }

        // GET: Admin/ProductOrders/Details/5
        public IActionResult Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productOrder = _context.ProductOrders
                .Where(x => x.Delivery == Model.Delivery.YENI)
                .Include(p => p.AppUser)
                .Include(p => p.Product)
                .Where(x => x.AppUserId == id)
                .ToList();

            if (productOrder == null)
            {
                return NotFound();
            }

            DetailOrdersViewModel orderUserViewModel = new DetailOrdersViewModel();
            orderUserViewModel.ProductOrders = productOrder;
            orderUserViewModel.Name = _context.Users.FirstOrDefault(x => x.Id == id).Name;
            orderUserViewModel.Id = id;


            return View(orderUserViewModel);
        }

        // GET: Admin/ProductOrders/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id");
            return View();
        }

        // POST: Admin/ProductOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,Count,AppUserId")] ProductOrder productOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", productOrder.AppUserId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", productOrder.ProductId);
            return View(productOrder);
        }


        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productOrder = _context.ProductOrders.Where(x => x.AppUserId == id);
            if (productOrder == null)
            {
                return NotFound();
            }
            foreach (var item in productOrder)
            {
                item.Delivery = Model.Delivery.CATDIRILDI;
                item.Date = DateTime.Now;
                _context.Update(item);

            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "ProductOrders");

        }


        

        private bool ProductOrderExists(int id)
        {
            return _context.ProductOrders.Any(e => e.Id == id);
        }
    }
}
