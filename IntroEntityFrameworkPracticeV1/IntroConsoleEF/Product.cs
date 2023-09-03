﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IntroConsoleEF
{
    public class Product
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public List<Company> Companies { get; set; }

        public List<SupplyHistory> SupplyHistory { get; set; }
    }
}
