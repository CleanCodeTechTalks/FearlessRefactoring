using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FoodTruckMvc.Data;

namespace FoodTruckMvc.Controllers
{
    public class AppointmentsController : Controller
    {


        public AppointmentsController(FoodTruckContext context)
        {
            this.appointmentRepository = new AppointmentsRepository(context);
        }

        private AppointmentsRepository appointmentRepository;

        // GET: Appointments
        public ActionResult Index(DateTime? startDate = null)
        {
            if (!startDate.HasValue)
                startDate = DateTime.Now;

            var appointments = this.appointmentRepository.GetAppoinments(startDate.Value.Date, startDate.Value.Date.AddDays(7));

            return View(appointments);
        }

        
    }
}