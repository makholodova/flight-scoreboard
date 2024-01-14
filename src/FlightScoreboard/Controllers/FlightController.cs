using FlightScoreboard.Models;
using FlightScoreboardData.Services;
using FlightScoreboardData.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightScoreboard.Controllers;

public class FlightController : Controller

{
	private readonly IAirlineAirplaneService _airlineAirplaneService;
	private readonly IAirlineService _airlineService;
	private readonly ICityService _cityService;
	private readonly IFlightService _flightService;
	private readonly IPilotService _pilotService;

	public FlightController(IFlightService flightService, IPilotService pilotService, ICityService cityService,
		IAirlineService airlineService, IAirlineAirplaneService airlineAirplaneService)
	{
		_flightService = flightService;
		_pilotService = pilotService;
		_cityService = cityService;
		_airlineService = airlineService;
		_airlineAirplaneService = airlineAirplaneService;
	}

	public async Task<IActionResult> Index(FlightIndexFilterModel flight)
	{
		var flightModel = new FlightIndexIpModel();
		flightModel.Flight = flight;
		flightModel.Pilots = await _pilotService.GetAllPilotsAsync();
		flightModel.Airlines = await _airlineService.GetAvailableAirlinesAsync();
		flightModel.Airplanes = await _airlineAirplaneService.GetAllAirlineAirplanesAsync();
		flightModel.Cities = await _cityService.GetAllCitiesAsync();
		flightModel.Flights = await _flightService.GetAllFlightsAsync(flight);

		return View(flightModel);
	}

	[HttpGet]
	public async Task<IActionResult> Create()
	{
		var flights = new FlightCreateModelGet();
		flights.Pilots = await _pilotService.GetAllPilotsAsync();
		flights.Airlines = await _airlineService.GetAvailableAirlinesAsync();
		flights.Airplanes = await _airlineAirplaneService.GetAllAirlineAirplanesAsync();
		flights.Cities = await _cityService.GetAllCitiesAsync();

		return View(flights);
	}

	[HttpPost]
	public async Task<IActionResult> Create(FlightCreateModel flight)
	{
		await _flightService.CreateFlightAsync(flight);

		return RedirectToAction("Index");
	}

	[HttpGet]
	public async Task<IActionResult> CreateRepeatEvent()
	{
		var flights = new FlightCreateRepeatEventModelGet();
		flights.Pilots = await _pilotService.GetAllPilotsAsync();
		flights.Airlines = await _airlineService.GetAvailableAirlinesAsync();
		flights.Airplanes = await _airlineAirplaneService.GetAllAirlineAirplanesAsync();
		flights.Cities = await _cityService.GetAllCitiesAsync();
		return View(flights);
	}

	[HttpPost]
	public async Task<IActionResult> CreateRepeatEvent(FlightCreateRepeatEventModel flight)
	{
		var startTime = flight.StartDay;
		var currentDate = new DateTime(startTime.Year, startTime.Month, startTime.Day)
			.Add(flight.DepartureTime);
		while (currentDate <= flight.FinishDay.AddDays(1))
		{
			var day = flight.DaysOfWeek.Any(day => day == currentDate.DayOfWeek);
			if (day)
			{
				var flightModel = new FlightCreateModel
				{
					DepartureTime = currentDate,
					ArrivalTime = currentDate.Add(flight.DurationTime),
					ActualDepartureTime = flight.ActualDepartureTime,
					ActualArrivalTime = flight.ActualArrivalTime,
					CheckInStartTime = flight.CheckInStartTime,
					CheckInEndTime = flight.CheckInEndTime,
					BoardingStartTime = flight.BoardingStartTime,
					BoardingEndTime = flight.BoardingEndTime,
					FromCityId = flight.FromCityId,
					ToCityId = flight.ToCityId,
					PilotId = flight.PilotId,
					AirlineId = flight.AirlineId,
					AirlineAirplaneId = flight.AirlineAirplaneId,
					NumberOfFlight = flight.NumberOfFlight,
					ToGate = flight.ToGate,
					ToTerminal = flight.ToTerminal,
					FromGate = flight.FromGate,
					FromTerminal = flight.FromTerminal
				};

				await _flightService.CreateFlightAsync(flightModel);
			}

			currentDate = currentDate.AddDays(1);
		}

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