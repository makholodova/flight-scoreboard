using System;
using System.Threading.Tasks;
using FlightScoreboardApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightScoreboardApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ScoreboardArrivalController : ControllerBase

{
	private readonly ICityService _cityService;
	private readonly IScoreboardService _scoreboardService;

	public ScoreboardArrivalController(ICityService cityService, IScoreboardService scoreboardService)
	{
		_cityService = cityService;
		_scoreboardService = scoreboardService;
	}

	[HttpGet]
	public async Task<IActionResult> All(int? cityId, DateTime? dateTime)
	{
		var flights = await _scoreboardService.GetArrivalFlightsAsync(cityId, dateTime);
		return Ok(flights);
	}
}