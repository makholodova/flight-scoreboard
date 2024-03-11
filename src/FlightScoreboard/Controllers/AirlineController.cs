using System.Threading.Tasks;
using FlightScoreboard.Models;
using FlightScoreboardData.Services;
using FlightScoreboardData.Services.Models;
using Microsoft.AspNetCore.Mvc;
using AirlineCreateModel = FlightScoreboard.Models.AirlineCreateModel;
using IAirlineService = FlightScoreboard.Services.IAirlineService;

namespace FlightScoreboard.Controllers;

public class AirlineController : Controller

{
	private readonly IAirlineService _airlineService;

	public AirlineController(IAirlineService airlineService)
	{
		_airlineService = airlineService;
	}

	public async Task<IActionResult> Index()
	{
		var airlines = await _airlineService.GetAllAirlinesAsync();
		return View(airlines);
	}

	[HttpGet]
	public IActionResult Create()
	{
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> Create(AirlineCreateModel airline)
	{
		await _airlineService.CreateAirlineAsync(airline);
		return RedirectToAction("Index");
	}


	public async Task<IActionResult> Delete(int id)
	{
		var result = await _airlineService.DeleteAirlineAsync(id);
		if (result == false)
			return RedirectToAction("Index", "Error", new ErrorModel
			{
				ErrorMessage = "Удалить невозможно, возможно авиакомпания используется в рейсе, в списке пилотов, списке самолетов",
				ActionName = "Index",
				ControllerName = "Airline"
			});
		return RedirectToAction("Index");
	}
}