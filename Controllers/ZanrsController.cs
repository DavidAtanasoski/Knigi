using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Knigi.Data;
using Knigi.Models;

namespace Knigi.Controllers
{
    public class ZanrsController : Controller
    {
        private readonly KnigiContext _context;

        public ZanrsController(KnigiContext context)
        {
            _context = context;
        }

        // GET: Zanrs
        public async Task<IActionResult> Index()
        {
              return _context.Zanr != null ? 
                          View(await _context.Zanr.ToListAsync()) :
                          Problem("Entity set 'KnigiContext.Zanr'  is null.");
        }

        // GET: Zanrs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Zanr == null)
            {
                return NotFound();
            }

            var zanr = await _context.Zanr
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zanr == null)
            {
                return NotFound();
            }

            return View(zanr);
        }

        // GET: Zanrs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Zanrs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ime")] Zanr zanr)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zanr);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(zanr);
        }

        // GET: Zanrs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Zanr == null)
            {
                return NotFound();
            }

            var zanr = await _context.Zanr.FindAsync(id);
            if (zanr == null)
            {
                return NotFound();
            }
            return View(zanr);
        }

        // POST: Zanrs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ime")] Zanr zanr)
        {
            if (id != zanr.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zanr);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZanrExists(zanr.Id))
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
            return View(zanr);
        }

        // GET: Zanrs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Zanr == null)
            {
                return NotFound();
            }

            var zanr = await _context.Zanr
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zanr == null)
            {
                return NotFound();
            }

            return View(zanr);
        }

        // POST: Zanrs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Zanr == null)
            {
                return Problem("Entity set 'KnigiContext.Zanr'  is null.");
            }
            var zanr = await _context.Zanr.FindAsync(id);
            if (zanr != null)
            {
                _context.Zanr.Remove(zanr);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZanrExists(int id)
        {
          return (_context.Zanr?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
