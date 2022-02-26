using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using movcontainer_crud_app.Data;
using movcontainer_crud_app.Models;

namespace movcontainer_crud_app.Controllers
{
    public class ContainersController : Controller
    {
        private readonly AppContextDb _context;
        public ContainersController(AppContextDb context)
        {
            _context = context;
        }
        //GET ALL
        public async Task<IActionResult> Index()
        {
            return View(await _context.Containers.ToListAsync());
        }
        //GET ID
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
                return NotFound();

            var container = await _context.Containers.FirstOrDefaultAsync(m => m.Id == id);
            if (container == null)
                return NotFound();

            return View(container);
        }
        public IActionResult Create()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClientName,Number,TypeCont,Status,Category")] Container container)
        {
            if(ModelState.IsValid)
            {
                _context.Add(container);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(container);
        }
        //GET
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var container = await _context.Containers.FindAsync(id);
            if (container == null)
                return NotFound();

            return View(container);
        }
        //UPDATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientName,Number,TypeCont,Status,Category")] Container container)
        {
            if(id != container.Id)
                return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(container);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException)
                {
                    if (!ContainerExists(container.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
                
            }
            return View(container);
        }
        //GET
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var container = await _context.Containers.FirstOrDefaultAsync(m => m.Id == id);
            if (container == null)
                return NotFound();

            return View(container);
        }
        //DELETE
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var container = await _context.Containers.FindAsync(id);
            _context.Containers.Remove(container);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool ContainerExists(int id)
        {
            return _context.Containers.Any(e => e.Id == id);
        }
    }
}
