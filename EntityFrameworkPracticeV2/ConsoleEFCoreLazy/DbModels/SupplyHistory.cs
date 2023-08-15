using System;
using System.ComponentModel.DataAnnotations;

namespace ConsoleEFCoreLazy.DbModels
{
    public class SupplyHistory // No sealed, also for lazy loading.
    {
        public int SupplyHistoryId { get; set; }

        public int ProductId { get; set; } // FK Table Product

        public virtual Product Product { get; set; } // Ref Table Product. Virtual for lazy loading.

        public int CompanyId { get; set; }

        public virtual Company Company { get; set; } // Virtual for lazy loading.

        public DateTime ShipmentDate { get; set; }

        public double Price { get; set; }
    }
}
