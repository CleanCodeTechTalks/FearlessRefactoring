using FoodTruckMvc.Controllers;
using FoodTruckMvc.Data;
using FoodTruckMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using Xunit;

namespace FoodTruckMvcTests
{
    public class LocationsControllerTests
    {
        public LocationsControllerTests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.Development.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            this.Configuration = builder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<FoodTruckContext>();
            optionsBuilder.UseSqlServer<FoodTruckContext>(Configuration.GetConnectionString("FoodTruckConnectionString"));

            Context = new FoodTruckContext(optionsBuilder.UseSqlServer(Configuration.GetConnectionString("FoodTruckConnectionString")).Options);
        }

        private IConfiguration Configuration;
        private FoodTruckContext Context;

        [Fact]
        public void LocationsControllerDoesNotReturnAddressIfAddressNotFound()
        {
            var locationsController = new LocationsController(Configuration, Context);

            var location = new LocationModel()
            {
                StreetAddress = "1313 Mockingbird Ln",
                City = "Keflavik",
                State = "CZ",
                ZipCode = "98765"
            };
            var result = locationsController.Create(location).Result as ViewResult;

            Assert.Equal("This address could not be found. Please check this address and try again!", result.ViewData["Error"]);
        }
    }
}
