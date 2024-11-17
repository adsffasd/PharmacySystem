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
    public class ImpRepoDetailController : Controller
    {
        private readonly AppDbContext _context;

        public ImpRepoDetailController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "admin,repo")]
        // GET: ImpRepoDetail
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.impRepoDetails.Include(i => i.ImpRepo).Include(i => i.Product);
            return View(await appDbContext.ToListAsync());
        }

        [Authorize(Roles = "admin,repo")]
        // GET: ImpRepoDetail/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var impRepoDetail = await _context.impRepoDetails
                .Include(i => i.ImpRepo)
                .Include(i => i.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (impRepoDetail == null)
            {
                return NotFound();
            }

            return View(impRepoDetail);
        }

        [Authorize(Roles = "admin,repo")]
        // GET: ImpRepoDetail/Create
        public IActionResult Create()
        {
            ViewData["ImpRepoId"] = new SelectList(_context.ImpRepos, "Id", "OrderDate");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        [Authorize(Roles = "admin,repo")]
        // POST: ImpRepoDetail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,ImpRepoId")] ImpRepoDetail impRepoDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(impRepoDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ImpRepoId"] = new SelectList(_context.ImpRepos, "Id", "OrderDateId", impRepoDetail.ImpRepoId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", impRepoDetail.ProductId);
            return View(impRepoDetail);
        }

        [Authorize(Roles = "admin,repo")]
        // GET: ImpRepoDetail/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var impRepoDetail = await _context.impRepoDetails.FindAsync(id);
            if (impRepoDetail == null)
            {
                return NotFound();
            }
            ViewData["ImpRepoId"] = new SelectList(_context.ImpRepos, "Id", "OrderDate", impRepoDetail.ImpRepoId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", impRepoDetail.ProductId);
            return View(impRepoDetail);
        }

        // POST: ImpRepoDetail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,repo")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,ImpRepoId")] ImpRepoDetail impRepoDetail)
        {
            if (id != impRepoDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(impRepoDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImpRepoDetailExists(impRepoDetail.Id))
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
            ViewData["ImpRepoId"] = new SelectList(_context.ImpRepos, "Id", "OrderDate", impRepoDetail.ImpRepoId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", impRepoDetail.ProductId);
            return View(impRepoDetail);
        }

        // GET: ImpRepoDetail/Delete/5
        [Authorize(Roles = "admin,repo")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var impRepoDetail = await _context.impRepoDetails
                .Include(i => i.ImpRepo)
                .Include(i => i.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (impRepoDetail == null)
            {
                return NotFound();
            }

            return View(impRepoDetail);
        }

        // POST: ImpRepoDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin,repo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var impRepoDetail = await _context.impRepoDetails.FindAsync(id);
            if (impRepoDetail != null)
            {
                _context.impRepoDetails.Remove(impRepoDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "admin,repo")]
        private bool ImpRepoDetailExists(int id)
        {
            return _context.impRepoDetails.Any(e => e.Id == id);
        }
    }
}
