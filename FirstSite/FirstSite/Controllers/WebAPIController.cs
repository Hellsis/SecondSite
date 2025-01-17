using FirstSite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Controllers;

namespace FirstSite.Controllers
{
	
    [ApiController]
	[Route("api/WebAPIController")]
	public class WebAPIController : ControllerBase
    {
		private readonly MyDbContext myDbContext;

		public WebAPIController(MyDbContext _myDbContext) { 
            myDbContext = _myDbContext;
        }
		
        [HttpGet]

		public List<Car> GetCars()
        {
			var cars = myDbContext.Cars.ToList();
			return cars;
        }

		[HttpDelete("{id}")]
		public async void Delete( int id)
        {
			var car = myDbContext.Cars.FirstOrDefault(x=> x.Id == id);
			myDbContext.Cars.Remove(car);
			myDbContext.SaveChanges();
			
		}

		[HttpPost]
		public async void Add(Car car) 
		{
			try 
			{
				myDbContext.Cars.Add(car);
				myDbContext.SaveChanges();
			}
			catch { }
			
		}

		[HttpPut]
		public async void Edit([FromForm]Car car) 
		{

			try
			{
				myDbContext.Cars.Update(car);
				myDbContext.SaveChanges();
			}
			catch { }
		}
        
    }
}
