using Microsoft.AspNetCore.Mvc;

namespace DACN1.Controllers
{
	public class ContactController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
