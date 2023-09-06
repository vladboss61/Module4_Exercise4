using IntroConsoleEF.Context;
using IntroConsoleEF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
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
                LazyLoading(context);

                //List<Company> companies = context.Companies.ToList();
                var users = context
                    .Users
                    .Include(x => x.Company)
                        .ThenInclude(c => c.SupplyHistory)
                            .ThenInclude(sh => sh.Product)
                    .Include(x => x.Profile);

                var sql = users.ToQueryString();

                Console.WriteLine(sql);
                await EagerLoadingAsync(context);

                /*
                ////context.Database.EnsureCreated(); // Possible but you should not use it in production. Instead you can invoke migrations.

                //var users = context.Users.ToList();
                //var companies = context.Companies.ToList();

                //DbSet<Product> products = context.Products; // SELECT * FROM dbo.Products
                //IQueryable<Product> products2 = context.Products.Where(x => x.Name.Equals("Guitare")); // SELECT * FROM dbo.Products as p WHERE p.Name = "Audi"
                //var arrayProducts = products2.ToArray();

                //var userProfiles = context.UserProfiles.ToList();
                */

                //await AddCompanyUserUserProfileAsync(context, "Apple", "Den");
                //await AddCompanyUserUserProfileAsync(context, "Microsoft", "Tom");

                //await UpdateUserAsync(context, 1);
                await DeleteUserAsync(context, 1);
            }
            Console.WriteLine("Hello World!");
        }

        public static async Task AddCompanyUserUserProfileAsync(
            ApplicationDbContext context,
            string companyName,
            string userName)
        {
            //var searchCompany = context.Companies.FirstOrDefault(x => x.Name == "Microsoft");

            var apple = new Company { Name = companyName, FoundationDate = DateTime.Now };
            EntityEntry<Company> addedApple = await context.Companies.AddAsync(apple);

            Company appleEntity = addedApple.Entity;


            // Something Delete...

            // Something Update...

            await context.SaveChangesAsync();

            User userDen = new User { FirstName = userName, LastName = "Doe", CompanyId = appleEntity.Id };

            var addedUserDen = await context.Users.AddAsync(userDen);

            await context.SaveChangesAsync();

            UserProfile profile = new UserProfile { About = "Some Info about Den Doe user", UserId = addedUserDen.Entity.Id };

            await context.UserProfiles.AddAsync(profile);

            await context.SaveChangesAsync();
        }

        public static async Task DeleteUserAsync(ApplicationDbContext context, int userId)
        {
            User user = context.Users.FirstOrDefault(x => x.Id == userId);
            var userProfile = context.UserProfiles.FirstOrDefault(x => x.Id == userId);

            context.UserProfiles.Remove(userProfile);

            await context.SaveChangesAsync();

            context.Users.Remove(user);

            await context.SaveChangesAsync();
        }

        public static async Task UpdateUserAsync(ApplicationDbContext context, int userId)
        {
            //Change Detection | Change Tracker

            User user = context.Users.FirstOrDefault(x => x.Id == userId);

            user.FirstName = "Vlad";

            context.Users.Update(user);

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
            //5. Add MultipleActiveResultSets=True to connection string
            // After that lazy loading is included and all related are loaded when needed without Include()/ ThenInclude() funcs.

            var lazyCompanies = context.Companies.Where(x => x.Name == "Microsoft");

            foreach (var company in lazyCompanies)
            {
                Console.WriteLine("=====");
                Console.WriteLine(company.Id);
                Console.WriteLine(company.Name);
                Console.WriteLine("=====");

                foreach (var user in company.Users.Where(x => x.Id > 3))
                {
                    Console.WriteLine("=====User=====");
                    Console.WriteLine(user.Id);
                    Console.WriteLine(user.FirstName);
                    Console.WriteLine("=====User=====");
                }
            }
        }

        public static async Task TransactionsExampleAsync(ApplicationDbContext context)
        {
            using (IDbContextTransaction transaction = await context.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable))
            {
                try
                {
                    // context.Users Add Something
                    // context.UserProfile Add Something
                    // context.Companies Remove Something

                    await transaction.CommitAsync();
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                }
            }
        }
    }
}
