using DACN1.Models;
using Microsoft.AspNetCore.Mvc;
using PagedList;
using X.PagedList.Mvc;
namespace DACN1.Controllers
{
	public class ShopController : Controller
	{
		private readonly BanHqContext _context;
		public ShopController(BanHqContext context)
		{
			_context = context;
		}
		public IActionResult Index(int? page)
		{
			int pageSize = 9;
			int pageNumber = page ?? 1;
			ViewBag.productCategories = _context.TbProductCategories.ToList();
			var products = _context.TbProducts.ToPagedList(pageNumber, pageSize);
			return View(products);
		}
	}
}
