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
    public class WorkDayController : Controller
    {
        private readonly AppDbContext _context;

        public WorkDayController(AppDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "admin")]
        // GET: WorkDay
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.WorkDays.Include(w => w.Shift);
            return View(await appDbContext.ToListAsync());
        }
        [Authorize(Roles = "admin")]
        // GET: WorkDay/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workDay = await _context.WorkDays
                .Include(w => w.Shift)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workDay == null)
            {
                return NotFound();
            }

            return View(workDay);
        }
        [Authorize(Roles = "admin")]
        // GET: WorkDay/Create
        public IActionResult Create()
        {
            ViewData["ShiftId"] = new SelectList(_context.Shifts, "Id", "Name");
            return View();
        }

        // POST: WorkDay/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([Bind("Id,Day,ShiftId")] WorkDay workDay)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workDay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShiftId"] = new SelectList(_context.Shifts, "Id", "Name", workDay.ShiftId);
            return View(workDay);
        }
        [Authorize(Roles = "admin")]
        // GET: WorkDay/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workDay = await _context.WorkDays.FindAsync(id);
            if (workDay == null)
            {
                return NotFound();
            }
            ViewData["ShiftId"] = new SelectList(_context.Shifts, "Id", "Name", workDay.ShiftId);
            return View(workDay);
        }
        [Authorize(Roles = "admin")]
        // POST: WorkDay/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Day,ShiftId")] WorkDay workDay)
        {
            if (id != workDay.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workDay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkDayExists(workDay.Id))
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
            ViewData["ShiftId"] = new SelectList(_context.Shifts, "Id", "Name", workDay.ShiftId);
            return View(workDay);
        }
        [Authorize(Roles = "admin")]
        // GET: WorkDay/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workDay = await _context.WorkDays
                .Include(w => w.Shift)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workDay == null)
            {
                return NotFound();
            }

            return View(workDay);
        }

        // POST: WorkDay/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workDay = await _context.WorkDays.FindAsync(id);
            if (workDay != null)
            {
                _context.WorkDays.Remove(workDay);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "admin")]
        private bool WorkDayExists(int id)
        {
            return _context.WorkDays.Any(e => e.Id == id);
        }
    }
}
