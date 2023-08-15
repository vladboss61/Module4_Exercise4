using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEFCore.DbModels
{
    public sealed class Car
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)] //default is Identity generated 0 1 2 3 and so on. Alternative to configuration.
        public int Id { get; set; }

        public string Name { get; set; }
        
        public int Size { get; set; }

        public string Color { get; set; }

        public string Color1 { get; set; }
    }
}
