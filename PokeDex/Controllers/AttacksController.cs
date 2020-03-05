using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PokeDex.Models;

namespace PokeDex.Controllers
{
    public class AttacksController : Controller
    {
        private readonly PokeDexContext _context;

        public AttacksController(PokeDexContext context)
        {
            _context = context;
        }

        // GET: Attacks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Attacks.ToListAsync());
        }

        // GET: Attacks/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var attacks = await _context.Attacks
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (attacks == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(attacks);
        //}

        // GET: Attacks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Attacks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Attack")] Attacks attacks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attacks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(attacks);
        }

        // GET: Attacks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attacks = await _context.Attacks.FindAsync(id);
            if (attacks == null)
            {
                return NotFound();
            }
            return View(attacks);
        }

        // POST: Attacks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Attack")] Attacks attacks)
        {
            if (id != attacks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attacks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttacksExists(attacks.Id))
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
            return View(attacks);
        }

        // GET: Attacks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attacks = await _context.Attacks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attacks == null)
            {
                return NotFound();
            }

            return View(attacks);
        }

        // POST: Attacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var attacks = await _context.Attacks.FindAsync(id);
            _context.Attacks.Remove(attacks);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttacksExists(int id)
        {
            return _context.Attacks.Any(e => e.Id == id);
        }
    }
}
