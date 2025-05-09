using System;
using System.Collections.Generic;

namespace E_Ticaret.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public string? Description { get; set; }

    public double? Price { get; set; }

    public int CategoryId { get; set; }

    public string? Image { get; set; }

    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
