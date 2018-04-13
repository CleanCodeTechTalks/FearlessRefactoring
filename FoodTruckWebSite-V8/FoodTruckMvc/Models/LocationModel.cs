using System;
using System.ComponentModel.DataAnnotations;

namespace FoodTruckMvc.Models
{
    public class LocationModel
    {
        [Key]
        public int LocationId { get; set; }

        public String Name { get; set; }

        public String StreetAddress { get; set; }

        public String City { get; set; }

        public String State { get; set; }

        public String ZipCode { get; set; }

        public String FormattedAddress { get; set; }
    }
}
