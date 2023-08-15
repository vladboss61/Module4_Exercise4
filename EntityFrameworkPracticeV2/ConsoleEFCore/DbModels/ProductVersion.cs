using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleEFCore.DbModels
{
    public sealed class ProductVersion
    {
        public int Id { get; set; }

        public int Name { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; } 
    }
}
