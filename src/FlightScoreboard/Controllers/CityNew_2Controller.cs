using FlightScoreboard.Models;
using FlightScoreboardData.Services;
using FlightScoreboardData.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightScoreboard.Controllers;

public class CityNew_2Controller : Controller
{
	private readonly ICityService _cityService;

	public CityNew_2Controller(ICityService cityService)
	{
		_cityService = cityService;
	}

	[HttpGet]
	public IActionResult Index()
	{
		return View();
	}

	[HttpGet]
	public async Task<IActionResult> All()
	{
		var cities = await _cityService.GetAllCitiesAsync();
		return Json(cities);
	}

	[HttpPost]
	public async Task<IActionResult> Create(CityCreateModel model)
	{
		var cityId = await _cityService.CreateCityAsync(model);
		return Json(cityId);
	}

	[HttpPost]
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