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

	public IActionResult Index()
	{
		var cities = this._cityService.GetAllCities();
		return this.View(cities);
	}

	[HttpGet]
	public IActionResult Create()
	{
		return this.View();
	}

	[HttpPost]
	public IActionResult Create(CityCreateModel city)
	{
		this._cityService.CreateCity(city);
		return this.RedirectToAction("Index");
	}

	public IActionResult Delete(int id)
	{
		this._cityService.DeleteCity(id);
		return this.RedirectToAction("Index");
	}
}