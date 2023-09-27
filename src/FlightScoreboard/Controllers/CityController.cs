using FlightScoreboard.Services.Interfaces;
using FlightScoreboard.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightScoreboard.Controllers;

public class CityController : Controller
{
	private readonly ICityService _cityService;

	public CityController(ICityService cityService)
	{
		this._cityService = cityService;
	}

	public async Task<IActionResult>  Index()
	{
		 var cities = await this._cityService.GetAllCitiesAsync();
		 return this.View(cities);
	}

	[HttpGet]
	public IActionResult Create()
	{
		return this.View();
	}

	[HttpPost]
	public async Task<IActionResult> Create(CityCreateModel city)
	{
		await this._cityService.CreateCityAsync(city);
		return this.RedirectToAction("Index");
	}

	public async Task<IActionResult> Delete(int id)
	{
		await this._cityService.DeleteCityAsync(id);
		return this.RedirectToAction("Index");
	}
}