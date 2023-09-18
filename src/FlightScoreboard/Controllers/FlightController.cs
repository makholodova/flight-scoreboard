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
		this._flightService = flightService;
		this._pilotService = pilotService;
		this._cityService = cityService;
		this._airlineService = airlineService;
		this._airlineAirplaneService = airlineAirplaneService;
	}

	public IActionResult Index()
	{
		var flights = this._flightService.GetAllFlights();
		return this.View(flights);
	}

	[HttpGet]
	public IActionResult Create()
	{
		var flights = new FlightCreateModelGet();
		flights.Pilots = this._pilotService.GetAllPilots();
		flights.Airlines = this._airlineService.GetAvailableAirlines();
		flights.Airplanes = this._airlineAirplaneService.GetAllAirlineAirplanes();
		flights.Cities = this._cityService.GetAllCities();
		return this.View(flights);
	}

	[HttpPost]
	public IActionResult Create(FlightCreateModel flight)
	{
		//var flightAirline = this._airlineAirplaneService.GetAirplaneAirlineById(flight.AirlineAirplaneId);
		//flight.AirlineId = flightAirline.AirlineId;      
		this._flightService.CreateFlight(flight);

		return this.RedirectToAction("Index");
	}

	[HttpGet]
	public IActionResult Update(int id)
	{
		var flight = new FlightUpdateModelGet();
		flight.Flight = this._flightService.GetFlightById(id);
		flight.Cities = this._cityService.GetAllCities();
		flight.Pilots = this._pilotService.GetAllPilots();
		flight.Airplanes = this._airlineAirplaneService.GetAllAirlineAirplanes();
		flight.Airlines = this._airlineService.GetAvailableAirlines();
		return this.View(flight);
	}

	[HttpPost]
	public IActionResult Update(FlightUpdateModel flight)
	{
		var flightAirline = this._airlineAirplaneService.GetAirplaneAirlineById(flight.AirlineAirplaneId);
		flight.AirlineId = flightAirline.AirlineId;
		this._flightService.UpdateFlight(flight);
		return this.RedirectToAction("Index");
	}

	public IActionResult Delete(int id)
	{
		this._flightService.DeleteFlight(id);
		return this.RedirectToAction("Index");
	}
}