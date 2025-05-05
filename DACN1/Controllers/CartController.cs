using Microsoft.AspNetCore.Mvc;
using DACN1.Models;
using DACN1.Helpers;
using DACN1.ViewModel;
namespace DACN1.Controllers
{
	public class CartController : Controller
	{
		private readonly BanHqContext _context;
		public CartController(BanHqContext context)
		{
			_context = context;
		}
		const string CART_KEY = "MYCART";
		public List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>(CART_KEY) ?? new List<CartItem>();
		public IActionResult Index()
		{
			return View(Cart);
		}
		public IActionResult AddToCart(int id, int q = 1)
		{
			var gioHang = Cart;
			var item = gioHang.SingleOrDefault(p => p.ProductId == id);
			if (item == null)
			{
				var hangHoa = _context.TbProducts.SingleOrDefault(p => p.ProductId == id);
				if (hangHoa == null)
				{
					return NotFound();
				}
				item = new CartItem
				{
					ProductId = hangHoa.ProductId,
					Title = hangHoa.Title,
					Price = hangHoa.Price,
					Image = hangHoa.Image ?? string.Empty,
					Quantity = q,
				};
				gioHang.Add(item);
			}
			else
			{
				item.Quantity += q;
			}
			HttpContext.Session.Set(CART_KEY, gioHang);
			return RedirectToAction("Index");
		}
		public IActionResult RemoveCart(int id)
		{
			var gioHang = Cart;
			var item = gioHang.SingleOrDefault(p => p.ProductId==id);
			if (item != null)
			{
				gioHang.Remove(item);
				HttpContext.Session.Set(CART_KEY, gioHang);
			}
			return RedirectToAction("Index");
		}
	}
}
