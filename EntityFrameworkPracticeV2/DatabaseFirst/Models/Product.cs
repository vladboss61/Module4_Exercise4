using System;
using System.Collections.Generic;

namespace DatabaseFirst.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Company> Companies { get; } = new List<Company>();

    public virtual ProductVersion ProductVersion { get; set; }

    public virtual ICollection<SupplyHistory> SupplyHistories { get; } = new List<SupplyHistory>();
}
