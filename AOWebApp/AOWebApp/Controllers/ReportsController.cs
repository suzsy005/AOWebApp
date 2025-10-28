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
				.OrderByDescending(i => i);
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
				.Where(iio => iio.OrderNumberNavigation.OrderDate.Year == year)
				.GroupBy(iio => iio.OrderNumberNavigation.OrderDate.Month)
				.Select(iio => new
				{
					monthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(iio.Key),
					Month = iio.Key,
					Year = year,
					monthNo = iio.Key,
					totalItems = iio.Sum(iio => iio.NumberOf),
					totalSales = iio.Sum(iio => iio.TotalItemCost),
				})
				.OrderBy(iio => iio.Month)
				.AsEnumerable();

				return Json(yearMatch);
		}

	}
}
