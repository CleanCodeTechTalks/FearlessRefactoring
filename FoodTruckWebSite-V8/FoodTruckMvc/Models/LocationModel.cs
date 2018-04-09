using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
