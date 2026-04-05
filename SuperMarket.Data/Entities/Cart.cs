using System;
using System.Collections.Generic;

namespace SuperMarket.Data.Entities;

public partial class Cart
{
    public int No { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal Total { get; set; }
}
