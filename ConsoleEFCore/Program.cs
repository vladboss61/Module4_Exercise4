using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ApplicationContext("Data Source=DESKTOP-EEE2PPD;Initial Catalog = Application;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
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
