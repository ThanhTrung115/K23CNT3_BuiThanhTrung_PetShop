using System;
using System.Collections.Generic;

namespace PetShop.Models;

public partial class PetCategory
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Pet> Pets { get; set; } = new List<Pet>();
}
