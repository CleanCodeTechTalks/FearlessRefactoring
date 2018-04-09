using FoodTruckMvc.Data;
using Microsoft.EntityFrameworkCore;

namespace FoodTruckMvcTests
{
    public class LocationsControllerTests : FoodTruckMvcBaseConfiguration
    {
        public LocationsControllerTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FoodTruckContext>();
            Context = new FoodTruckContext(
                optionsBuilder
                .UseInMemoryDatabase(databaseName: "FoodTruckDemo")
                .Options);
        }
        protected FoodTruckContext Context;
    }
}
