using ConsoleEFCore.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ConsoleEFCore.DbModels.HW;

namespace ConsoleEFCore
{
    public class JoinedUserCompany
    {
        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public string UserName { get; set; }

        public int UserId { get; set; }

    }

    public sealed class Program
    {
        #region
        //protected override void Up(MigrationBuilder migrationBuilder)
        //{
        //    migrationBuilder.CreateTable(
        //        name: "ProductVersion",
        //        columns: table => new
        //        {
        //            Id = table.Column<int>(type: "int", nullable: false),
        //            Version = table.Column<string>(type: "nvarchar(max)", nullable: true),
        //        },
        //        constraints: table =>
        //        {
        //            table.PrimaryKey("PK_ProductVersionId", x => x.Id);
        //        });
        //}

        //protected override void Down(MigrationBuilder migrationBuilder)
        //{
        //    migrationBuilder.DropTable("ProductVersion");
        //}
        #endregion
         
        internal static async Task Main(string[] args)
        {
            await using (ConsoleApplicationContext context = new ConsoleApplicationContextFactory().CreateDbContext(Array.Empty<string>()))
            {
                AddEntitiesExample(context);
                context. .EnsureCreated();
                Company[] companies = context.Companies.ToArray();
                Car[] cars = context.Cars.ToArray();
            }
        }

        private static void EntityStateExample(ConsoleApplicationContext context)
        {
            //var trackingUser = context.Users.SingleOrDefault(x => x.Id == 77);
            //var entitiesTrack1 = context.ChangeTracker.Entries().ToList();
            //DisplayStates(entitiesTrack1);
            //trackingUser.LastName = "123";
            //var entitiesTrack2 = context.ChangeTracker.Entries().ToList();
            //DisplayStates(entitiesTrack2);

            List<User> asNoTrackingUsers = context.Users.AsNoTracking().ToList();
            context.ChangeTracker.Entries().ToList()[1].State = EntityState.Detached;
        }

        public static void AddEntitiesExample(ConsoleApplicationContext context)
        {
            var company = new Company
            {
                Id = 101,
                FoundationDate = DateTime.UtcNow,
                Name = "A-Code-2",
                Revenue = 2500
            };

            context
                .Companies
                .Add(company);

            var user = new User
            {
                UserId = 251,
                CompanyId = company.Id,
                Company = company,
                FirstName = "123 SaveChangesAsync",
                LastName = "123 SaveChangesAsync",
                HiredDate = DateTime.UtcNow,
            };

            context
                .Users
                .Add(user);

            context
                .UserProfiles
                .Add(new UserProfile
                {
                    Id = 11,
                    UserId = user.UserId,
                    User = user,
                    About = "In Code User Profile",
                    ImageUrl = "123"
                });


            //List<EntityEntry> trackerEntities = context.ChangeTracker.Entries().ToList();

            //DisplayStates(trackerEntities);

            context.Companies.Remove(company);

            context.SaveChanges();
        }

        private static void DisplayStates(IEnumerable<EntityEntry> entries)
        {
            foreach (var entry in entries)
            {
                Console.WriteLine($"Entity: {entry.Entity.GetType().Name}, State: {entry.State}");
            }
        }

        private static void GetEntitiesExample(ConsoleApplicationContext context)
        {
            var audiCar = context.Cars.FirstOrDefault(x => x.Name.Equals("New Name Audi 4"));
            var fordCar = context.Cars.FirstOrDefault(x => x.Name.Equals("Ford"));

            audiCar.Name = "Audi 4";
            fordCar.Name = "Ford Focus";

            context.SaveChanges();


            var groupedCars = context.Cars.GroupBy(x => x.Name.Length);

            foreach (var item in groupedCars)
            {
                Console.WriteLine(item.Key);
            }

            Console.WriteLine(groupedCars.ToQueryString());

            // SELECT [u].[Id], [u].[CompanyId], [u].[FirstName], [u].[HiredDate], [u].[LastName]
            // FROM[User] AS[u]
            // WHERE[u].[FirstName] LIKE N'%a%'

            //var sqlQueryable = users1.ToSql();
            //var newWay = users1.ToQueryString();

            //Console.WriteLine(newWay);

            IEnumerable<User> users2 = context.Users.AsEnumerable().Where(x => x.FirstName.Contains("A")); // SELECT * FROM dbo.Users 

            DbSet<Product> products = context.Products; // SELECT * FROM dbo.Products

            IQueryable<Product> products2 = context.Products.Where(x => x.Name.Equals("Audi")); // SELECT * FROM dbo.Products as p WHERE p.Name = "Audi"
            IEnumerable<Product> products3 = context.Products.Where(x => x.Name.Equals("Audi"));  // SELECT * FROM dbo.Products

            var userProfiles = context.UserProfiles.ToList();
        }
    }
}