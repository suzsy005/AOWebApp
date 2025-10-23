using AOWebApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AOWebApp.Controllers
{
	public class ReportsController : Controller
	{
		private readonly AmazonOrders2025Context _context;

		public ReportsController(AmazonOrders2025Context context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			var yearList = _context.CustomerOrders
				.Select(i => i.OrderDate.Year)
				.Distinct()
				.OrderByDescending(i => i)
				.ToList();

			return View("AnnualSalesReport", new SelectList(yearList));
		}
	}
}
