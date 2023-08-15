using System;
using System.ComponentModel.DataAnnotations;

namespace ConsoleEFCore.DbModels
{
    public sealed class SupplyHistory
    {
        public int Id { get; set; }

        public int ProductId { get; set; } // FK Table Product

        public Product Product { get; set; } // Ref Table Product

        public int CompanyId { get; set; }

        public Company Company { get; set; }

        public DateTime ShipmentDate { get; set; }

        public double Price { get; set; }
    }
}
