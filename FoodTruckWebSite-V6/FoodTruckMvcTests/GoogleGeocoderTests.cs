using FoodTruckMvc.Geocoder;
using FoodTruckMvc.Models;
using Microsoft.Extensions.Configuration;
using System.IO;
using Xunit;

namespace FoodTruckMvcTests
{
    public class GoogleGeocoderTests
    {
        public GoogleGeocoderTests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        private IConfiguration Configuration;

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
