using System;
using System.Collections.Generic;

namespace SuperMarket.Data.Entities;

public partial class Product
{
    public int ProductId { get; set; }

    public string BarCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

    public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
