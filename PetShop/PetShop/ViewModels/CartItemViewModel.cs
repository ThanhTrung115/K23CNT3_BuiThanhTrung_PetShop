using PetShop.Models;

namespace PetShop.ViewModels
{
    public class CartItemViewModel
    {
        public int PetId { get; set; }
        public string PetName { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total => Price * Quantity;
    }
}