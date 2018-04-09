using FoodTruckMvc.Data;
using FoodTruckMvc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodTruckMvc.Controllers
{
    public class FoodTrucksController : Controller
    {
        public FoodTrucksController(FoodTruckContext context)
        {
            this.foodTruckRepository = new FoodTruckRepository(context);
        }

        private FoodTruckRepository foodTruckRepository;

        // GET: FoodTrucks
        public IActionResult Index()
        {
            var foodTrucks = this.foodTruckRepository.GetFoodTrucks();

            return View(foodTrucks);
        }

        // GET: FoodTrucks/Details/5
        public IActionResult Details(int id)
        {
            var foodTruck = this.foodTruckRepository.GetFoodTruckById(id);

            return View(foodTruck);
        }

        // GET: FoodTrucks/Create
        public IActionResult Create()
        {
            var model = new FoodTruckModel();
            return View(model);
        }

        // POST: FoodTrucks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FoodTruckModel model)
        {
            try
            {
                this.foodTruckRepository.CreateFoodTruck(model);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

        // GET: FoodTrucks/Edit/5
        public IActionResult Edit(int id)
        {
            var foodTruck = this.foodTruckRepository.GetFoodTruckById(id);

            return View(foodTruck);
        }

        // POST: FoodTrucks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, FoodTruckModel model)
        {
            try
            {
                model.FoodTruckId = id;
                this.foodTruckRepository.UpdateFoodTruck(model);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(model);
            }
        }

        // GET: FoodTrucks/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: FoodTrucks/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}