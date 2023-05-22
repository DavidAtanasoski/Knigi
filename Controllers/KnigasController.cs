using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Knigi.Data;
using Knigi.Models;
using Knigi.ViewModels;

namespace Knigi.Controllers
{
    public class KnigasController : Controller
    {
        private readonly KnigiContext _context;

        public KnigasController(KnigiContext context)
        {
            _context = context;
        }

        // GET: Knigas
        public async Task<IActionResult> Index(string searchNaslov, string searchAvtor, string searchGodina, string knigaZanr)
        {
            IQueryable<Kniga> knigi = _context.Kniga.AsQueryable();
            IQueryable<string> zanrQuery = _context.Zanr.Select(x => x.Ime);


            if(!string.IsNullOrEmpty(searchNaslov))
            {
                knigi = knigi.Where(x => x.Naslov.Contains(searchNaslov));
            }

            if(!string.IsNullOrEmpty(searchAvtor))
            {
                knigi = knigi.Where(x => x.Avtor.Ime.Contains(searchAvtor) || x.Avtor.Prezime.Contains(searchAvtor));
            }

            if(!string.IsNullOrEmpty(searchGodina))
            {
                knigi = knigi.Where(x => x.Godina.ToString() == searchGodina);
            }

            if(!string.IsNullOrEmpty(knigaZanr))
            {
                knigi = knigi.Where(x => x.Zanrovi.Any(x => x.Zanr.Ime == knigaZanr));
            }

            knigi = knigi.Include(x => x.Avtor).Include(x => x.Zanrovi!).ThenInclude(x => x.Zanr);

            var knigaFilterVM = new KnigaFilterViewModel
            {
                Knigi = await knigi.ToListAsync(),
                Zanrovi = new SelectList(await zanrQuery.ToListAsync())
            };

            return View(knigaFilterVM);
        }

        // GET: Knigas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Kniga == null)
            {
                return NotFound();
            }

            var kniga = await _context.Kniga
                .Include(k => k.Avtor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kniga == null)
            {
                return NotFound();
            }

            return View(kniga);
        }

        // GET: Knigas/Create
        public IActionResult Create()
        {
            var zanrovi = _context.Zanr.ToList();

            var viewModel = new KnigaCreateViewModel
            { 
                Kniga = new Kniga(),
                ZanroviLista = zanrovi.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Ime
                })
            };

            ViewData["AvtorId"] = new SelectList(_context.Avtor, "Id", "FullName");
            return View(viewModel);
        }

        // POST: Knigas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KnigaCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(viewModel.Kniga);
                await _context.SaveChangesAsync();

                if (viewModel.SelectedZanrovi != null)
                {
                    foreach (var zanrId in viewModel.SelectedZanrovi)
                    {
                        var knigaZanr = new KnigaZanr
                        {
                            KnigaId = viewModel.Kniga.Id,
                            ZanrId = zanrId
                        };
                        _context.Add(knigaZanr);
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AvtorId"] = new SelectList(_context.Avtor, "Id", "FullName", viewModel.Kniga.AvtorId);
            viewModel.ZanroviLista = _context.Zanr.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Ime
            });
            return View(viewModel.Kniga);
        }

        // GET: Knigas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Kniga == null)
            {
                return NotFound();
            }

            var kniga = _context.Kniga.Where(x => x.Id == id).Include(x => x.Zanrovi).First();
            if (kniga == null)
            {
                return NotFound();
            }

            var zanrovi = _context.Zanr.AsEnumerable();

            var knigaEditVM = new KnigaEditViewModel
            {
                Kniga = kniga,
                ZanrLista = new MultiSelectList(zanrovi, "Id", "Ime"),
                SelektiraniZanrovi = kniga.Zanrovi.Select(x => x.ZanrId)
            };

            ViewData["AvtorId"] = new SelectList(_context.Avtor, "Id", "FullName", kniga.AvtorId);
            return View(knigaEditVM);
        }

        // POST: Knigas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, KnigaEditViewModel viewModel)
        {
            if (id != viewModel.Kniga.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viewModel.Kniga);
                    await _context.SaveChangesAsync();

                    IEnumerable<int> novaZanrLista = viewModel.SelektiraniZanrovi!;
                    IEnumerable<int> prethodnaZanrLista = _context.KnigaZanr.Where(x => x.KnigaId == id).Select(x => x.ZanrId);
                    IQueryable<KnigaZanr> toBeRemoved = _context.KnigaZanr.Where(x => x.KnigaId == id);

                    if(novaZanrLista != null)
                    {
                        toBeRemoved = toBeRemoved.Where(x => novaZanrLista.Contains(x.ZanrId));
                        foreach (int zanrId in novaZanrLista)
                        {
                            if(!prethodnaZanrLista.Any(x => x == zanrId))
                            {
                                _context.KnigaZanr.Add(new KnigaZanr { ZanrId = zanrId, KnigaId = id });
                            }
                        }
                    }

                    _context.KnigaZanr.RemoveRange(toBeRemoved);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KnigaExists(viewModel.Kniga.Id))
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
            ViewData["AvtorId"] = new SelectList(_context.Avtor, "Id", "FullName", viewModel.Kniga.AvtorId);
            return View(viewModel.Kniga);
        }

        // GET: Knigas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Kniga == null)
            {
                return NotFound();
            }

            var kniga = await _context.Kniga
                .Include(k => k.Avtor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kniga == null)
            {
                return NotFound();
            }

            return View(kniga);
        }

        // POST: Knigas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Kniga == null)
            {
                return Problem("Entity set 'KnigiContext.Kniga'  is null.");
            }
            var kniga = await _context.Kniga.FindAsync(id);
            if (kniga != null)
            {
                _context.Kniga.Remove(kniga);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KnigaExists(int id)
        {
          return (_context.Kniga?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
