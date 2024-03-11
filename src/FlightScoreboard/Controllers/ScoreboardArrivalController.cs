using System;
using System.Threading.Tasks;
using FlightScoreboard.Models;
using FlightScoreboard.Services;
using FlightScoreboardData.Services;
using Microsoft.AspNetCore.Mvc;
using IScoreboardService = FlightScoreboard.Services.IScoreboardService;

namespace FlightScoreboard.Controllers;

public class ScoreboardArrivalController : Controller

{
	private readonly ICityService _cityService;
	private readonly IScoreboardService _scoreboardService;

	public ScoreboardArrivalController(ICityService cityService, IScoreboardService scoreboardService)
	{
		_cityService = cityService;
		_scoreboardService = scoreboardService;
	}

	public async Task<IActionResult> Index(int? cityId, DateTime? dateTime)
	{
		var flight = new ScoreboardArrivalIndexIpModel();
		flight.CityId = cityId;
		flight.DateTime = dateTime;
		flight.Cities = await _cityService.GetAllCitiesAsync();
		flight.Flights = await _scoreboardService.GetArrivalFlightsAsync(cityId, dateTime);
		return View(flight);
	}
}