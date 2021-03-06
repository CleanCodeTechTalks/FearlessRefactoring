﻿using FoodTruckMvc.Data;
using FoodTruckMvc.Geocoder;
using FoodTruckMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace FoodTruckMvc.Controllers
{
    public class LocationsController : Controller
    {
        public LocationsController(
            IConfiguration configuration,
            FoodTruckContext foodTruckContext,
            IGeocoder geocoder)
        {
            Configuration = configuration;
            Geocoder = geocoder;
            Repository = new LocationRepository(foodTruckContext);
        }

        private IConfiguration Configuration;
        private LocationRepository Repository;
        private IGeocoder Geocoder;

        // GET: Locations
        public IActionResult Index()
        {
            var locations = Repository.GetLocations();
            return View(locations);
        }

        // GET: Locations/Details/5
        public IActionResult Details(int id)
        {
            var location = Repository.GetLocation(id);

            if (location == null)
            {
                ViewBag["Error"] = $"No location was found with the id {id}";
                return RedirectToAction(nameof(Index));
            }

            return View(location);
        }

        // GET: Locations/Create
        public IActionResult Create()
        {
            var location = new LocationModel();

            return View(location);
        }

        // POST: Locations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LocationModel location)
        {
            try
            {
                var geocodeResult = await Geocoder.GetGeocodeAsync(location);

                if (geocodeResult.results.Count == 0)
                {
                    ViewBag.Error = "This address could not be found. Please check this address and try again!";
                    return View(location);
                }

                var formattedAddress = geocodeResult.results[0].formatted_address;

                var existingAddres = Repository.GetLocationByFormattedAddress(formattedAddress);
                if (existingAddres != null)
                {
                    ViewBag.Error = "The given address already exists. Enter a new address";
                    return View(location);
                }

                var newLocation = new LocationModel();
                newLocation.Name = location.Name;
                newLocation.StreetAddress = location.StreetAddress;
                newLocation.City = location.City;
                newLocation.State = location.State;
                newLocation.ZipCode = location.ZipCode;
                newLocation.FormattedAddress = formattedAddress;

                Repository.CreateLocation(newLocation);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: Locations/Edit/5
        public IActionResult Edit(int id)
        {
            var location = Repository.GetLocation(id);
            if (location == null)
            {
                ViewBag.Error = $"No location with id {id} exists. Please select a different location to edit";
            }

            return View(location);
        }

        // POST: Locations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LocationModel location)
        {
            try
            {
                var current = Repository.GetLocation(id);

                if (current == null)
                {
                    return RedirectToAction(nameof(Edit), new { id });
                }

                var geocodeResult = await Geocoder.GetGeocodeAsync(location);

                if (geocodeResult.results.Count == 0)
                {
                    ViewBag.Error = "This address could not be found. Please check this address and try again!";
                    return View(location);
                }

                var formattedAddress = geocodeResult.results[0].formatted_address;

                var existingAddres = Repository.GetLocationByFormattedAddress(formattedAddress);
                if (existingAddres != null)
                {
                    ViewBag.Error = "The given address already exists. Enter a new address";
                    return View(location);
                }

                current.Name = location.Name;
                current.StreetAddress = location.Name;
                current.City = location.City;
                current.State = location.State;
                current.ZipCode = location.ZipCode;
                current.FormattedAddress = $"{location.StreetAddress} {location.City} {location.State}";

                Repository.UpdateLocation(current);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(location);
            }
        }
    }
}