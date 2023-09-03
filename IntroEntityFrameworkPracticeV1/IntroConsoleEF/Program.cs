using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace IntroConsoleEF
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ApplicationContext())
            {
                // context.Database.EnsureCreated(); // Possible but you should not use it in production. Instead you can invoke migraions.

                var users = context.Users.ToList();
                var companies = context.Companies.ToList();

                DbSet<Product> products = context.Products; // SELECT * FROM dbo.Products
                IQueryable<Product> products2 = context.Products.Where(x => x.Name.Equals("Audi")); // SELECT * FROM dbo.Products as p WHERE p.Name = "Audi"

                var userProfiles = context.UserProfiles.ToList();
            }
            Console.WriteLine("Hello World!");
        }
    }
}
