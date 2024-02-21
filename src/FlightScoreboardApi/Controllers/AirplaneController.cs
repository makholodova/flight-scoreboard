using System.Threading.Tasks;
using FlightScoreboardData.Services;
using FlightScoreboardData.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightScoreboardApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AirplaneController : ControllerBase
{
	private readonly IAirplaneService _airplaneService;

	public AirplaneController(IAirplaneService airplaneService)
	{
		_airplaneService = airplaneService;
	}

	[HttpGet]
	public async Task<IActionResult> All()
	{
		var airplanes = await _airplaneService.GetAllAirplanesAsync();
		return Ok(airplanes);
	}

	[Route("{id:int}")]
	[HttpGet]
	public async Task<IActionResult> Get([FromRoute] int id)
	{
		var airplane = await _airplaneService.GetAirplaneByIdAsync(id);
		return Ok(airplane);
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] AirplaneCreateModel airplane)
	{
		var airplaneId = await _airplaneService.CreateAirplaneAsync(airplane);
		return Ok(airplaneId);
	}

	[HttpPut]
	[ProducesResponseType(200)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> Update([FromBody] AirplaneUpdateModel airplane)
	{
		var result = await _airplaneService.UpdateAirplaneAsync(airplane);
		if (result == false) return BadRequest("Airplane is not found");
		return Ok();
	}

	[Route("{id:int}")]
	[HttpDelete]
	[ProducesResponseType(200)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> Delete([FromRoute] int id)
	{
		var result = await _airplaneService.DeleteAirplaneAsync(id);
		if (result == false)
			return BadRequest("Airplane is not found");
		return Ok();
	}
}