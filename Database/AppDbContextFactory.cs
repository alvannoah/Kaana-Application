using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Database
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            var dbPath = @"C:\Users\ThyFellow\AppData\Local\Packages\51ae0c25-605a-426d-a8ca-2d3ab0f85152_xmed2a7jkj8s2\AC\kaana.db";

            //var dbPath = Environment.GetEnvironmentVariable("DB_LOCATION");

            optionsBuilder.UseSqlite($"Data Source={dbPath}");


            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
