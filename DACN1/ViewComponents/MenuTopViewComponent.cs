
using DACN1.Models;
using Microsoft.AspNetCore.Mvc;

namespace DACN1.ViewComponents
{
    public class MenuTopViewComponent : ViewComponent
    {
        private readonly BanHqContext _context;

        public MenuTopViewComponent(BanHqContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = _context.TbMenus.Where(m => (bool)m.IsActive).
                OrderBy(m => m.Position).ToList();
            return await Task.FromResult<IViewComponentResult>(View(items));
        }
    }
}
