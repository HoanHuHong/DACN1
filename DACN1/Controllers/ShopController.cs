using DACN1.Models;
using Microsoft.AspNetCore.Mvc;

namespace DACN1.Controllers
{
	public class ShopController : Controller
	{
		private readonly BanHqContext _context;
		public ShopController(BanHqContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			ViewBag.productCategories = _context.TbProductCategories.ToList();
			var products = _context.TbProducts.ToList();
			return View(products);
		}
	}
}
