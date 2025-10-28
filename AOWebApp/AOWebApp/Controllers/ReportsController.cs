using AOWebApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
				.OrderBy(i => i);
				//.ToList();

			return View("AnnualSalesReport", new SelectList(yearList));
		}

		[Produces("application/json")]
		public IActionResult AnnualSalesReportData(int? year)
		{

			if (!year.HasValue || year.Value <= 0)
			{
				return BadRequest();
			}

			var yearMatch = _context.ItemsInOrders
				.Where(i => i.OrderNumberNavigation.OrderDate.Year == year)
				.GroupBy(i => i.OrderNumberNavigation.OrderDate.Month)
				.Select(i => new
				{
					monthName = System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(i.Key),
					Month = i.Key,
					Year = year,
					totalItems = i.Sum(iio => iio.NumberOf),
					totalSales = i.Sum(iio => iio.TotalItemCost),
				})
				.OrderBy(i => i.Month)
				.AsEnumerable();

				return Json(yearMatch);
		}

	}
}
