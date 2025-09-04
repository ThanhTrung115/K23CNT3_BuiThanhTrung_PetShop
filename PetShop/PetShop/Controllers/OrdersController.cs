using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PetShop.Models;
using PetShop.ViewModels;

public class OrdersController : Controller
{
    private readonly PetShopContext _context;
    public const string CARTKEY = "cart";

    public OrdersController(PetShopContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (!userId.HasValue)
        {
            return RedirectToAction("Login", "Account");
        }

        var orders = await _context.Orders
                                   .Where(o => o.UserId == userId.Value)
                                   .OrderByDescending(o => o.OrderDate)
                                   .ToListAsync();

        return View(orders);
    }

    // GET: /Orders/Create
    public IActionResult Create()
    {
        var session = HttpContext.Session;
        var jsoncart = session.GetString(CARTKEY);
        var cart = JsonConvert.DeserializeObject<List<CartItemViewModel>>(jsoncart);

        if (cart == null || cart.Count == 0)
        {
            return RedirectToAction("Index", "Carts");
        }

        var userId = HttpContext.Session.GetInt32("UserId");
        if (!userId.HasValue)
        {
            // Nếu chưa đăng nhập, chuyển đến trang đăng nhập
            return RedirectToAction("Login", "Account");
        }

        var order = new Order
        {
            UserId = userId.Value,
            OrderDate = DateTime.Now,
            TotalAmount = cart.Sum(item => item.Total),
            Status = "Đang xử lý"
        };

        // Mặc định lấy địa chỉ của người dùng, hoặc hiển thị form cho người dùng nhập
        var user = _context.Users.Find(userId.Value);
        order.ShippingAddress = user.Address ?? "Chưa có địa chỉ";

        _context.Orders.Add(order);
        _context.SaveChanges();

        foreach (var item in cart)
        {
            var orderDetail = new OrderDetail
            {
                OrderId = order.OrderId,
                PetId = item.PetId,
                Quantity = item.Quantity,
                UnitPrice = item.Price
            };
            _context.OrderDetails.Add(orderDetail);
        }

        _context.SaveChanges();
        session.Remove(CARTKEY); // Xóa giỏ hàng sau khi đặt hàng

        return View("OrderSuccess");
    }

    // Action để hiển thị trang đặt hàng thành công
    public IActionResult OrderSuccess()
    {
        return View();
    }
}