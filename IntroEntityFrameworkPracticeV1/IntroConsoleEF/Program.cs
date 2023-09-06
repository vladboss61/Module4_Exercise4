using IntroConsoleEF.Context;
using IntroConsoleEF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IntroConsoleEF
{
    public sealed class Program
    {
        public static async Task Main(string[] args)
        {
            using (var context = new ApplicationDbContextFactory().CreateDbContext())
            {
                await EagerLoadingAsync(context);
                //context.Database.EnsureCreated(); // Possible but you should not use it in production. Instead you can invoke migrations.

                var users = context.Users.ToList();
                var companies = context.Companies.ToList();

                DbSet<Product> products = context.Products; // SELECT * FROM dbo.Products
                IQueryable<Product> products2 = context.Products.Where(x => x.Name.Equals("Guitare")); // SELECT * FROM dbo.Products as p WHERE p.Name = "Audi"
                var arrayProducts = products2.ToArray();

                var userProfiles = context.UserProfiles.ToList();

                await AddCompanyUserUserProfileAsync(context);

                await UpdateUserAsync(context, 1);
                //await DeleteUserAsync(context, 1);
            }
            Console.WriteLine("Hello World!");
        }

        public static async Task AddCompanyUserUserProfileAsync(ApplicationDbContext context)
        {
            var apple = new Company { Name = "Apple", FoundationDate = DateTime.Now };
            var addedApple = await context.Companies.AddAsync(apple);
            
            await context.SaveChangesAsync();

            Company appleEntity = addedApple.Entity;
            User userDen = new User { FirstName = "Den", LastName = "Doe", CompanyId = appleEntity.Id };

            var addedUserDen = await context.Users.AddAsync(userDen);
            await context.SaveChangesAsync();

            UserProfile profile = new UserProfile { About = "Some Info about Den Doe user", UserId = addedUserDen.Entity.Id };

            await context.UserProfiles.AddAsync(profile);

            await context.SaveChangesAsync();
        }

        public static async Task DeleteUserAsync(ApplicationDbContext context, int userId)
        {
            var user = context.Users.FirstOrDefault(x => x.Id == userId);

            context.Remove(user);

            await context.SaveChangesAsync();
        }

        public static async Task UpdateUserAsync(ApplicationDbContext context, int userId)
        {
            var user = context.Users.FirstOrDefault(x => x.Id == userId);
            user.FirstName = "Vlad";
            context.Update(user);

            await context.SaveChangesAsync();
        }

        public static Task EagerLoadingAsync(ApplicationDbContext context)
        {
            var eagerCompanies = context.Companies.Include(x => x.Users.Where(x => x.CompanyId != null));

            foreach (var company in eagerCompanies)
            {
                Console.WriteLine("=====");
                Console.WriteLine(company.Id);
                Console.WriteLine(company.Name);
                Console.WriteLine("=====");

                foreach (var user in company.Users)
                {
                    Console.WriteLine("=====User=====");
                    Console.WriteLine(user.Id);
                    Console.WriteLine(user.FirstName);
                    Console.WriteLine("=====User=====");
                }
            }

            return Task.CompletedTask;
        }

        public static void LazyLoading(ApplicationDbContext context)
        {
            //----To turn on lazy loading----
            //1. Add package <PackageReference Include="Microsoft.EntityFrameworkCore.Proxies" Version="7.0.4" /> to csproj
            //2. add .UseLazyLoadingProxies() to ApplicationDbContextFactory
            //3. No sealed classes near of database model-class.
            //4. Add virtual for linked class-table. For instance see SupplyHistory class Product and Company fields.
            // After that lazy loading is included and all related are loaded when needed without Include()/ ThenInclude() funcs.

            var lazyCompanies = context.Companies;

            foreach (var company in lazyCompanies)
            {
                Console.WriteLine("=====");
                Console.WriteLine(company.Id);
                Console.WriteLine(company.Name);
                Console.WriteLine("=====");

                foreach (var user in company.Users)
                {
                    Console.WriteLine("=====User=====");
                    Console.WriteLine(user.Id);
                    Console.WriteLine(user.FirstName);
                    Console.WriteLine("=====User=====");
                }
            }
        }
    }
}
