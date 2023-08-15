using System;
using System.Collections.Generic;

namespace ConsoleEFCoreLazy.DbModels
{
    public class Company
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal? Revenue { get; set; }

        public DateTime FoundationDate { get; set; }

        public virtual List<SupplyHistory> SupplyHistory { get; set; }
    }
}
