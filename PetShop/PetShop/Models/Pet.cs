using System;
using System.Collections.Generic;

namespace PetShop.Models;

public partial class Pet
{
    public int PetId { get; set; }

    public string PetName { get; set; } = null!;

    public int? CategoryId { get; set; }

    public int? Age { get; set; }

    public string? Gender { get; set; }

    public decimal Price { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public int Stock { get; set; }

    public DateTime DateAdded { get; set; }

    public virtual PetCategory? Category { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
