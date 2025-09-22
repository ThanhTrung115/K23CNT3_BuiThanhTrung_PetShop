using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetShop.Models
{
    public class PetImage
    {
        [Key]
        public int ImageId { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public int PetId { get; set; }

        [ForeignKey("PetId")]
        public virtual Pet Pet { get; set; }
    }
}