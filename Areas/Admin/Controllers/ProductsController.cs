using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DentalShop.Areas.Admin.Model;
using DentalShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using DentalShop.Helper;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using DentalShop.Areas.ViewModels;
using System.Security.Claims;

namespace DentalShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly DentalShopDbContext _context;
        IWebHostEnvironment _appEnvironment;

        public ProductsController(DentalShopDbContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index(int? id)
        {
            var dentalShopDbContext = _context.Products.Include(p => p.Category).OrderByDescending(x => x.Id).Where(x => x.CategoryId == id);
            return View(await dentalShopDbContext.OrderBy(x => x.Id).ToListAsync());
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            var images = await _context.Images.Where(x => x.ProductId == id).ToListAsync();

            ProductViewModel productViewModel = new ProductViewModel();

            productViewModel.Product = product;
            productViewModel.Images = images;

            if (product == null)
            {
                return NotFound();
            }

            return View(productViewModel);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories.Where(x => x.ParentCategory == null), "Id", "Title");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,CoverImage,Price,Description,CategoryId,IsActive,Color,Link,Currency")] Product product, IFormFileCollection Image,int categoryname)
        {
            //var path = await FileUploadHelper.UploadAsync(Image[2]);

            List<Image> images = new List<Image>();


            //product.CoverImage = path;
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();

                foreach (var item in Image)
                {
                    var path = await FileUploadHelper.UploadAsync(item);
                    images.Add(new Image { ProductImage = path, ProductId = product.Id }); ;
                }
                _context.Images.AddRange(images);
                _context.SaveChanges();

                product.CoverImage = _context.Images.Where(x => x.ProductId == product.Id).FirstOrDefault().ProductImage;
                product.CategoryId = categoryname;
                _context.SaveChanges();


                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Title", product.CategoryId);


            


            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Title", product.CategoryId);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,CoverImage,Price,Description,CategoryId,IsActive,Color,Link,Currency")] Product product, IFormFileCollection Image)
        {
            //var path = await FileUploadHelper.UploadAsync(Image[1]);

            foreach (var uploadedFile in Image)
            {
                // путь к папке Files
                string path = "/uploads/" + uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                product.CoverImage = path;

            }



            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", product.CategoryId);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Admin");

        }
    }
}
