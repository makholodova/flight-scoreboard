using System.Threading.Tasks;
using FlightScoreboardData.Services;
using FlightScoreboardData.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightScoreboardApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AirlineController : ControllerBase
{
	private readonly IAirlineService _airlineService;

	public AirlineController(IAirlineService airlineService)
	{
		_airlineService = airlineService;
	}

	[HttpGet]
	public async Task<IActionResult> All()
	{
		var airlines = await _airlineService.GetAllAirlinesAsync();
		return Ok(airlines);
	}

	[Route("{id:int}")]
	[HttpGet]
	public async Task<IActionResult> Get([FromRoute] int id)
	{
		var airline = await _airlineService.GetArlineByIdAsync(id);
		return Ok(airline);
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] AirlineCreateModel airline)
	{
		var airlineId = await _airlineService.CreateAirlineAsync(airline);
		return Ok(airlineId);
	}

	[HttpPut]
	public async Task<IActionResult> Update([FromBody] AirlineUpdateModel airline)
	{
		var result = await _airlineService.UpdateArlineAsync(airline);
		if (result == false)
			return BadRequest("Airline is not found");
		return Ok();
	}

	[Route("{id:int}")]
	[HttpDelete]
	[ProducesResponseType(200)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> Delete(int id)
	{
		var result = await _airlineService.DeleteAirlineAsync(id);
		if (result == false)
			return BadRequest("Airline is not found");
		return Ok();
	}
}