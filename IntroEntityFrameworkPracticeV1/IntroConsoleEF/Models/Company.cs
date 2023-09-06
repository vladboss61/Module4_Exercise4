using System;
using System.Collections.Generic;

namespace IntroConsoleEF.Models
{
    public class Company
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal? Revenue { get; set; }

        public string AuditInfo { get; set; }

        public int? ProductId { get; set; }

        public virtual Product Product { get; set; }

        public DateTime FoundationDate { get; set; }

        public virtual List<User> Users { get; set; }

        public virtual List<SupplyHistory> SupplyHistory { get; set; }
    }
}
