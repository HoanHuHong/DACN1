using DACN1.Models;
using DACN1.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // để dùng Session

namespace DACN1.Controllers
{
	public class LoginController : Controller
	{
		private readonly BanHqContext _context;

		public LoginController(BanHqContext context)
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

			// Mã hoá mật khẩu người dùng
			string pw = Function.MD5Password(user.Password);

			// Kiểm tra tài khoản trong CSDL
			var check = _context.TbAccounts
				.FirstOrDefault(m => m.Email == user.Email && m.Password == pw);

			if (check == null)
			{
				Function._Message = "Invalid UserName or Password!";
				return RedirectToAction("Index", "Login");
			}

			// ✅ Lưu vào SESSION
			HttpContext.Session.SetInt32("UserID", check.AccountId);
			HttpContext.Session.SetString("UserName", check.Username ?? "");
			HttpContext.Session.SetString("Email", check.Email ?? "");

			// ✅ Đồng bộ vào biến static Function
			Function._Message = string.Empty;
			Function._UserID = check.AccountId;
			Function._UserName = check.Username ?? "";
			Function._Email = check.Email ?? "";

			return RedirectToAction("Index", "Home");
		}

		// ✅ Đăng xuất
		public IActionResult Logout()
		{
			// Xoá Session
			HttpContext.Session.Clear();

			// Reset các biến static
			Function._UserID = 0;
			Function._UserName = string.Empty;
			Function._Email = string.Empty;
			Function._Message = string.Empty;

			return RedirectToAction("Index", "Home");
		}
	}
}
