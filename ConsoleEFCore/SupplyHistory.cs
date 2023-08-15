using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEFCore
{
    public sealed class SupplyHistory
    {
        public int Id { get; set; }
        
        public int ProductId { get; set; }
       
        public Product Product { get; set; }
    
        public int CompanyId { get; set; }
        
        public Company Company { get; set; }
     
        public DateTime ShipmentDate { get; set; }
     
        public double Price { get; set; }
    }
}
