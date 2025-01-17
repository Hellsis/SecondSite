using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SecondSite.Models;
using System.Net;
using System.Text;


namespace SecondSite.Controllers
{
    public class MainController : Controller
    {
        private readonly ILogger<MainController> _logger;



        public MainController(ILogger<MainController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> MainPage()
        {

            string apiUrl = "https://localhost:44371/api/WebAPIController";

            List<Car> cars = new List<Car>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(apiUrl))
                {

                    string json = await response.Content.ReadAsStringAsync();
                    cars = JsonConvert.DeserializeObject<List<Car>>(json);

                }
            }

            return View(cars);
        }

        [HttpPost]
		public async Task<IActionResult> Delete(int id) 
        {
			string apiUrl = "https://localhost:44371/api/WebAPIController/";

			using (var httpClient = new HttpClient())
			{
				using (var response = await httpClient.DeleteAsync(apiUrl + id))
				{
                    

					string json = await response.Content.ReadAsStringAsync();
				}
			}
			return RedirectToAction("MainPage");
		}

        [HttpPost]
        public async Task<IActionResult> Add(string name, string type, string price, string discription) 
        {
			try
			{
				Car car = new Car() { Name = name, Type = type, Discription = discription, Price = Convert.ToDecimal(price) };
				Car deserCar = new Car();

				string apiUrl = "https://localhost:44371/api/WebAPIController/";

				using (var httpClient = new HttpClient())
				{
					StringContent content = new StringContent(JsonConvert.SerializeObject(car), Encoding.UTF8, "application/json");

					using (var response = await httpClient.PostAsync(apiUrl, content))
					{
						string json = await response.Content.ReadAsStringAsync();

						deserCar = JsonConvert.DeserializeObject<Car>(json);
					}
				}
			} catch { }
			return RedirectToAction("MainPage");
	
		}
		[HttpPost]
		public async Task<IActionResult> Edit(int id, string name, string type, string price, string discription) 
        {
			try
			{
				Car deserCar = new Car();

				string apiUrl = "https://localhost:44371/api/WebAPIController/";

				using (var httpClient = new HttpClient())
				{
					var content = new MultipartFormDataContent();
					content.Add(new StringContent(id.ToString()), "Id");
					content.Add(new StringContent(name), "Name");
					content.Add(new StringContent(type), "Type");
					content.Add(new StringContent(price), "Price");
					if (discription != null)
					{
						content.Add(new StringContent(discription), "Discription");
					}
					else
					{
						content.Add(new StringContent(""), "Discription");
					}

					using (var response = await httpClient.PutAsync(apiUrl, content))
					{
						string json = await response.Content.ReadAsStringAsync();

						deserCar = JsonConvert.DeserializeObject<Car>(json);
					}
				}
			}catch { }
			return RedirectToAction("MainPage");
		}
	}
}
