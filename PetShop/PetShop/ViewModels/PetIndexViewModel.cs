using System.Collections.Generic;
using PetShop.Models;

namespace PetShop.ViewModels
{
    public class PetIndexViewModel
    {
        // Danh sách thú cưng đã được lọc để hiển thị
        public List<Pet> Pets { get; set; }

        // Danh sách các danh mục cha (Chó, Mèo, Cá...) để tạo bộ lọc
        public List<PetCategory> ParentCategories { get; set; }

        // Tên của danh mục đang được chọn để làm nổi bật trên giao diện
        public string? ActiveCategoryName { get; set; }
    }
}