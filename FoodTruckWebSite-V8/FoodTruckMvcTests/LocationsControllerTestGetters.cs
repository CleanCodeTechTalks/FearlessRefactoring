using FoodTruckMvc.Controllers;
using FoodTruckMvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;

namespace FoodTruckMvcTests
{
    public class LocationsControllerTestGetters : LocationsControllerTests
    {
        [Fact]
        public void IndexIsInitiallyEmpty()
        {
            var locationsController = new LocationsController(null, Context, null);
            var result = locationsController.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<LocationModel>>(viewResult.ViewData.Model);
            Assert.Empty(model);
        }
    }
}
