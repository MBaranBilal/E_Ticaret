using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E_Ticaret.Models;

public partial class Product
{
    public int ProductId { get; set; }

    [Required]
    public string ProductName { get; set; }

    public string? Description { get; set; }

    [Required]
    public double? Price { get; set; }

    [Required]
    public int CategoryId { get; set; }

    public string? Image { get; set; }

    public virtual ICollection<Card> Cards { get; set; } = new List<Card>();

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
