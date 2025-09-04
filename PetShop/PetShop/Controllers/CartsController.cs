using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PetShop.Models;
using PetShop.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CartsController : Controller
{
    private readonly PetShopContext _context;
    public const string CARTKEY = "cart";

    public CartsController(PetShopContext context)
    {
        _context = context;
    }

    private List<CartItemViewModel> GetCartItems()
    {
        var session = HttpContext.Session;
        string jsoncart = session.GetString(CARTKEY);
        if (jsoncart != null)
        {
            return JsonConvert.DeserializeObject<List<CartItemViewModel>>(jsoncart);
        }
        return new List<CartItemViewModel>();
    }

    private void SaveCartSession(List<CartItemViewModel> ls)
    {
        var session = HttpContext.Session;
        string jsoncart = JsonConvert.SerializeObject(ls);
        session.SetString(CARTKEY, jsoncart);
    }

    public IActionResult Index()
    {
        return View(GetCartItems());
    }

    [HttpPost]
    public async Task<IActionResult> AddToCartApi(int petId)
    {
        var cart = GetCartItems();
        var item = cart.Find(p => p.PetId == petId);

        if (item != null)
        {
            item.Quantity++;
        }
        else
        {
            var pet = await _context.Pets.FindAsync(petId);
            if (pet == null)
            {
                return Json(new { success = false, message = "Sản phẩm không tồn tại!" });
            }
            cart.Add(new CartItemViewModel()
            {
                PetId = pet.PetId,
                PetName = pet.PetName,
                Price = pet.Price,
                ImageUrl = pet.ImageUrl,
                Quantity = 1
            });
        }

        SaveCartSession(cart);
        return Json(new { success = true, cartItemCount = cart.Count });
    }

    [HttpPost]
    public IActionResult RemoveFromCart(int petId)
    {
        var cart = GetCartItems();
        var item = cart.Find(p => p.PetId == petId);

        if (item != null)
        {
            cart.Remove(item);
        }

        SaveCartSession(cart);
        return RedirectToAction("Index");
    }
}