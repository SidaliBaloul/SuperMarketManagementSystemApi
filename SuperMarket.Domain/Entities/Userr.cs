using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SuperMarket.Domain.Entities;

public partial class Userr
{

    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
    public ICollection<RefreshToken> RefreshTokens { get; set; }
}
