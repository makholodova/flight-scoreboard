using System.Threading.Tasks;
using FlightScoreboard.Models;
using FlightScoreboardData.Services;
using FlightScoreboardData.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightScoreboard.Controllers;

public class AirlineAirplaneController : Controller
{
	private readonly IAirlineAirplaneService _airlineAirplaneService;
	private readonly IAirlineService _airlineService;
	private readonly IAirplaneService _airplaneService;

	public AirlineAirplaneController(IAirlineAirplaneService airlineAirplaneService, IAirlineService airlineService,
		IAirplaneService airplaneService)
	{
		_airlineAirplaneService = airlineAirplaneService;
		_airlineService = airlineService;
		_airplaneService = airplaneService;
	}

	public async Task<IActionResult> Index(int airlineId)
	{
		var airplanesDb = await _airlineAirplaneService.GetAllAirlineAirplanesAsync(airlineId);
		var airplanes = new AirlineAirplaneIndexModel();
		airplanes.Airplanes = airplanesDb;
		airplanes.AirlineId = airlineId;
		return View(airplanes);
	}

	[HttpGet]
	public async Task<IActionResult> Create(int airlineId)
	{
		var airplaneModelGet = new AirlineAirplaneCreateModelGet();
		airplaneModelGet.Airplanes = await _airplaneService.GetAllAirplanesAsync();
		airplaneModelGet.AirlineId = airlineId;
		return View(airplaneModelGet);
	}

	[HttpPost]
	public async Task<IActionResult> Create(AirlineAirplaneCreateModel airplane)
	{
		await _airlineAirplaneService.CreateAirplaneAsync(airplane);
		return RedirectToAction("Index", new { airlineId = airplane.AirlineId });
	}

	[HttpGet]
	public async Task<IActionResult> Update(int id)
	{
		var airplane = new AirlineAirplaneUpdateModelGet();
		airplane.Airplane = await _airlineAirplaneService.GetAirplaneAirlineByIdAsync(id);
		airplane.Airplanes = await _airplaneService.GetAllAirplanesAsync();
		airplane.Airlines = await _airlineService.GetAvailableAirlinesAsync();

		//await Task.WhenAll(airplaneTask, airplanesTask, airlinesTask); 

		return View(airplane);
	}

	[HttpPost]
	public async Task<IActionResult> Update(AirlineAirplaneUpdateModel airplane)
	{
		await _airlineAirplaneService.UpdateAirplaneAsync(airplane);
		return RedirectToAction("Index", new { airlineId = airplane.AirlineId });
	}

	public async Task<IActionResult> Delete(int id, int airlineId)
	{
		var result = await _airlineAirplaneService.DeleteAirplaneAsync(id); // проблема new { airlineId = airlineId })
		if (result == false)
			return RedirectToAction("Index", "Error", new ErrorModel
			{
				ErrorMessage = "Удалить невозможно, возможно самолёт в рейсе",
				ActionName = "Index",
				ControllerName = "AirlineAirplane",
				RouteDataString = $"airlineId={airlineId}" //"key1=value1&key2=value2"
			});
		return RedirectToAction("Index", new { airlineId });
	}
}