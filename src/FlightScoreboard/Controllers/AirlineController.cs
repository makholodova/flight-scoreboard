using FlightScoreboard.Services.Interfaces;
using FlightScoreboard.Services.Models;
using Microsoft.AspNetCore.Mvc;

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
        await _airlineService.DeleteAirlineAsync(id);
        return RedirectToAction("Index");
    }
}