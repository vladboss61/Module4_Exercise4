﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IntroConsoleEF
{
    public class Company
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public decimal? Revenue { get; set; }
     
        public DateTime FoundationDate { get; set; }

        public List<SupplyHistory> SupplyHistory { get; set; }
    }
}
