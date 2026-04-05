using System;
using System.Collections.Generic;

namespace SuperMarket.Data.Entities;

public partial class Sale
{
    public int SaleId { get; set; }

    public decimal Amount { get; set; }

    public int UserId { get; set; }

    public DateTime Date { get; set; }

    public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();

    public virtual Userr User { get; set; } = null!;
}
