using System.Threading.Tasks;
using FlightScoreboardData.Services;
using FlightScoreboardData.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightScoreboardApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightController : ControllerBase

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

	[HttpGet]
	public async Task<IActionResult> All()
	{
		var flights = await _flightService.GetAllFlightsAsync();

		return Ok(flights);
	}

	[Route("{flightId:int}")]
	[HttpGet]
	public async Task<IActionResult> Flight([FromRoute] int flightId)
	{
		var flight = await _flightService.GetFlightByIdAsync(flightId);

		return Ok(flight);
	}


	[HttpPost]
	public async Task<IActionResult> Create([FromBody] FlightCreateModel flight)
	{
		var flightId = await _flightService.CreateFlightAsync(flight);

		return Ok(flightId);
	}


	[Route("{id:int}")]
	[HttpDelete]
	[ProducesResponseType(200)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> Delete(int id)
	{
		var result = await _flightService.DeleteFlightAsync(id);
		if (result == false) return BadRequest("Flight is not found");
		return Ok();
	}

	[HttpPut]
	[ProducesResponseType(200)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> Update(FlightUpdateModel flight)
	{
		var result = await _flightService.UpdateFlightAsync(flight);
		if (result == false)
			return BadRequest("Flight is not found");
		return Ok();
	}
}