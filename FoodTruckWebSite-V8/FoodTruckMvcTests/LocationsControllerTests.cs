using FoodTruckMvc.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace FoodTruckMvcTests
{
    public class LocationsControllerTests : FoodTruckMvcBaseConfiguration
    {
        public LocationsControllerTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FoodTruckContext>();
            Context = new FoodTruckContext(
                optionsBuilder
                .UseInMemoryDatabase(databaseName: $"{Guid.NewGuid()}")
                .Options);
        }
        protected FoodTruckContext Context;
    }
}
