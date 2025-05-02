using Microsoft.AspNetCore.Mvc;

namespace DACN1.Controllers
{
	public class CartController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
