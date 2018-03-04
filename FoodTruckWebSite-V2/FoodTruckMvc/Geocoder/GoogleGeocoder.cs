using FoodTruckMvc.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace FoodTruckMvc.Geocoder
{
    public class GoogleGeocoder
    {
        public GoogleGeocoder(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public async Task<GoogleGeocodeResponse> GetGeocodeAsync(LocationModel location)
        {
            var apiKey = Configuration["AppSettings:googleMapsApiKey"];
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://maps.googleapis.com/maps/api/geocode/json");
            var addressString = WebUtility.UrlEncode($"{location.StreetAddress} {location.City} {location.State}");
            var response = await httpClient.GetAsync($"?address={addressString}&key={apiKey}");
            var responseString = await response.Content.ReadAsStringAsync();
            var geocodeResult = JsonConvert.DeserializeObject<GoogleGeocodeResponse>(responseString);
            return geocodeResult;
        }
    }
}
