using System.Threading.Tasks;
using FlightScoreboardApi.Models;
using FlightScoreboardApi.Services;
using FlightScoreboardData.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FlightScoreboardApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightController : ControllerBase

{
	private readonly IFlightReadRepository _flightReadRepository;
	private readonly IFlightService _flightService;

	public FlightController(IFlightService flightService, IFlightReadRepository flightReadRepository)
	{
		_flightService = flightService;
		_flightReadRepository = flightReadRepository;
	}

	[HttpGet]
	public async Task<IActionResult> All()
	{
		var flights = await _flightReadRepository.GetFlightsWithDetailsAsync();
		return Ok(flights);
	}

	[Route("{flightId:int}")]
	[HttpGet]
	public async Task<IActionResult> Get([FromRoute] int flightId)
	{
		var flight = await _flightReadRepository.GetFlightWithDetailsAsync(flightId);
		return Ok(flight);
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] FlightCreateModel flight)
	{
		var flightId = await _flightService.CreateFlightAsync(flight);
		return Ok(flightId);
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
}