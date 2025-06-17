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
    public class OrdersController : Controller
    {
        private readonly BanHqContext _context;

        public OrdersController(BanHqContext context)
        {
            _context = context;
        }

        // GET: Admin/Orders
        public async Task<IActionResult> Index()
        {
            var banHqContext = _context.TbOrders.Include(t => t.OrderStatus);
            return View(await banHqContext.ToListAsync());
        }

        // GET: Admin/Orders/Details/5
        public async Task<IActionResult> ChangeStatus(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbOrder = await _context.TbOrders.FindAsync(id);
            if (tbOrder == null)
            {
                return NotFound();
            }
            ViewData["TrangThai"] = new SelectList(_context.TbOrderStatuses, "OrderStatusId", "Name", tbOrder.OrderStatusId);
            return View(tbOrder);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(int id, [Bind("OrderId,OrderStatusId")] TbOrder tbOrder)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingOrder = await _context.TbOrders.FindAsync(id);
                    if (existingOrder == null)
                    {
                        return NotFound();
                    }

                    // Cập nhật trạng thái
                    existingOrder.OrderStatusId = tbOrder.OrderStatusId;
                    _context.Update(existingOrder);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.TbOrders.Any(e => e.OrderId == tbOrder.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            ViewData["TrangThai"] = new SelectList(_context.TbOrderStatuses, "OrderStatusId", "Name", tbOrder.OrderStatusId);
            return View(tbOrder);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbOrder = await _context.TbOrders
                .Include(t => t.OrderStatus)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (tbOrder == null)
            {
                return NotFound();
            }

            return View(tbOrder);
        }

        // GET: Admin/Orders/Create
        public IActionResult Create()
        {
            ViewData["OrderStatusId"] = new SelectList(_context.TbOrderStatuses, "OrderStatusId", "OrderStatusId");
            return View();
        }

        // POST: Admin/Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,Code,CustomerName,Phone,Address,TotalAmount,Quanlity,OrderStatusId,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy")] TbOrder tbOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tbOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderStatusId"] = new SelectList(_context.TbOrderStatuses, "OrderStatusId", "OrderStatusId", tbOrder.OrderStatusId);
            return View(tbOrder);
        }

        // GET: Admin/Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbOrder = await _context.TbOrders.FindAsync(id);
            if (tbOrder == null)
            {
                return NotFound();
            }
            ViewData["OrderStatusId"] = new SelectList(_context.TbOrderStatuses, "OrderStatusId", "OrderStatusId", tbOrder.OrderStatusId);
            return View(tbOrder);
        }

        // POST: Admin/Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,Code,CustomerName,Phone,Address,TotalAmount,Quanlity,OrderStatusId,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy")] TbOrder tbOrder)
        {
            if (id != tbOrder.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tbOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TbOrderExists(tbOrder.OrderId))
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
            ViewData["OrderStatusId"] = new SelectList(_context.TbOrderStatuses, "OrderStatusId", "OrderStatusId", tbOrder.OrderStatusId);
            return View(tbOrder);
        }

        // GET: Admin/Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tbOrder = await _context.TbOrders
                .Include(t => t.OrderStatus)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (tbOrder == null)
            {
                return NotFound();
            }

            return View(tbOrder);
        }

        // POST: Admin/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tbOrder = await _context.TbOrders.FindAsync(id);
            if (tbOrder != null)
            {
                _context.TbOrders.Remove(tbOrder);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TbOrderExists(int id)
        {
            return _context.TbOrders.Any(e => e.OrderId == id);
        }
    }
}
