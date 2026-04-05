using System;
using System.Collections.Generic;

namespace SuperMarket.Data.Entities;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Phone { get; set; }

    public string Email { get; set; }

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
}
