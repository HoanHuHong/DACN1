using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DACN1.Models;

namespace DACN1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RevenueController : Controller
    {
        private readonly BanHqContext _context;

        public RevenueController(BanHqContext context)
        {
            _context = context;
        }

        public IActionResult Index(DateTime? fromDate, DateTime? toDate)
        {
            var query = _context.TbOrders
                .Include(o => o.OrderStatus)
                .Include(o => o.TbOrderDetails)
                    .ThenInclude(od => od.Product)
                .Where(o => o.OrderStatusId == 2);

            if (fromDate.HasValue)
            {
                query = query.Where(o => o.CreatedDate >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                query = query.Where(o => o.CreatedDate < toDate.Value.AddDays(1));
            }

            var orders = query.OrderByDescending(o => o.CreatedDate).ToList();

            int totalRevenue = orders.Sum(o => o.TotalAmount ?? 0);
            

  

            ViewBag.TotalRevenue = totalRevenue;
       

            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;

            return View(orders);
        }
    }
}