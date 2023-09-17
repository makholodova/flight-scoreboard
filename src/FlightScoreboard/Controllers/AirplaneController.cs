using FlightScoreboard.Services.Interfaces;
using FlightScoreboard.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightScoreboard.Controllers;

public class AirplaneController : Controller
{
	private readonly IAirplaneService _airplaneService;

	public AirplaneController(IAirplaneService airplaneService)
	{
		this._airplaneService = airplaneService;
	}

	public IActionResult Index()
	{
		var airplanes = this._airplaneService.GetAllAirplanes();
		return this.View(airplanes);
	}

	[HttpGet]
	public IActionResult Create()
	{
		return this.View();
	}

	[HttpPost]
	public IActionResult Create(AirplaneCreateModel airplane)
	{
		this._airplaneService.CreateAirplane(airplane);
		return this.RedirectToAction("Index");
	}


	public IActionResult Delete(int id)
	{
		this._airplaneService.DeleteAirplane(id);
		return this.RedirectToAction("Index");
	}
}