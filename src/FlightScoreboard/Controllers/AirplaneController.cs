using FlightScoreboard.Models;
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

	public async Task<IActionResult> Index()
	{
		var airplanes = await this._airplaneService.GetAllAirplanesAsync();
		return this.View(airplanes);
	}

	[HttpGet]
	public IActionResult Create()
	{
		return this.View();
	}

	[HttpPost]
	public async Task<IActionResult> Create(AirplaneCreateModel airplane)
	{
		await this._airplaneService.CreateAirplaneAsync(airplane);
		return this.RedirectToAction("Index");
	}


	public async Task<IActionResult> Delete(int id)
	{
		var result = await this._airplaneService.DeleteAirplaneAsync(id);
		if (result == false)
			return RedirectToAction("Index", "Error", new ErrorModel
			{
				ErrorMessage = "Удалить невозможно, возможно модель самолета используется у авиакомпании",
				ActionName = "Index",
				ControllerName = "Airplane"
			});
		return this.RedirectToAction("Index");
	}
}