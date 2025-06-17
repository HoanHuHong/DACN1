using Microsoft.AspNetCore.Mvc;


using DACN1.Utilities;
using DACN1.Models;

namespace DACN1.Controllers
{
	public class RegisterController : Controller
	{
		private readonly BanHqContext _context;
		public RegisterController(BanHqContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Index(TbAccount user)
		{
			if (user == null)
			{
				return NotFound();
			}

			var check = _context.TbAccounts.Where(m => m.Email == user.Email).FirstOrDefault();
			if (check != null)
			{
				Function._MessageEmail = "Duplicate Email!";
				return RedirectToAction("Index", "Register");
			}
			Function._MessageEmail = string.Empty;
			user.Password = Function.MD5Password(user.Password);
			_context.Add(user);
			_context.SaveChanges();

			return RedirectToAction("Index", "Login");
		}
	}
}

