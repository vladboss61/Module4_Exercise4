using System;
using System.Collections.Generic;
using System.Text;

namespace IntroConsoleEF.Models
{
    public class SupplyHistory
    {
        public int Id { get; set; }

        public int? ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int? CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public DateTime ShipmentDate { get; set; }

        public double Price { get; set; }
    }
}
