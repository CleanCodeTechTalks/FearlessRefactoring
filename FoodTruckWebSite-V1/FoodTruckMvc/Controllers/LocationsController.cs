using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FoodTruckMvc.Data;
using FoodTruckMvc.Models;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace FoodTruckMvc.Controllers
{
    public class LocationsController : Controller
    {

        public LocationsController(IConfiguration configuration, FoodTruckContext foodTruckContext)
        {
            this.configuration = configuration;
            this.locationRepository = new LocationRepository(foodTruckContext);
        }

        private IConfiguration configuration;
        private LocationRepository locationRepository;


        // GET: Locations
        public ActionResult Index()
        {
            var locations = this.locationRepository.GetLocations();
            return View(locations);
        }

        // GET: Locations/Details/5
        public ActionResult Details(int id)
        {
            var location = this.locationRepository.GetLocation(id);

            if ( location == null)
            {
                ViewBag["Error"] = $"No location was found with the id {id}";
                return RedirectToAction(nameof(Index));
            }

            return View(location);
        }

        // GET: Locations/Create
        public ActionResult Create()
        {
            var location = new LocationModel();

            return View(location);
        }

        // POST: Locations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LocationModel model)
        {
            try
            {                
                String apiKey = this.configuration["AppSettings:googleMapsApiKey"];
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri("https://maps.googleapis.com/maps/api/geocode/json");
                var addressString = WebUtility.UrlEncode($"{model.StreetAddress} {model.City} {model.State}");
                var response = await httpClient.GetAsync($"?address={addressString}&key={apiKey}");                
                var geocodeResult = JsonConvert.DeserializeObject<GoogleGeocodeResponse>(response.Content.ReadAsStringAsync().Result);

                if ( geocodeResult.results.Count == 0)
                {
                    ViewBag.Error = "This address could not be found.  Please check this address and try again!";
                    return View(model);
                }

                var formattedAddress = geocodeResult.results[0].formatted_address;

                var existingAddres = this.locationRepository.GetLocationByFormattedAddress(formattedAddress);
                if ( existingAddres != null )
                {
                    ViewBag.Error = "The given address already exists.  Enter a new address";
                    return View(model);
                }

                var newLocation = new LocationModel();
                newLocation.Name = model.Name;
                newLocation.StreetAddress = model.StreetAddress;
                newLocation.City = model.City;
                newLocation.State = model.State;
                newLocation.ZipCode = model.ZipCode;
                newLocation.FormattedAddress = formattedAddress;

                this.locationRepository.CreateLocation(newLocation);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: Locations/Edit/5
        public ActionResult Edit(int id)
        {
            var location = this.locationRepository.GetLocation(id);

            return View(location);
        }

        // POST: Locations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, LocationModel model)
        {
            try
            {
                var current = this.locationRepository.GetLocation(id);

                if ( current == null)
                {
                    ViewBag["Error"] = $"No location with id {id} exists.  Please select a different location to edit";
                    return RedirectToAction(nameof(Index));
                }

                String apiKey = this.configuration["AppSettings:googleMapsApiKey"];
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri("https://maps.googleapis.com/maps/api/geocode/json");
                var addressString = WebUtility.UrlEncode($"{model.StreetAddress} {model.City} {model.State}");
                var response = await httpClient.GetAsync($"?address={addressString}&key={apiKey}");
                var geocodeResult = JsonConvert.DeserializeObject<GoogleGeocodeResponse>(response.Content.ReadAsStringAsync().Result);

                var formattedAddress = geocodeResult.results[0].formatted_address;

                var existingAddres = this.locationRepository.GetLocationByFormattedAddress(formattedAddress);
                if (existingAddres != null)
                {
                    ViewBag["Error"] = "The given address already exists.  Enter a new address";
                    return View(model);
                }

                current.Name = model.Name;
                current.StreetAddress = model.Name;
                current.City = model.City;
                current.State = model.State;
                current.ZipCode = model.ZipCode;
                current.FormattedAddress = $"{model.StreetAddress} {model.City} {model.State}";

                this.locationRepository.UpdateLocation(current);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }

    }
}