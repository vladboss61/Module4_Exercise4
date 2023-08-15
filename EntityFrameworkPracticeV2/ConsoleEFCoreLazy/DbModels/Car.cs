using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEFCoreLazy.DbModels
{
    public class Car // No sealed, also for lazy loading.
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public int Size { get; set; }

        public string Color { get; set; }

        public string Color1 { get; set; }
    }
}
