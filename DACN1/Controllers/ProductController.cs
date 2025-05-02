using DACN1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DACN1.Controllers
{
	public class ProductController : Controller
	{
		private readonly BanHqContext _context;
		public ProductController(BanHqContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			return View();
		}
		[Route("/product/{alias}-{id}.html")]
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.TbProducts == null)
			{
				return NotFound();
			}
			var product = await _context.TbProducts
	.Include(i => i.CategoryProduct)
	.FirstOrDefaultAsync(m => m.ProductId == id);

			if (product == null)
			{
				return NotFound(); 
			}
			ViewBag.productRelated = _context.TbProducts.
				Where(i => i.ProductId != id && i.CategoryProductId == product.CategoryProductId).Take(3).OrderByDescending(i => i.ProductId).ToList();
			return View(product);
		}
	}
}
