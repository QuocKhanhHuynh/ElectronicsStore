using ElectronicsStore.BackendApi.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ElectronicsStore.BackendApi.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var enviromentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").AddJsonFile($"appsettings.{enviromentName}.json").Build();
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connecttionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connecttionString);
            return new ApplicationDbContext(builder.Options);
        }
    }
}
