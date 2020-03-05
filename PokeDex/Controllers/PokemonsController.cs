using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PokeDex.Models;

namespace PokeDex.Controllers
{
    public class PokemonsController : Controller
    {
        private readonly PokeDexContext _context;

        public PokemonsController(PokeDexContext context)
        {
            _context = context;
        }

        // GET: Pokemons
        public async Task<IActionResult> Index()
        {
            var pokeDexContext = _context.Pokemons.Include(p => p.AttackId1Navigation).Include(p => p.AttackId2Navigation).Include(p => p.AttackId3Navigation).Include(p => p.AttackId4Navigation).Include(p => p.Region).Include(p => p.TypeId1Navigation).Include(p => p.TypeId2Navigation);
            return View(await pokeDexContext.ToListAsync());
        }

        // GET: Pokemons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokemons = await _context.Pokemons
                .Include(p => p.AttackId1Navigation)
                .Include(p => p.AttackId2Navigation)
                .Include(p => p.AttackId3Navigation)
                .Include(p => p.AttackId4Navigation)
                .Include(p => p.Region)
                .Include(p => p.TypeId1Navigation)
                .Include(p => p.TypeId2Navigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pokemons == null)
            {
                return NotFound();
            }

            return View(pokemons);
        }

        // GET: Pokemons/Create
        public IActionResult Create()
        {
            ViewData["AttackId1"] = new SelectList(_context.Attacks, "Id", "Attack");
            ViewData["AttackId2"] = new SelectList(_context.Attacks, "Id", "Attack");
            ViewData["AttackId3"] = new SelectList(_context.Attacks, "Id", "Attack");
            ViewData["AttackId4"] = new SelectList(_context.Attacks, "Id", "Attack");
            ViewData["RegionId"] = new SelectList(_context.Regions, "Id", "Region");
            ViewData["TypeId1"] = new SelectList(_context.Types, "Id", "Type");
            ViewData["TypeId2"] = new SelectList(_context.Types, "Id", "Type");
            return View();
        }

        // POST: Pokemons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,RegionId,TypeId1,TypeId2,AttackId1,AttackId2,AttackId3,AttackId4")] Pokemons pokemons, IList<IFormFile> Avatar )
        {

            if (ModelState.IsValid)
            {
                IFormFile uploadedImage = Avatar.FirstOrDefault();
                if (uploadedImage.ContentType.ToLower().StartsWith("image/"))
                    // Check whether the selected file is image
                {
                    byte[] b;
                    using (BinaryReader br = new BinaryReader(uploadedImage.OpenReadStream()))
                    {
                        b = br.ReadBytes((int)uploadedImage.OpenReadStream().Length);
                        // Convert the image in to bytes
                        pokemons.Avatar = b;
                    }
                    Response.StatusCode = 200;
                }
               
               // Stream AvatarStream = 
               
        

                _context.Add(pokemons);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }


            ViewData["AttackId1"] = new SelectList(_context.Attacks, "Id", "Attack", pokemons.AttackId1);
            ViewData["AttackId2"] = new SelectList(_context.Attacks, "Id", "Attack", pokemons.AttackId2);
            ViewData["AttackId3"] = new SelectList(_context.Attacks, "Id", "Attack", pokemons.AttackId3);
            ViewData["AttackId4"] = new SelectList(_context.Attacks, "Id", "Attack", pokemons.AttackId4);
            ViewData["RegionId"] = new SelectList(_context.Regions, "Id", "Region", pokemons.RegionId);
            ViewData["TypeId1"] = new SelectList(_context.Types, "Id", "Type", pokemons.TypeId1);
            ViewData["TypeId2"] = new SelectList(_context.Types, "Id", "Type", pokemons.TypeId2);
            return View(pokemons);
        }

        public ActionResult ImageViewer(int id)
        {
            using (var context = new PokeDexContext())
            {
                var Img = (from pokemon in context.Pokemons
                    where pokemon.Id == id
                    select pokemon.Avatar).FirstOrDefault();
                return File(Img,"imagenes/jpg");
            }
        }

