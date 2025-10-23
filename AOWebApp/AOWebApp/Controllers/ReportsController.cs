using AOWebApp.Data;
using Microsoft.AspNetCore.Mvc;

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
			return View("AnnualSalesReport");
		}
	}
}
