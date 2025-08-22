using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AOWebApp.Data;
using AOWebApp.Models;
using AOWebApp.ViewModels;

namespace AOWebApp.Controllers
{
    public class ItemsController : Controller
    {
        private readonly AmazonOrders2025Context _context;

        public ItemsController(AmazonOrders2025Context context)
        {
            _context = context;
        }

        // Get: Items
        // this appears when user comes to this Index page for the first time
        // Items are ordered in ItemId
        // was missing the ViewBag.Categories, that's why the categories list didn't display categories name but only "All Categories..." before input
        [HttpGet]
        public async Task<IActionResult> Index(int? categoryId)
        {
            var amazonOrders2025Categories = _context.ItemCategories.
                Where(ic => !ic.ParentCategoryId.HasValue)
                .OrderBy(ic => ic.CategoryName)
                .Select(ic => new { ic.CategoryId, ic.CategoryName })
                .ToList();

            ViewBag.Categories = new SelectList(amazonOrders2025Categories.Select(i => i).ToList(),
                                     nameof(ItemCategory.CategoryId),
                                     nameof(ItemCategory.CategoryName),
                                     categoryId);

            var amazonOrder2025Context = _context.Items.Include(i => i.Category);
            return View(await amazonOrder2025Context.ToListAsync());
        }

        // Post: Items
        // the orders of Items turns into Alphabetical
        [HttpPost]
        public async Task<IActionResult> Index(ItemSearchViewModel vm)
        {
            #region CategoriesQuery
            var amazonOrders2025Categories = _context.ItemCategories.
                Where(ic => !ic.ParentCategoryId.HasValue)
                .OrderBy(ic => ic.CategoryName)
                .Select(ic => new { ic.CategoryId, ic.CategoryName })
                .ToList();

            vm.CategoryList = new SelectList(amazonOrders2025Categories.Select(i => i).ToList(), 
                                     nameof(ItemCategory.CategoryId),
                                     nameof(ItemCategory.CategoryName),
                                     vm.CategoryId);
            #endregion

            #region ItemQuery
            //ViewBag.SearchText = SearchText;
            var amazonOrders2025Context = _context.Items
                .Include(i => i.Category)
                .OrderBy(i => i.ItemName)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(vm.SearchText))
            {
                amazonOrders2025Context = amazonOrders2025Context
                    .Where(i => i.ItemName.Contains(vm.SearchText));
            }

            if (vm.CategoryId.HasValue)
            {
                amazonOrders2025Context = amazonOrders2025Context
                    .Where(i => i.Category.CategoryId == vm.CategoryId || i.Category.ParentCategoryId == vm.CategoryId);
            }

            amazonOrders2025Context = amazonOrders2025Context.OrderBy(i => i.ItemName);
            #endregion

            return View(await amazonOrders2025Context.ToListAsync());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "CategoryId", "CategoryId");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,ItemName,ItemDescription,ItemCost,ItemImage,CategoryId")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "CategoryId", "CategoryId", item.CategoryId);
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "CategoryId", "CategoryId", item.CategoryId);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,ItemName,ItemDescription,ItemCost,ItemImage,CategoryId")] Item item)
        {
            if (id != item.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "CategoryId", "CategoryId", item.CategoryId);
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.ItemId == id);
        }
    }
}
