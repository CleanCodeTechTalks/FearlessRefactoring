using FoodTruckMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckMvc.Data
{
    public class LocationRepository
    {

        public LocationRepository(FoodTruckContext context)
        {
            this.dataContext = context;
        }

        private FoodTruckContext dataContext;


        public List<LocationModel> GetLocations()
        {
            return this.dataContext.Locations.ToList();
        }


        public LocationModel GetLocation(int id)
        {
            return this.dataContext.Locations
                .Where(x => x.LocationId == id)
                .FirstOrDefault();
        }


        public LocationModel GetLocationByFormattedAddress(string formattedAddress)
        {
            return this.dataContext.Locations
                .Where(x => x.FormattedAddress == formattedAddress)
                .FirstOrDefault();
        }


        public void CreateLocation(LocationModel model)
        {
            this.dataContext.Locations.Add(model);
            this.dataContext.SaveChanges();
        }


        public void UpdateLocation(LocationModel model)
        {
            this.dataContext.Locations.Update(model);
            this.dataContext.SaveChanges();
        }

    }
}
