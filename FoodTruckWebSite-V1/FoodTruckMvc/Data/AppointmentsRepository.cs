using FoodTruckMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FoodTruckMvc.Data
{
    public class AppointmentsRepository
    {

        public AppointmentsRepository(FoodTruckContext context)
        {
            this.dataContext = context;
        }

        private FoodTruckContext dataContext;


        public List<AppointmentModel> GetAppoinments(DateTime startDate, DateTime endDate)
        {
            return this.dataContext.Appointments
                .Include(a => a.FoodTruck)
                .Include(a => a.Location)
                .Where(x => x.StartTime >= startDate && x.StartTime <= endDate)                
                .ToList();
        }





    }
}
