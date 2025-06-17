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
		public IActionResult Checkout()
		{
			var cartItems = Cart;
			return View(cartItems);
		}

		[HttpPost]
		public IActionResult CheckoutConfirm(string customerName, string phone, string address)
		{
			var cartItems = Cart;

			if (cartItems == null || !cartItems.Any())
			{
				return RedirectToAction("Index", "Home");
			}

			string orderCode = "DH" + DateTime.Now.ToString("yyyyMMddHHmmss");

			var order = new TbOrder
			{
				Code = orderCode,
				CustomerName = customerName,
				Phone = phone,
				Address = address,
			
				TotalAmount = cartItems.Sum(x => (int)x.PriceTotal),
				Quanlity = cartItems.Sum(x => x.Quantity),
				OrderStatusId = 1,
				CreatedDate = DateTime.Now,
				CreatedBy = "Customer",
			};

			_context.TbOrders.Add(order);
			_context.SaveChanges();

			foreach (var item in cartItems)
			{
				var orderDetail = new TbOrderDetail
				{
					OrderId = order.OrderId,
					ProductId = item.ProductId,
					Price = item.Price,
					Quantity = item.Quantity
				};
				_context.TbOrderDetails.Add(orderDetail);
			}

			_context.SaveChanges();
			HttpContext.Session.Remove(CART_KEY);

            TempData["SuccessMessage"] = "Đặt hàng thành công!";
            return RedirectToAction("OrderSucess");

        }
		public IActionResult OrderSucess()
		{
			return View();
		}
	}
}