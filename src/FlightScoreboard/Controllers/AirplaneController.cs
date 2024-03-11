using System.Threading.Tasks;
using FlightScoreboard.Models;
using FlightScoreboardData.Services;
using FlightScoreboardData.Services.Models;
using Microsoft.AspNetCore.Mvc;
using AirplaneCreateModel = FlightScoreboard.Models.AirplaneCreateModel;

namespace FlightScoreboard.Controllers;

public class AirplaneController : Controller
{
	private readonly IAirplaneService _airplaneService;

	public AirplaneController(IAirplaneService airplaneService)
	{
		_airplaneService = airplaneService;
	}

	public async Task<IActionResult> Index()
	{
		var airplanes = await _airplaneService.GetAllAirplanesAsync();
		return View(airplanes);
	}

	[HttpGet]
	public IActionResult Create()
	{
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> Create(AirplaneCreateModel airplane)
	{
		await _airplaneService.CreateAirplaneAsync(airplane);
		return RedirectToAction("Index");
	}


	public async Task<IActionResult> Delete(int id)
	{
		var result = await _airplaneService.DeleteAirplaneAsync(id);
		if (result == false)
			return RedirectToAction("Index", "Error", new ErrorModel
			{
				ErrorMessage = "Удалить невозможно, возможно модель самолета используется у авиакомпании",
				ActionName = "Index",
				ControllerName = "Airplane"
			});
		return RedirectToAction("Index");
	}
}