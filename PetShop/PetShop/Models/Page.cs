using System;
using System.Collections.Generic;

namespace PetShop.Models;

public partial class Page
{
    public int PageId { get; set; }

    public string Title { get; set; } = null!;

    public string Alias { get; set; } = null!;

    public string? Content { get; set; }

    public bool IsPublished { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime? DateModified { get; set; }
}
