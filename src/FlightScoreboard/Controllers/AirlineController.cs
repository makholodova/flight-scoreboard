using FlightScoreboard.Services.Interfaces;
using FlightScoreboard.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightScoreboard.Controllers;

public class AirlineController : Controller

{
	private readonly IAirlineService _airlineService;

	public AirlineController(IAirlineService airlineService)
	{
		this._airlineService = airlineService;
	}

	public IActionResult Index()
	{
		var airlines = this._airlineService.GetAllAirlines();
		return this.View(airlines);
	}

	[HttpGet]
	public IActionResult Create()
	{
		return this.View();
	}

	[HttpPost]
	public IActionResult Create(AirlineCreateModel airline)
	{
		this._airlineService.CreateAirline(airline);
		return this.RedirectToAction("Index");
	}


	public IActionResult Delete(int id)
	{
		this._airlineService.DeleteAirline(id);
		return this.RedirectToAction("Index");
	}
}