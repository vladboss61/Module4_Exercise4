using System;
using System.Collections.Generic;
using System.Text;
using ConsoleEFCore.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ConsoleEFCore.DbModels
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Company> Companies { get; set; } //navigation property.

        public List<SupplyHistory> SupplyHistory { get; set; } //navigation property.

        public ProductVersion ProductVersion { get; set; } //navigation property.
    }
}
