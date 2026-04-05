using System;
using System.Collections.Generic;

namespace SuperMarket.Data.Entities;

public partial class SaleDetail
{
    public int SaleDetailId { get; set; }

    public int SaleId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public virtual Sale Sale { get; set; }

    public virtual Product Product { get; set; }
}
