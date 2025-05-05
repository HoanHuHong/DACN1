using DACN1.Models;
using Microsoft.AspNetCore.Mvc;

namespace DACN1.Controllers
{
	public class ContactController : Controller
	{
		private readonly BanHqContext _context;
		public ContactController(BanHqContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(string name, string phone, string email, string message)
		{
			try
			{
				TbContact contact = new TbContact();
				contact.Name = name;
				contact.Phone = phone;
				contact.Email = email;
				contact.Message = message;
				contact.CreatedDate = DateTime.Now;
				_context.Add(contact);
				_context.SaveChanges();
				return Json(new { status = true });
			}
			catch
			{
				return Json(new { status = false });
			}
		}
	}
}
