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
                .Include(p => p.AppUser)
                .GroupBy(x => new {   x.AppUser.Id , x.AppUser.Name , x.AppUser.Email })
                .Select(o => new OrderUserViewModel
                {
                     Id = o.Key.Id,
                     Email = o.Key.Email,
                     Name = o.Key.Name

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
                .Include(p => p.AppUser)
                .Include(p => p.Product)
                .Where(x => x.AppUserId == id)
                .ToList();
            if (productOrder == null)
            {
                return NotFound();
            }

            return View(productOrder);
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

        // GET: Admin/ProductOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productOrder = await _context.ProductOrders.FindAsync(id);
            if (productOrder == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", productOrder.AppUserId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", productOrder.ProductId);
            return View(productOrder);
        }

        // POST: Admin/ProductOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,Count,AppUserId")] ProductOrder productOrder)
        {
            if (id != productOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductOrderExists(productOrder.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", productOrder.AppUserId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", productOrder.ProductId);
            return View(productOrder);
        }

        // GET: Admin/ProductOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productOrder = await _context.ProductOrders
                .Include(p => p.AppUser)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productOrder == null)
            {
                return NotFound();
            }

            return View(productOrder);
        }

        // POST: Admin/ProductOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productOrder = await _context.ProductOrders.FindAsync(id);
            _context.ProductOrders.Remove(productOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductOrderExists(int id)
        {
            return _context.ProductOrders.Any(e => e.Id == id);
        }
    }
}
