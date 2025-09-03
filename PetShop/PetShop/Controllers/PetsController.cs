using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShop.Models;

public class PetsController : Controller
{
    private readonly PetShopContext _context;

    public PetsController(PetShopContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(int? categoryId)
    {
        ViewData["Categories"] = await _context.PetCategories.ToListAsync();
        ViewData["SelectedCategoryId"] = categoryId;

        var petsQuery = _context.Pets.Include(p => p.Category).AsQueryable();

        if (categoryId.HasValue)
        {
            petsQuery = petsQuery.Where(p => p.CategoryId == categoryId.Value);
            var categoryName = await _context.PetCategories
                                            .Where(c => c.CategoryId == categoryId.Value)
                                            .Select(c => c.CategoryName)
                                            .FirstOrDefaultAsync();
            ViewData["PageTitle"] = $"Thú cưng: {categoryName}";
        }
        else
        {
            ViewData["PageTitle"] = "Tất cả Thú cưng";
        }

        var pets = await petsQuery.ToListAsync();
        return View(pets);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pet = await _context.Pets
            .Include(p => p.Category)
            .FirstOrDefaultAsync(m => m.PetId == id);

        if (pet == null)
        {
            return NotFound();
        }

        return View(pet);
    }
}