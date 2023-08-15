using System;
using System.Collections.Generic;

namespace ConsoleEFCore.DbModels
{
    public class Company
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal? Revenue { get; set; }

        public DateTime FoundationDate { get; set; }

        public Product Product { get; set; }

        public int? ProductId { get; set; }

        public List<User> Users { get; set; }

        public List<SupplyHistory> SupplyHistory { get; set; }
    }
}
