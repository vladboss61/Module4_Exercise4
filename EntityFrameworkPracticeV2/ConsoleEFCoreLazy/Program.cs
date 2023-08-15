using Azure;
using ConsoleEFCoreLazy.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleEFCoreLazy
{
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

        internal static void Main(string[] args)
        {
            using (ApplicationContext context = new ApplicationContextFactory().CreateDbContext(Array.Empty<string>()))
            {
                //Eager Loading.
                var usersJoined = context
                    .Users
                    .Include(user => user.Company)
                    .ThenInclude(company => company.SupplyHistory);

                Console.WriteLine(usersJoined.ToQueryString());
                Console.WriteLine();
                /*
                //context.Database.EnsureCreated();

                // Lazy loading
                var users0 = context.Users;

                foreach (var user in users0)
                {
                    Console.WriteLine(user.FirstName);
                    Console.WriteLine(user.Company?.Name ?? "NUll");
                }

                Console.WriteLine(users0.ToQueryString());


                var users2 = context
                    .Users
                    .Include(x => x.Company)
                    .Include(user => user.Profile).ToList();

                */

                //Change Tracker
                //var first = context.Users.FirstOrDefault();

                //var listOfEntries = context.ChangeTracker.Entries().ToList();

                //first.LastName = "Changed New";

                //var afterChangedlistOfEntries = context.ChangeTracker.Entries().ToList();

                //context.SaveChanges();

                ////No Change Tracker

                var noChangeFirst = context.Users.AsNoTracking().FirstOrDefault();

                var comp = context.Companies;

                foreach (var item in comp)
                {
                    item.Name = "New Complany name";
                }

                var noChangeListOfEntries = context.ChangeTracker.Entries().ToList();

                noChangeFirst.LastName = "NoChange Changed";

                var noChangeAfterChangedlistOfEntries = context.ChangeTracker.Entries().ToList();

                var companies = context.Users.Select(x => x.Company);
                context.SaveChanges();
            }
            Console.WriteLine("Hello World!");
        }
    }
}