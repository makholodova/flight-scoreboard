using System.Threading.Tasks;
using FlightScoreboard.Models;
using FlightScoreboard.Services;
using FlightScoreboardData.Services;
using FlightScoreboardData.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightScoreboard.Controllers;

public class CityController : Controller
{
	private readonly ICityService _cityService;

	public CityController(ICityService cityService)
	{
		_cityService = cityService;
	}

	public async Task<IActionResult> Index()
	{
		var cities = await _cityService.GetAllCitiesAsync();
		return View(cities);
	}

	[HttpGet]
	public IActionResult Create()
	{
		return View();
	}

	/*[HttpPost]
	public async Task<IActionResult> Create(CityCreateModel city)
	{
		await _cityService.CreateCityAsync(city);
		return RedirectToAction("Index");
	}*/

	public async Task<IActionResult> Delete(int id)
	{
		var result = await _cityService.DeleteCityAsync(id);
		if (result == false)
			return RedirectToAction("Index", "Error", new ErrorModel
			{
				ErrorMessage = "Удалить невозможно, возможно город используется в рейсе",
				ActionName = "Index",
				ControllerName = "City"
			});
		return RedirectToAction("Index");
	}
}