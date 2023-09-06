using System.Collections.Generic;

namespace IntroConsoleEF.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual List<Company> Companies { get; set; }

        public virtual List<SupplyHistory> SupplyHistory { get; set; }
    }
}
