using System.Collections.Generic;

namespace IntroConsoleEF.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Company> Companies { get; set; }

        public List<SupplyHistory> SupplyHistory { get; set; }
    }
}
