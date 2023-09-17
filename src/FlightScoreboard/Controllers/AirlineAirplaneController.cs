using FlightScoreboard.Models;
using FlightScoreboard.Services.Interfaces;
using FlightScoreboard.Services.Models;
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
		this._airlineAirplaneService = airlineAirplaneService;
		this._airlineService = airlineService;
		this._airplaneService = airplaneService;
	}

	public IActionResult Index(int airlineId)
	{
		var airplanesDb = this._airlineAirplaneService.GetAllAAirlineAirplanes(airlineId);
		var airplanes = new AirlineAirplaneIndexModel();
		airplanes.Airplanes = airplanesDb;
		airplanes.AirlineId = airlineId;
		return this.View(airplanes);
	}

	[HttpGet]
	public IActionResult Create(int airlineId)
	{
		var airplaneModelGet = new AirlineAirplaneCreateModelGet();
		airplaneModelGet.Airplanes = this._airplaneService.GetAllAirplanes();
		airplaneModelGet.AirlineId = airlineId;
		return this.View(airplaneModelGet);
	}

	[HttpPost]
	public IActionResult Create(AirlineAirplaneCreateModel airplane)
	{
		this._airlineAirplaneService.CreateAirplane(airplane);
		return this.RedirectToAction("Index", new { airlineId = airplane.AirlineId });
	}

	[HttpGet]
	public IActionResult Update(int id)
	{
		var airplane = new AirlineAirplaneUpdateModelGet();
		airplane.Airplane = this._airlineAirplaneService.GetAirplaneAirlineById(id);
		airplane.Airplanes = this._airplaneService.GetAllAirplanes();
		airplane.Airlines = this._airlineService.GetAvailableAirlines();

		return this.View(airplane);
	}

	[HttpPost]
	public IActionResult Update(AirlineAirplaneUpdateModel airplane)
	{
		this._airlineAirplaneService.UpdateAirplane(airplane);
		return this.RedirectToAction("Index", new { airlineId = airplane.AirlineId });
	}

	public IActionResult Delete(int id, int airlineId)
	{
		this._airlineAirplaneService.DeleteAirplane(id);
		return this.RedirectToAction("Index", new { airlineId = airlineId });
	}
}