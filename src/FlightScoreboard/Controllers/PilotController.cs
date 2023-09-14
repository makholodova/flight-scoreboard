using FlightScoreboard.Models;
using FlightScoreboard.Services;
using FlightScoreboard.Services.Interfaces;
using FlightScoreboard.Services.Models;
using Microsoft.AspNetCore.Mvc;


namespace FlightScoreboard.Controllers;

public class PilotController : Controller
{
	private readonly IPilotService _pilotService;
	private readonly IAirlineService _airlineService;

	public PilotController(IPilotService pilotService, IAirlineService airlineService)
	{
		this._pilotService = pilotService;
		this._airlineService = airlineService;
	}

	public IActionResult Index()
	{
		var pilots = this._pilotService.GetAllPilots();
		return this.View(pilots);
	}

	[HttpGet]
	public IActionResult Create()
	{
		var pilotModelGet = new PilotCreateModelGet();
		pilotModelGet.Airlines = this._airlineService.GetAvailableAirlines();
		return this.View(pilotModelGet);
	}

	[HttpPost]
	public IActionResult Create(PilotCreateModel pilotNew)
	{
		this._pilotService.CreatePilot(pilotNew);
		return this.RedirectToAction("Index");
	}

	[HttpGet]
	public IActionResult Update(int id)
	{
		var pilotModel = new PilotUpdateModelGet();

		pilotModel.Pilot = this._pilotService.GetPilotById(id);
		pilotModel.Airlines = this._airlineService.GetAvailableAirlines();

		return this.View(pilotModel);
	}

	[HttpPost]
	public IActionResult Update(PilotUpdateModel pilot)
	{
		this._pilotService.UpdatePilot(pilot);

		return this.RedirectToAction("Index");
	}

	public IActionResult Delete(int id)
	{
		this._pilotService.DeletePilot(id);
		return this.RedirectToAction("Index");
	}
}