using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DentalShop.Models;
using Microsoft.AspNetCore.Authorization;

namespace DentalShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrderProductsUsersController : Controller
    {
        private readonly DentalShopDbContext _context;

        public OrderProductsUsersController(DentalShopDbContext context)
        {
            _context = context;
        }

        // GET: Admin/OrderProductsUsers
        public async Task<IActionResult> Index()
        {
            return View(await _context.OrderProductUsers.ToListAsync());
        }

        // GET: Admin/OrderProductsUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderProductsUsers = await _context.OrderProductUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderProductsUsers == null)
            {
                return NotFound();
            }

            return View(orderProductsUsers);
        }

        // GET: Admin/OrderProductsUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/OrderProductsUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,ProductId,Count")] OrderProductsUsers orderProductsUsers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderProductsUsers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orderProductsUsers);
        }

        // GET: Admin/OrderProductsUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderProductsUsers = await _context.OrderProductUsers.FindAsync(id);
            if (orderProductsUsers == null)
            {
                return NotFound();
            }
            return View(orderProductsUsers);
        }

        // POST: Admin/OrderProductsUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,ProductId,Count")] OrderProductsUsers orderProductsUsers)
        {
            if (id != orderProductsUsers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderProductsUsers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderProductsUsersExists(orderProductsUsers.Id))
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
            return View(orderProductsUsers);
        }

        // GET: Admin/OrderProductsUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderProductsUsers = await _context.OrderProductUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderProductsUsers == null)
            {
                return NotFound();
            }

            return View(orderProductsUsers);
        }

        // POST: Admin/OrderProductsUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderProductsUsers = await _context.OrderProductUsers.FindAsync(id);
            _context.OrderProductUsers.Remove(orderProductsUsers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderProductsUsersExists(int id)
        {
            return _context.OrderProductUsers.Any(e => e.Id == id);
        }
    }
}
