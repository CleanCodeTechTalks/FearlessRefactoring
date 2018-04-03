using FoodTruckMvc.Controllers;
using FoodTruckMvc.Data;
using FoodTruckMvc.Geocoder;
using FoodTruckMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FoodTruckMvcTests
{
    public class LocationsControllerCreateTests : FoodTruckMvcBaseConfiguration
    {
        public LocationsControllerCreateTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FoodTruckContext>();
            Context = new FoodTruckContext(
                optionsBuilder
                .UseInMemoryDatabase(databaseName: "FoodTruckDemo")
                .Options);
        }

        private FoodTruckContext Context;

        [Fact]
        public async Task LocationsControllerDoesNotReturnAddressIfAddressNotFound()
        {
            var badLocation = new LocationModel
            {
                Name = "Imaginary Spot",
                StreetAddress = "1313 Mockingbird Ln",
                City = "Keflavik",
                State = "CZ",
                ZipCode = "98765"
            };
            var badGeocode = new GoogleGeocodeResponse
            {
                results = Enumerable.Empty<Result>().ToList()
            };
            var mockGeocoder = new Mock<IGeocoder>();
            mockGeocoder.Setup(g => g.GetGeocodeAsync(badLocation)).Returns(Task.FromResult(badGeocode));

            var locationsController = new LocationsController(null, Context, mockGeocoder.Object);
            var result = await locationsController.Create(badLocation) as ViewResult;

            Assert.Equal(
                "This address could not be found. Please check this address and try again!",
                result.ViewData["Error"]);
        }

        [Fact]
        public async Task LocationsControllerShouldNotPersistTheSameLocationTwice()
        {
            var location = new LocationModel
            {
                Name = "Prime Spot",
                StreetAddress = "777 E Wisconsin Ave",
                City = "Milwaukee",
                State = "WI",
                ZipCode = "53202"
            };
            var mockGeocoder = new Mock<IGeocoder>();
            var geocodeWithGoodAddress = new GoogleGeocodeResponse
            {
                results = new List<Result>
                {
                    new Result
                    {
                        formatted_address = "This is a nicely formatted address."
                    }
                }
            };
            mockGeocoder.Setup(g => g.GetGeocodeAsync(location)).Returns(Task.FromResult(geocodeWithGoodAddress));

            var locationsController = new LocationsController(null, Context, mockGeocoder.Object);
            var result = await locationsController.Create(location) as ViewResult;
            result = await locationsController.Create(location) as ViewResult;

            Assert.Equal("The given address already exists. Enter a new address",
                         result.ViewData["Error"]);
        }
    }
}
