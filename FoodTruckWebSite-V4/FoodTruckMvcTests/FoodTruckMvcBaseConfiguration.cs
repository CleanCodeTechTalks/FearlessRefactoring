using Microsoft.Extensions.Configuration;
using System.IO;

namespace FoodTruckMvcTests
{
    public class FoodTruckMvcBaseConfiguration
    {
        public FoodTruckMvcBaseConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        protected IConfiguration Configuration;
    }
}