        // GET: Pokemons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokemons = await _context.Pokemons.FindAsync(id);
            if (pokemons == null)
            {
                return NotFound();
            }
            ViewData["AttackId1"] = new SelectList(_context.Attacks, "Id", "Attack", pokemons.AttackId1);
            ViewData["AttackId2"] = new SelectList(_context.Attacks, "Id", "Attack", pokemons.AttackId2);
            ViewData["AttackId3"] = new SelectList(_context.Attacks, "Id", "Attack", pokemons.AttackId3);
            ViewData["AttackId4"] = new SelectList(_context.Attacks, "Id", "Attack", pokemons.AttackId4);
            ViewData["RegionId"] = new SelectList(_context.Regions, "Id", "Region", pokemons.RegionId);
            ViewData["TypeId1"] = new SelectList(_context.Types, "Id", "Type", pokemons.TypeId1);
            ViewData["TypeId2"] = new SelectList(_context.Types, "Id", "Type", pokemons.TypeId2);
            ViewBag.Avatar = ImageViewer(pokemons.Id);
            return View(pokemons);
        }

        // POST: Pokemons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,RegionId,TypeId1,TypeId2,AttackId1,AttackId2,AttackId3,AttackId4")] Pokemons pokemons, IList<IFormFile> Avatar)
        {
            if (id != pokemons.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   if( Avatar.Count != 0) 
                   { 
                        IFormFile uploadedImage = Avatar.FirstOrDefault();
                        if (uploadedImage.ContentType.ToLower().StartsWith("image/") )
                        // Check whether the selected file is image
                        {
                            byte[] b;
                            using (BinaryReader br = new BinaryReader(uploadedImage.OpenReadStream()))
                            {
                                b = br.ReadBytes((int)uploadedImage.OpenReadStream().Length);
                                // Convert the image in to bytes
                                pokemons.Avatar = b;
                            }
                            Response.StatusCode = 200;
                        }
                   }
                   else
                   {
                       using (PokeDexContext db = new PokeDexContext() )
                       {
                           var PokeBall = db.Pokemons.Find(id);
                           pokemons.Avatar = PokeBall.Avatar;
                       }

                   }
                    _context.Update(pokemons);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PokemonsExists(pokemons.Id))
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
            ViewData["AttackId1"] = new SelectList(_context.Attacks, "Id", "Attack", pokemons.AttackId1);
            ViewData["AttackId2"] = new SelectList(_context.Attacks, "Id", "Attack", pokemons.AttackId2);
            ViewData["AttackId3"] = new SelectList(_context.Attacks, "Id", "Attack", pokemons.AttackId3);
            ViewData["AttackId4"] = new SelectList(_context.Attacks, "Id", "Attack", pokemons.AttackId4);
            ViewData["RegionId"] = new SelectList(_context.Regions, "Id", "Region", pokemons.RegionId);
            ViewData["TypeId1"] = new SelectList(_context.Types, "Id", "Type", pokemons.TypeId1);
            ViewData["TypeId2"] = new SelectList(_context.Types, "Id", "Type", pokemons.TypeId2);
            return View(pokemons);
        }

        // GET: Pokemons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokemons = await _context.Pokemons
                .Include(p => p.AttackId1Navigation)
                .Include(p => p.AttackId2Navigation)
                .Include(p => p.AttackId3Navigation)
                .Include(p => p.AttackId4Navigation)
                .Include(p => p.Region)
                .Include(p => p.TypeId1Navigation)
                .Include(p => p.TypeId2Navigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pokemons == null)
            {
                return NotFound();
            }

            return View(pokemons);
        }

        // POST: Pokemons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pokemons = await _context.Pokemons.FindAsync(id);
            _context.Pokemons.Remove(pokemons);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PokemonsExists(int id)
        {
            return _context.Pokemons.Any(e => e.Id == id);
        }
    }
}
