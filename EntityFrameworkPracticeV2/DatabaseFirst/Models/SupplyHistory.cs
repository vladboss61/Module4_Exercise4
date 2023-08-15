using System;
using System.Collections.Generic;

namespace DatabaseFirst.Models;

public partial class SupplyHistory
{
    public int SupplyHistoryId { get; set; }

    public int ProductId { get; set; }

    public int CompanyId { get; set; }

    public DateTime ShipmentDate { get; set; }

    public double Price { get; set; }

    public virtual Company Company { get; set; }

    public virtual Product Product { get; set; }
}
