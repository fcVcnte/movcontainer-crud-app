#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using movcontainer_crud_app.Data;
using movcontainer_crud_app.Models;

namespace movcontainer_crud_app.Controllers
{
    public class MovementsController : Controller
    {
        private readonly AppContextDb _context;

        public MovementsController(AppContextDb context)
        {
            _context = context;
        }

        // GET: Movements
        public async Task<IActionResult> Index()
        {
            var appContextDb = _context.Movements.Include(m => m.Container);
            return View(await appContextDb.ToListAsync());
        }

        // GET: Movements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movement = await _context.Movements
                .Include(m => m.Container)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movement == null)
            {
                return NotFound();
            }

            return View(movement);
        }

        // GET: Movements/Create
        public IActionResult Create()
        {
            ViewData["ContainerId"] = new SelectList(_context.Containers, "Id", "ClientName");
            return View();
        }

        // POST: Movements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TypeMov,StartDate,EndDate,ContainerId")] Movement movement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ContainerId"] = new SelectList(_context.Containers, "Id", "ClientName", movement.ContainerId);
            return View(movement);
        }

        // GET: Movements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movement = await _context.Movements.FindAsync(id);
            if (movement == null)
            {
                return NotFound();
            }
            ViewData["ContainerId"] = new SelectList(_context.Containers, "Id", "ClientName", movement.ContainerId);
            return View(movement);
        }

        // POST: Movements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TypeMov,StartDate,EndDate,ContainerId")] Movement movement)
        {
            if (id != movement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovementExists(movement.Id))
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
            ViewData["ContainerId"] = new SelectList(_context.Containers, "Id", "ClientName", movement.ContainerId);
            return View(movement);
        }

        // GET: Movements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movement = await _context.Movements
                .Include(m => m.Container)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movement == null)
            {
                return NotFound();
            }

            return View(movement);
        }

        // POST: Movements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movement = await _context.Movements.FindAsync(id);
            _context.Movements.Remove(movement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovementExists(int id)
        {
            return _context.Movements.Any(e => e.Id == id);
        }
    }
}
