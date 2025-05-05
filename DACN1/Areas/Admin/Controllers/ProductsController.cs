using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DACN1.Models;

namespace DACN1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly BanHqContext _context;

        public ProductsController(BanHqContext context)
        {
            _context = context;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            var banHqContext = _context.TbProducts.Include(t => t.CategoryProduct);
            return View(await banHqContext.ToListAsync());
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbProduct = await _context.TbProducts
                .Include(t => t.CategoryProduct)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (tbProduct == null)
            {
                return NotFound();
            }

            return View(tbProduct);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryProductId"] = new SelectList(_context.TbProductCategories, "CategoryProductId", "Title");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Title,Alias,CategoryProductId,Description,Detail,Image,Price,PriceSale,Quantity,CreatedDate,CreatedBy,IsNew,IsBestSeller,IsActive,Star")] TbProduct tbProduct)
        {
            if (ModelState.IsValid)
            {
                tbProduct.Alias = DACN1.Utilities.Function.TitleSlugGenerationAlias(tbProduct.Title);
                _context.Add(tbProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryProductId"] = new SelectList(_context.TbProductCategories, "CategoryProductId", "Title", tbProduct.CategoryProductId);
            return View(tbProduct);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbProduct = await _context.TbProducts.FindAsync(id);
            if (tbProduct == null)
            {
                return NotFound();
            }
            ViewData["CategoryProductId"] = new SelectList(_context.TbProductCategories, "CategoryProductId", "Title", tbProduct.CategoryProductId);
            return View(tbProduct);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Title,Alias,CategoryProductId,Description,Detail,Image,Price,PriceSale,Quantity,CreatedDate,CreatedBy,IsNew,IsBestSeller,IsActive,Star")] TbProduct tbProduct)
        {
            if (id != tbProduct.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbProductExists(tbProduct.ProductId))
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
            ViewData["CategoryProductId"] = new SelectList(_context.TbProductCategories, "CategoryProductId", "Title", tbProduct.CategoryProductId);
            return View(tbProduct);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbProduct = await _context.TbProducts
                .Include(t => t.CategoryProduct)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (tbProduct == null)
            {
                return NotFound();
            }

            return View(tbProduct);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbProduct = await _context.TbProducts.FindAsync(id);
            if (tbProduct != null)
            {
                _context.TbProducts.Remove(tbProduct);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbProductExists(int id)
        {
            return _context.TbProducts.Any(e => e.ProductId == id);
        }
    }
}
