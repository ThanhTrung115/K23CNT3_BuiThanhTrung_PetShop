using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using PetShop.Models;

namespace PetShop.ViewModels
{
    public class PetViewModel
    {
        public Pet Pet { get; set; }

        // Dùng để nhận nhiều file ảnh từ form
        public List<IFormFile> ImageFiles { get; set; }
    }
}