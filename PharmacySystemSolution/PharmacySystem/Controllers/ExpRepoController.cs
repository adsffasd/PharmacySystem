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
    public class ExpRepoController : Controller
    {
        private readonly AppDbContext _context;

        public ExpRepoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ExpRepo
        [Authorize(Roles = "admin,repo")]
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ExpRepos.Include(e => e.Product);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ExpRepo/Details/5
        [Authorize(Roles = "admin,repo")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expRepo = await _context.ExpRepos
                .Include(e => e.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expRepo == null)
            {
                return NotFound();
            }

            return View(expRepo);
        }

        // GET: ExpRepo/Create
        [Authorize(Roles = "admin,repo")]
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // POST: ExpRepo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,repo")]
        public async Task<IActionResult> Create([Bind("Id,Name,Status,Quantity,ProductId")] ExpRepo expRepo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(expRepo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", expRepo.ProductId);
            return View(expRepo);
        }

        // GET: ExpRepo/Edit/5
        [Authorize(Roles = "admin,repo")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expRepo = await _context.ExpRepos.FindAsync(id);
            if (expRepo == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", expRepo.ProductId);
            return View(expRepo);
        }

        // POST: ExpRepo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "admin,repo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Status,Quantity,ProductId")] ExpRepo expRepo)
        {
            if (id != expRepo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(expRepo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpRepoExists(expRepo.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", expRepo.ProductId);
            return View(expRepo);
        }

        // GET: ExpRepo/Delete/5
        [Authorize(Roles = "admin,repo")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expRepo = await _context.ExpRepos
                .Include(e => e.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expRepo == null)
            {
                return NotFound();
            }

            return View(expRepo);
        }

        // POST: ExpRepo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,repo")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expRepo = await _context.ExpRepos.FindAsync(id);
            if (expRepo != null)
            {
                _context.ExpRepos.Remove(expRepo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "admin,repo")]
        private bool ExpRepoExists(int id)
        {
            return _context.ExpRepos.Any(e => e.Id == id);
        }
    }
}
