
using DACN1.Models;
using DACN1.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace DoAn.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            string pw = Function.MD5Password(user.Password);

            var check = _context.TbAccounts.Where(m => (m.Email == user.Email) && (m.Password == pw)).FirstOrDefault();
            if (check == null)
            {
                Function._Message = "Invalid UserName or Password!";
                return RedirectToAction("Index", "Login");
            }
            Function._Message = string.Empty;
            Function._UserID = check.AccountId;
            Function._UserName = string.IsNullOrEmpty(check.Username) ? string.Empty : check.Username;
            Function._Email = string.IsNullOrEmpty(check.Email) ? string.Empty : check.Email;

            return RedirectToAction("Index", "Home");
        }
    }
}