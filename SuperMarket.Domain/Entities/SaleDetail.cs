using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SuperMarket.Domain.Entities;

public partial class SaleDetail
{
    [Key]
    public int SaleDetailId { get; set; }

    public int SaleId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public virtual Sale Sale { get; set; }

    public virtual Product Product { get; set; }
}
