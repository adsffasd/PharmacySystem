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
    public class ImpRepoController : Controller
    {
        private readonly AppDbContext _context;

        public ImpRepoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ImpRepo
        [Authorize(Roles = "admin,repo")]
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ImpRepos.Include(i => i.Supplier);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ImpRepo/Details/5
        [Authorize(Roles = "admin,repo")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var impRepo = await _context.ImpRepos
                .Include(i => i.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (impRepo == null)
            {
                return NotFound();
            }

            return View(impRepo);
        }
        [Authorize(Roles = "admin,repo")]

        // GET: ImpRepo/Create
        public IActionResult Create()
        {
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name");
            return View();
        }
        [Authorize(Roles = "admin,repo")]

        // POST: ImpRepo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrderDate,TotalAmount,Status,Price,SupplierId")] ImpRepo impRepo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(impRepo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name", impRepo.SupplierId);
            return View(impRepo);
        }
        [Authorize(Roles = "admin,repo")]

        // GET: ImpRepo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var impRepo = await _context.ImpRepos.FindAsync(id);
            if (impRepo == null)
            {
                return NotFound();
            }
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name", impRepo.SupplierId);
            return View(impRepo);
        }
        [Authorize(Roles = "admin,repo")]

        // POST: ImpRepo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrderDate,TotalAmount,Status,Price,SupplierId")] ImpRepo impRepo)
        {
            if (id != impRepo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(impRepo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImpRepoExists(impRepo.Id))
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
            ViewData["SupplierId"] = new SelectList(_context.Suppliers, "Id", "Name", impRepo.SupplierId);
            return View(impRepo);
        }

        // GET: ImpRepo/Delete/5
        [Authorize(Roles = "admin,repo")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var impRepo = await _context.ImpRepos
                .Include(i => i.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (impRepo == null)
            {
                return NotFound();
            }

            return View(impRepo);
        }

        // POST: ImpRepo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,repo")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var impRepo = await _context.ImpRepos.FindAsync(id);
            if (impRepo != null)
            {
                _context.ImpRepos.Remove(impRepo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "admin,repo")]
        private bool ImpRepoExists(int id)
        {
            return _context.ImpRepos.Any(e => e.Id == id);
        }
    }
}
