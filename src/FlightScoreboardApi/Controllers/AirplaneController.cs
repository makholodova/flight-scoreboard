using System.Threading.Tasks;
using FlightScoreboardApi.Services;
using FlightScoreboardData.Repositories;
using Microsoft.AspNetCore.Mvc;
using AirplaneCreateModel = FlightScoreboardApi.Models.AirplaneCreateModel;
using AirplaneUpdateModel = FlightScoreboardApi.Models.AirplaneUpdateModel;

namespace FlightScoreboardApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AirplaneController : ControllerBase
{
	private readonly IAirplaneService _airplaneService;
	private readonly IAirplaneReadRepository _airplaneReadRepository;

	public AirplaneController(IAirplaneService airplaneService, IAirplaneReadRepository airplaneReadRepository)
	{
		_airplaneService = airplaneService;
		_airplaneReadRepository = airplaneReadRepository;
	}

	[HttpGet]
	public async Task<IActionResult> All()
	{
		var airplanes = await _airplaneReadRepository.GetAirplanesAsync();
		return Ok(airplanes);
	}

	[Route("{airplaneId:int}")]
	[HttpGet]
	public async Task<IActionResult> Get([FromRoute] int airplaneId)
	{
		var airplane = await _airplaneReadRepository.GetAirplaneAsync(airplaneId);
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