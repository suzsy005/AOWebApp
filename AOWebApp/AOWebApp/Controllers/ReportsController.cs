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

		[Produces("application/json")]
		public IActionResult AnnualSalesReportData(int? year)
		{

			if (!year.HasValue || year.Value <= 0)
			{
				return BadRequest();
			}
			var yearMatch = _context.ItemsInOrders
				.Where(iio => iio.OrderNumberNavigation.OrderDate.Year == year)
				.GroupBy(iio => iio.OrderNumberNavigation.OrderDate.Month)
				.Select(iio => new
				{
					year = year,
					monthNo = iio.Key,
					monthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(iio.Key),
					totalItems = iio.Sum(j => j.NumberOf),
					totalSales = iio.Sum(j => j.TotalItemCost),
				})
				.OrderBy(iio => iio.monthNo)
				.AsEnumerable();

			return Json(yearMatch);
		}

	}
}
