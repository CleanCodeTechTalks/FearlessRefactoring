using FoodTruckMvc.Data;
using Microsoft.AspNetCore.Mvc;
using System;

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
        public IActionResult Index(DateTime? startDate = null)
        {
            if (!startDate.HasValue)
                startDate = DateTime.Now;

            var appointments = this.appointmentRepository.GetAppoinments(startDate.Value.Date, startDate.Value.Date.AddDays(7));

            return View(appointments);
        }
    }
}