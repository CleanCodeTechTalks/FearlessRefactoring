using FoodTruckMvc.Geocoder;
using FoodTruckMvc.Models;
using System.Threading.Tasks;
using Xunit;

namespace FoodTruckMvcTests
{
    public class GoogleGeocoderTests : FoodTruckMvcBaseConfiguration
    {
        [Fact]
        public async Task GetGeocodeDoesNotFindInvalidAddressAsync()
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

            var geocode = await geocoder.GetGeocodeAsync(location);

            Assert.Empty(geocode.results);
        }

        [Fact]
        public async Task GetGeocodeFindsValidAddress()
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

            var geocode = await geocoder.GetGeocodeAsync(location);

            Assert.Single(geocode.results);
        }
    }
}
