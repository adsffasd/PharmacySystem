using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PharmacySystem.Data;
using PharmacySystem.Models;

namespace PharmacySystem.Controllers
{
    public class BillController : Controller
    {
        private readonly AppDbContext _context;

        public BillController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Bill
        [Authorize(Roles = "admin,pharmacist")]
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Bills.Include(b => b.Account).Include(b => b.Customer).Include(b => b.Product);
            return View(await appDbContext.ToListAsync());
        }
        [Authorize(Roles = "admin,pharmacist")]
        // GET: Bill/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills
                .Include(b => b.Account)
                .Include(b => b.Customer)
                .Include(b => b.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }
        [Authorize(Roles = "admin,pharmacist")]
        // GET: Bill/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Name");
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }
        [Authorize(Roles = "admin,pharmacist")]
        // POST: Bill/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Amount,ProductId,CustomerId,AccountId")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Name", bill.AccountId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", bill.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", bill.ProductId);
            return View(bill);
        }
        [Authorize(Roles = "admin,pharmacist")]
        // GET: Bill/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills.FindAsync(id);
            if (bill == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Name", bill.AccountId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", bill.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", bill.ProductId);
            return View(bill);
        }

        // POST: Bill/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,pharmacist")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Amount,ProductId,CustomerId,AccountId")] Bill bill)
        {
            if (id != bill.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillExists(bill.Id))
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
            ViewData["AccountId"] = new SelectList(_context.Accounts, "Id", "Name", bill.AccountId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Name", bill.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", bill.ProductId);
            return View(bill);
        }
        [Authorize(Roles = "admin,pharmacist")]
        // GET: Bill/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills
                .Include(b => b.Account)
                .Include(b => b.Customer)
                .Include(b => b.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }
        [Authorize(Roles = "admin,pharmacist")]
        // POST: Bill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bill = await _context.Bills.FindAsync(id);
            if (bill != null)
            {
                _context.Bills.Remove(bill);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillExists(int id)
        {
            return _context.Bills.Any(e => e.Id == id);
        }
    }
}
