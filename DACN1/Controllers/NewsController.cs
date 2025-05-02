using Microsoft.AspNetCore.Mvc;

namespace DACN1.Controllers
{
	public class NewsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
