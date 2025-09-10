using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShop.Filters;
using PetShop.Models;
using PetShop.ViewModels;

namespace PetShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionAuthorize]
    public class DashboardController : Controller
    {
        private readonly PetShopContext _context;

        public DashboardController(PetShopContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new AdminDashboardViewModel
            {
                UserCount = await _context.Users.CountAsync(),
                OrderCount = await _context.Orders.CountAsync(),
                PetCount = await _context.Pets.CountAsync(),
                TotalRevenue = await _context.Orders
                                            .Where(o => o.Status == "Hoàn thành")
                                            .SumAsync(o => o.TotalAmount)
            };
            return View(viewModel);
        }
    }
}