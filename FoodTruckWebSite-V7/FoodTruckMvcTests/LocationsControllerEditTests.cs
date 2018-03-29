using FoodTruckMvc.Controllers;
using FoodTruckMvc.Geocoder;
using FoodTruckMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FoodTruckMvcTests
{
    public class LocationsControllerEditTests : LocationsControllerTests
    {
        [Fact]
        public void LocationsCannotGetLocationsThatDoNotExist()
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

            var locationsController = new LocationsController(Configuration, Context, mockGeocoder.Object);
            var id = 1;
            var result = locationsController.Edit(id) as ViewResult;

            Assert.Equal($"No location with id {id} exists. Please select a different location to edit",
                         result.ViewData["Error"]);
        }
    }
}
