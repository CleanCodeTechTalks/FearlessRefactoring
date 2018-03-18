using FoodTruckMvc.Geocoder;
using FoodTruckMvc.Models;
using Xunit;

namespace FoodTruckMvcTests
{
    public class GoogleGeocoderTests : FoodTruckMvcBaseConfiguration
    {
        [Fact]
        public void GetGeocodeDoesNotFindInvalidAddress()
        {
            var location = new LocationModel()
            {
                Name = "Imaginary Spot",
                StreetAddress = "1313 Mockingbird Ln",
                City = "Keflavik",
                State = "CZ",
                ZipCode = "98765"
            };
            var geocoder = new GoogleGeocoder(Configuration);

            var geocode = geocoder.GetGeocodeAsync(location).Result;

            Assert.Empty(geocode.results);
        }

        [Fact]
        public void GetGeocodeFindsValidAddress()
        {
            var location = new LocationModel()
            {
                Name = "Prime Spot",
                StreetAddress = "777 E Wisconsin Ave",
                City = "Milwaukee",
                State = "WI",
                ZipCode = "53202"
            };
            var geocoder = new GoogleGeocoder(Configuration);

            var geocode = geocoder.GetGeocodeAsync(location).Result;

            Assert.Single(geocode.results);
        }
    }
}
