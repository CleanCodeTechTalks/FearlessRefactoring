using FoodTruckMvc.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckMvc.Data
{
    public class FoodTruckContext : DbContext
    {

        public FoodTruckContext(DbContextOptions options) : base(options)
        {

        }




        public DbSet<FoodTruckModel> FoodTrucks { get; set; }

        public DbSet<LocationModel> Locations { get; set; }

        public DbSet<AppointmentModel> Appointments { get; set; }

    }
}
