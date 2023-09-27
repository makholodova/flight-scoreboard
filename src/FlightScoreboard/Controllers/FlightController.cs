using FlightScoreboard.Models;
using FlightScoreboard.Services.Interfaces;
using FlightScoreboard.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightScoreboard.Controllers;

public class FlightController : Controller

{
    private readonly IFlightService _flightService;
    private readonly IPilotService _pilotService;
    private readonly ICityService _cityService;
    private readonly IAirlineService _airlineService;
    private readonly IAirlineAirplaneService _airlineAirplaneService;

    public FlightController(IFlightService flightService, IPilotService pilotService, ICityService cityService,
        IAirlineService airlineService, IAirlineAirplaneService airlineAirplaneService)
    {
        _flightService = flightService;
        _pilotService = pilotService;
        _cityService = cityService;
        _airlineService = airlineService;
        _airlineAirplaneService = airlineAirplaneService;
    }

    public async Task<IActionResult> Index()
    {
        var flights = await _flightService.GetAllFlightsAsync();
        return View(flights);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var flights = new FlightCreateModelGet();
        flights.Pilots = await _pilotService.GetAllPilotsAsync();
        flights.Airlines = await _airlineService.GetAvailableAirlinesAsync();
        flights.Airplanes = await _airlineAirplaneService.GetAllAirlineAirplanesAsync();
        flights.Cities = await _cityService.GetAllCitiesAsync();

        //await Task.WhenAll(pilotsTask, airlinesTask, airplanesTask, citiesTask); //Task.WaitAll();  

        return View(flights);
    }

    [HttpPost]
    public async Task<IActionResult> Create(FlightCreateModel flight)
    {
        await _flightService.CreateFlightAsync(flight);

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var flight = new FlightUpdateModelGet();
        flight.Flight = await _flightService.GetFlightByIdAsync(id);
        flight.Cities = await _cityService.GetAllCitiesAsync();
        flight.Pilots = await _pilotService.GetAllPilotsAsync();
        flight.Airplanes = await _airlineAirplaneService.GetAllAirlineAirplanesAsync();
        flight.Airlines = await _airlineService.GetAvailableAirlinesAsync();

        // await Task.WhenAll(flightTask, citiesTask,pilotsTask,airplanesTask,airlinesTask);  

        return View(flight);
    }

    [HttpPost]
    public async Task<IActionResult> Update(FlightUpdateModel flight)
    {
        var flightAirline = await _airlineAirplaneService.GetAirplaneAirlineByIdAsync(flight.AirlineAirplaneId);
        flight.AirlineId = flightAirline.AirlineId;
        await _flightService.UpdateFlightAsync(flight);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        await _flightService.DeleteFlightAsync(id);
        return RedirectToAction("Index");
    }
}