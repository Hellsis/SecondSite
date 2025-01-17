using FirstSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    public class MainController : Controller
    {
        private readonly ILogger<MainController> _logger;

        private readonly MyDbContext myDbContext;
        
        public MainController(ILogger<MainController> logger, MyDbContext context) 
        {
            _logger = logger;
            myDbContext = context;
        }

        public IActionResult MainPage()
        {
            var cars = myDbContext.Cars.ToList();
            return View(cars);
        }

        [HttpPost]
        public async  Task<IActionResult> Delete(int id) 
        {
            var car = await myDbContext.Cars.FirstOrDefaultAsync(x => x.Id == id);
            myDbContext.Cars.Remove(car);
			await myDbContext.SaveChangesAsync();
            return RedirectToAction("MainPage");
        }

        [HttpPost]
        public async Task<IActionResult> Add(string name, string type, string price, string discription)
        {
            try 
            {
				var car = new Car() { Name = name, Type = type, Price = Convert.ToDecimal(price), Discription = discription };
				myDbContext.Cars.Add(car);
				await myDbContext.SaveChangesAsync();
			}
            catch 
            {
				
			}
			return RedirectToAction("MainPage");
		}

		[HttpPost]
		public async Task<IActionResult> Edit(string name, string type, string price, string discription, int id)
		{
            try 
            {
				var car = await myDbContext.Cars.FirstOrDefaultAsync(x => x.Id == id);
				car.Name = name;
				car.Type = type;
				car.Price = Convert.ToDecimal(price);
				car.Discription = discription;

				myDbContext.Cars.Update(car);
				await myDbContext.SaveChangesAsync();
			}
            catch 
            {
                
            }
            
			return RedirectToAction("MainPage");
		}
	}
}
