using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DentalShop.Models;
using DentalShop.Models.Identity;

namespace DentalShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserOrdersController : Controller
    {
        private readonly DentalShopDbContext _context;

        public UserOrdersController(DentalShopDbContext context)
        {
            _context = context;
        }

        // GET: Admin/UserOrders
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var orderusers = _context.UserOrders.ToList();
            List<AppUser> users = new List<AppUser>();

            foreach (var item in orderusers)
            {
                users.Add(_context.Users.Where(x => x.Id == item.AppUserId).FirstOrDefault());

            }
            


            return View(users);
        }

        // GET: Admin/UserOrders/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var userOrder = await _context.PorductOrders
            //    .FirstOrDefaultAsync(m => m.UserId == id);
            //if (userOrder == null)
            //{
            //    return NotFound();
            //}
            //var user = _context.Users.Where(x => x.Id == id).FirstOrDefault();
            return View();
        }

        // GET: Admin/UserOrders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/UserOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId")] UserOrder userOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userOrder);
        }

        // GET: Admin/UserOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userOrder = await _context.UserOrders.FindAsync(id);
            if (userOrder == null)
            {
                return NotFound();
            }
            return View(userOrder);
        }

        // POST: Admin/UserOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId")] UserOrder userOrder)
        {
            if (id != userOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserOrderExists(userOrder.Id))
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
            return View(userOrder);
        }

        // GET: Admin/UserOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userOrder = await _context.UserOrders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userOrder == null)
            {
                return NotFound();
            }

            return View(userOrder);
        }

        // POST: Admin/UserOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userOrder = await _context.UserOrders.FindAsync(id);
            _context.UserOrders.Remove(userOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserOrderExists(int id)
        {
            return _context.UserOrders.Any(e => e.Id == id);
        }
    }
}
