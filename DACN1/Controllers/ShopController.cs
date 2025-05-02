using Microsoft.AspNetCore.Mvc;

namespace DACN1.Controllers
{
	public class ShopController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
