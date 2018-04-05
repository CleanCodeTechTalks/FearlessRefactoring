﻿using FoodTruckMvc.Controllers;
using FoodTruckMvc.Data;
using FoodTruckMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            var locationsController = new LocationsController(Configuration, Context);
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

            var locationsController = new LocationsController(Configuration, Context);
            var result = await locationsController.Create(location) as ViewResult;
            result = await locationsController.Create(location) as ViewResult;

            Assert.Equal("The given address already exists. Enter a new address",
                         result.ViewData["Error"]);
        }
    }
}
