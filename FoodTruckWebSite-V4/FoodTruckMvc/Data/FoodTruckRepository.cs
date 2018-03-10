using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodTruckMvc.Models;

namespace FoodTruckMvc.Data
{
    public class FoodTruckRepository
    {

        public FoodTruckRepository(FoodTruckContext context)
        {
            this.dataContext = context;
        }


        private FoodTruckContext dataContext;


        public void CreateFoodTruck(FoodTruckModel model)
        {
            this.dataContext.FoodTrucks.Add(model);
            this.dataContext.SaveChanges();
        }

        public FoodTruckModel GetFoodTruckById(int id)
        {
            return this.dataContext.FoodTrucks
                .Where(x => x.FoodTruckId == id)
                .FirstOrDefault();
        }

        public List<FoodTruckModel> GetFoodTrucks()
        {
            return this.dataContext.FoodTrucks.ToList();
        }

        public void UpdateFoodTruck(FoodTruckModel model)
        {
            this.dataContext.FoodTrucks.Update(model);
            this.dataContext.SaveChanges();
        }
    }
}
