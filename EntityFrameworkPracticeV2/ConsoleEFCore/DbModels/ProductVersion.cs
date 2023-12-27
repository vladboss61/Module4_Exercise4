using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEFCore.DbModels
{
    public sealed class ProductVersion
    {
        public int Id { get; set; }

        public int Name { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; } //navigation property. Here, Product is a navigation property that represents the relationship between the SupplyHistory entity and the Product
    }
}
