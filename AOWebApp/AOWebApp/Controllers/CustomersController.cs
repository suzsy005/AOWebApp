﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AOWebApp.Data;
using AOWebApp.Models;
using System.Reflection.Metadata.Ecma335;

namespace AOWebApp.Controllers
{
    public class CustomersController : Controller
    {
        private readonly AmazonOrders2025Context _context;

        public CustomersController(AmazonOrders2025Context context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index(string SearchText, string Suburb)
        {
            #region Suburb Query
            var SuburbList = _context.Addresses
                .Select(a => a.Suburb)
                .Distinct()
                .OrderBy(a => a)
                .ToList();

            ViewBag.Suburb = new SelectList(SuburbList, Suburb);
            #endregion

            #region customerQuery
            List<Customer> CustomerList = new List<Customer>();

            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                var query = _context.Customers
                    .Include(c => c.Address)
                    .Where(c => c.FirstName.StartsWith(SearchText) || c.LastName.StartsWith(SearchText));
                   

                if (!string.IsNullOrEmpty(Suburb))
                {
                    query = query.Where(c => c.Address.Suburb == Suburb);
                }

                query = query
                    .OrderBy(c => !c.FirstName.StartsWith(SearchText))
                    .ThenBy(c => !c.LastName.StartsWith(SearchText));

                CustomerList = await  query.ToListAsync();
            }
            #endregion

            //// Suburb query
            //var suburbQuery = _context.Customers
            //    .Include(s => s.Address)
            //    .AsQueryable();

            //// Suburb list
            //var SuburbList = new SelectList(
            //    _context.Addresses
            //    .Where(s => s.Suburb.HasValue)
            //    .OrderBy(s => s.Suburb),
            //    nameof(Address.Postcode),
            //    nameof(Address.Suburb));


            return View(CustomerList);


            //var amazonOrders2025Context = _context.Customers.Include(c => c.Address);
            //return View(await amazonOrders2025Context.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.Address)
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,FirstName,LastName,Email,MainPhoneNumber,SecondaryPhoneNumber,AddressId")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", customer.AddressId);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", customer.AddressId);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,FirstName,LastName,Email,MainPhoneNumber,SecondaryPhoneNumber,AddressId")] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
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
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "AddressId", customer.AddressId);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(c => c.Address)
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
    }
}
