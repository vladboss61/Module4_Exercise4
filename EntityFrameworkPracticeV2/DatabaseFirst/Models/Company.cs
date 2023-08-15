using System;
using System.Collections.Generic;

namespace DatabaseFirst.Models;

public partial class Company
{
    public int CompanyId { get; set; }

    public string Name { get; set; }

    public decimal? Revenue { get; set; }

    public DateTime FoundationDate { get; set; }

    public int? ProductId { get; set; }

    public virtual Product Product { get; set; }

    public virtual ICollection<SupplyHistory> SupplyHistories { get; } = new List<SupplyHistory>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
