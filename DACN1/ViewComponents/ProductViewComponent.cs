using DACN1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DACN1.ViewComponents
{

	public class ProductViewComponent : ViewComponent
	{
		private readonly BanHqContext _context;

		public ProductViewComponent(BanHqContext context)
		{
			_context = context;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var items = _context.TbProducts.Include(m => m.CategoryProduct)
				.Where(m => (bool)m.IsActive).Where(m => m.IsNew);
			return await Task.FromResult<IViewComponentResult>
				(View(items.OrderBy(m => m.ProductId).ToList()));
		}
	}
}
