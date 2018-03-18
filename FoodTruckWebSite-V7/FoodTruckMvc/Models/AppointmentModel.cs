using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckMvc.Models
{
    public class AppointmentModel
    {
        [Key]
        public int AppointmentId { get; set; }

        public int LocationId { get; set; }

        public LocationModel Location { get; set; }

        public int FoodTruckId { get; set; }

        public FoodTruckModel FoodTruck { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }


    }
}
