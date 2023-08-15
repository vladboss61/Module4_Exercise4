namespace ConsoleEFCore
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;

    public sealed class ConsoleApplicationContextFactory : IDesignTimeDbContextFactory<ConsoleApplicationContext>
    {
        public ConsoleApplicationContext CreateDbContext(string[] args)
        {
            // var connectionString = Environment.GetEnvironmentVariable("EFCORETOOLSDB");
            // SQLEXPRESS - Data Source=localhost\\SQLEXPRESS;Initial Catalog=ConsoleApplication;Integrated Security=True;MultipleActiveResultSets=true
            // SQL Server - Data Source=localhost;Initial Catalog=Application;Integrated Security=True;MultipleActiveResultSets=true
            // "Data Source=localhost;Initial Catalog=AdventureWorksLT2019;Integrated Security=True;TrustServerCertificate=True;MultipleActiveResultSets=true"

            var connectionString = "Data Source=localhost;Initial Catalog=ConsoleApplication;TrustServerCertificate=True;Integrated Security=True;MultipleActiveResultSets=true";
            var optionsBuilder = new DbContextOptionsBuilder<ConsoleApplicationContext>();
            var options = optionsBuilder
                .UseSqlServer(connectionString)
                .Options;

            return new ConsoleApplicationContext(options);
        }
    }
}
