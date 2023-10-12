using FlightScoreboard.Models;
using FlightScoreboard.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightScoreboard.Controllers;

public class ScoreboardController : Controller
{
    private readonly ICityService _cityService;
    private readonly IScoreboardService _scoreboardService;

    public ScoreboardController(ICityService cityService, IScoreboardService scoreboardService)
    {
        _cityService = cityService;
        _scoreboardService = scoreboardService;
    }

    public async Task<IActionResult> Index(int? cityId, DateTime? dateTime)
    {
        var flightModel = new ScoreboardIndexIpModel();
        flightModel.CityId = cityId;
        flightModel.DateTime = dateTime;
        flightModel.Cities = await _cityService.GetAllCitiesAsync();
        flightModel.Flights = await _scoreboardService.GetAllFlightsAsync(cityId, dateTime);
        return View(flightModel);
    }
}