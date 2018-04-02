using FoodTruckMvc.Controllers;
using FoodTruckMvc.Geocoder;
using FoodTruckMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FoodTruckMvcTests
{
    public class LocationsControllerEditTests : LocationsControllerTests
    {
        [Fact]
        public void CannotEditLocationsThatDoNotExist()
        {
            var locationsController = new LocationsController(null, Context, null);
            var id = 9;
            var result = locationsController.Edit(id) as ViewResult;

            Assert.Equal($"No location with id {id} exists. Please select a different location to edit",
                         result.ViewData["Error"]);
        }

        [Fact]
        public void CannotMakeExistingLocationsInvalid()
        {
            var goodLocation = new LocationModel
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
            mockGeocoder.Setup(g => g.GetGeocodeAsync(goodLocation)).Returns(Task.FromResult(geocodeWithGoodAddress));

            var locationsController = new LocationsController(Configuration, Context, mockGeocoder.Object);
            var result = locationsController.Create(goodLocation).Result as ViewResult;

            var badLocation = new LocationModel
            {
                Name = goodLocation.Name,
                StreetAddress = goodLocation.StreetAddress,
                City = goodLocation.City,
                State = goodLocation.State,
                ZipCode = "99999"   // Invalid Zip Code
            };
            var badGeocode = new GoogleGeocodeResponse
            {
                results = Enumerable.Empty<Result>().ToList()
            };
            mockGeocoder.Setup(g => g.GetGeocodeAsync(badLocation)).Returns(Task.FromResult(badGeocode));

            result = locationsController.Edit(1, badLocation).Result as ViewResult;

            Assert.Equal(
                "This address could not be found. Please check this address and try again!",
                result.ViewData["Error"]);
        }
    }
}
