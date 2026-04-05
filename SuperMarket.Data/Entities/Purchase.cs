using System;
using System.Collections.Generic;

namespace SuperMarket.Data.Entities;

public partial class Purchase
{
    public int PurchaseId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal PricePerUnit { get; set; }

    public decimal Total { get; set; }

    public int SupplierId { get; set; }

    public DateOnly PurchaseDate { get; set; }

    public bool StokedIn { get; set; }

    public DateOnly ExpDate { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Supplier Supplier { get; set; } = null!;
}
