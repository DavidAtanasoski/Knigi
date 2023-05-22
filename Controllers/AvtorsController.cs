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
    public class AvtorsController : Controller
    {
        private readonly KnigiContext _context;

        public AvtorsController(KnigiContext context)
        {
            _context = context;
        }

        // GET: Avtors
        public async Task<IActionResult> Index()
        {
              return _context.Avtor != null ? 
                          View(await _context.Avtor.ToListAsync()) :
                          Problem("Entity set 'KnigiContext.Avtor'  is null.");
        }

        // GET: Avtors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Avtor == null)
            {
                return NotFound();
            }

            var avtor = await _context.Avtor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (avtor == null)
            {
                return NotFound();
            }

            return View(avtor);
        }

        // GET: Avtors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Avtors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ime,Prezime,Nacionalnost,DatumRagjanje")] Avtor avtor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(avtor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(avtor);
        }

        // GET: Avtors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Avtor == null)
            {
                return NotFound();
            }

            var avtor = await _context.Avtor.FindAsync(id);
            if (avtor == null)
            {
                return NotFound();
            }
            return View(avtor);
        }

        // POST: Avtors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ime,Prezime,Nacionalnost,DatumRagjanje")] Avtor avtor)
        {
            if (id != avtor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(avtor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AvtorExists(avtor.Id))
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
            return View(avtor);
        }

        // GET: Avtors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Avtor == null)
            {
                return NotFound();
            }

            var avtor = await _context.Avtor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (avtor == null)
            {
                return NotFound();
            }

            return View(avtor);
        }

        // POST: Avtors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Avtor == null)
            {
                return Problem("Entity set 'KnigiContext.Avtor'  is null.");
            }
            var avtor = await _context.Avtor.FindAsync(id);
            if (avtor != null)
            {
                _context.Avtor.Remove(avtor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AvtorExists(int id)
        {
          return (_context.Avtor?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
