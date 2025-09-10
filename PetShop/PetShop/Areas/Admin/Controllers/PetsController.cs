using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetShop.Filters;
using PetShop.Models;

namespace PetShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionAuthorize]
    public class PetsController : Controller
    {
        private readonly PetShopContext _context;

        public PetsController(PetShopContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var petShopContext = _context.Pets.Include(p => p.Category);
            return View(await petShopContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var pet = await _context.Pets
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.PetId == id);
            if (pet == null) return NotFound();
            return View(pet);
        }

        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.PetCategories, "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PetName,CategoryId,Age,Gender,Price,Description,ImageUrl,Stock")] Pet pet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.PetCategories, "CategoryId", "CategoryName", pet.CategoryId);
            return View(pet);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var pet = await _context.Pets.FindAsync(id);
            if (pet == null) return NotFound();
            ViewData["CategoryId"] = new SelectList(_context.PetCategories, "CategoryId", "CategoryName", pet.CategoryId);
            return View(pet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PetId,PetName,CategoryId,Age,Gender,Price,Description,ImageUrl,Stock,DateAdded")] Pet pet)
        {
            if (id != pet.PetId) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetExists(pet.PetId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.PetCategories, "CategoryId", "CategoryName", pet.CategoryId);
            return View(pet);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var pet = await _context.Pets
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.PetId == id);
            if (pet == null) return NotFound();
            return View(pet);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pet = await _context.Pets.FindAsync(id);
            if (pet != null)
            {
                _context.Pets.Remove(pet);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PetExists(int id)
        {
            return _context.Pets.Any(e => e.PetId == id);
        }
    }
}