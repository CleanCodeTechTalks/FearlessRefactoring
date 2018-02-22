using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTruckMvc.Models
{
    public class FoodTruckModel
    {
        [Key]
        public int FoodTruckId { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public String Website { get; set; }

    }
}
