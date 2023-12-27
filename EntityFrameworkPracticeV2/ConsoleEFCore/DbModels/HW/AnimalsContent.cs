using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEFCore.DbModels.HW;
internal class AnimalsContent : DbContext
{
    public DbSet<Category> Categories { get; set; }

    public DbSet<Pet> Pets { get; set; }
}
