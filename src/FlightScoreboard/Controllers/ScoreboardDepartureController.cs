using System;
using System.Threading.Tasks;
using FlightScoreboard.Models;
using FlightScoreboardData.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightScoreboard.Controllers;

public class ScoreboardDepartureController : Controller
{
	private readonly ICityService _cityService;
	private readonly IScoreboardService _scoreboardService;

	public ScoreboardDepartureController(ICityService cityService, IScoreboardService scoreboardService)
	{
		_cityService = cityService;
		_scoreboardService = scoreboardService;
	}

	public async Task<IActionResult> Index(int? cityId, DateTime? dateTime)
	{
		var flightModel = new ScoreboardDepartureIndexIpModel();
		flightModel.CityId = cityId;
		flightModel.DateTime = dateTime;
		flightModel.Cities = await _cityService.GetAllCitiesAsync();
		flightModel.Flights = await _scoreboardService.GetDepartureFlightsAsync(cityId, dateTime);
		return View(flightModel);
	}
}