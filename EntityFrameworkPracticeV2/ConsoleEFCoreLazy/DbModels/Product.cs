using System;
using System.Collections.Generic;
using System.Text;
using ConsoleEFCoreLazy.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ConsoleEFCoreLazy.DbModels
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<Company> Companies { get; set; }

        public virtual List<SupplyHistory> SupplyHistory { get; set; }

        public virtual ProductVersion ProductVersion { get; set; }
    }
}
