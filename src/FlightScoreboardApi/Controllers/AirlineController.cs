using System.Threading.Tasks;
using FlightScoreboardApi.Models;
using FlightScoreboardApi.Services;
using FlightScoreboardData.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FlightScoreboardApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AirlineController : ControllerBase
{
	private readonly IAirlineAirplaneReadRepository _airlineAirplaneReadRepository;
	private readonly IAirlineAirplaneService _airlineAirplaneService;
	private readonly IAirlineReadRepository _airlineReadRepository;
	private readonly IAirlineService _airlineService;


	public AirlineController(IAirlineService airlineService, IAirlineAirplaneService airlineAirplaneService,
		IAirlineReadRepository airlineReadRepository, IAirlineAirplaneReadRepository airlineAirplaneReadRepository)
	{
		_airlineService = airlineService;
		_airlineAirplaneService = airlineAirplaneService;
		_airlineReadRepository = airlineReadRepository;
		_airlineAirplaneReadRepository = airlineAirplaneReadRepository;
	}

	[HttpGet]
	public async Task<IActionResult> All()
	{
		var airlines = await _airlineReadRepository.GetAirlinesAsync();
		return Ok(airlines);
	}

	[Route("{id:int}")]
	[HttpGet]
	public async Task<IActionResult> Get([FromRoute] int id)
	{
		var airline = await _airlineReadRepository.GetArlineAsync(id);
		return Ok(airline);
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] AirlineCreateModel airline)
	{
		var airlineId = await _airlineService.CreateAirlineAsync(airline);
		return Ok(airlineId);
	}

	[HttpPut]
	[ProducesResponseType(200)]
	[ProducesResponseType(400)]
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

	[Route("{airlineId:int}/airplanes")]
	[HttpGet]
	public async Task<IActionResult> Airplanes([FromRoute] int airlineId)
	{
		var airplanes = await _airlineAirplaneReadRepository.GetAirlineAirplanesWithDetailsAsync(airlineId);
		return Ok(airplanes);
	}


	[Route("{airlineId:int}/airplane/{airplaneId:int}")]
	[HttpGet]
	public async Task<IActionResult> GetAirplane([FromRoute] int airplaneId)
	{
		var airplane = await _airlineAirplaneReadRepository.GetAirplaneAirlineWithDetailsAsync(airplaneId);
		return Ok(airplane);
	}

	[Route("{airlineId:int}/airplane")]
	[HttpPost]
	public async Task<IActionResult> CreateAirplane([FromRoute] int airlineId, [FromBody] AirlineAirplaneCreateModel airplane)
	{
		var airplaneId = await _airlineAirplaneService.CreateAirplaneAsync(airlineId, airplane);
		return Ok(airplaneId);
	}

	[Route("{airlineId:int}/airplane")]
	[HttpPut]
	[ProducesResponseType(200)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> UpdateAirplane([FromBody] AirlineAirplaneUpdateModel airplane)
	{
		var result = await _airlineAirplaneService.UpdateAirplaneAsync(airplane);
		if (result == false)
			return BadRequest("Airplane is not found");
		return Ok();
	}

	[Route("{airlineId:int}/airplane/{airplaneId:int}")]
	[HttpDelete]
	[ProducesResponseType(200)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> DeleteAirplane(int airplaneId)
	{
		var result = await _airlineAirplaneService.DeleteAirplaneAsync(airplaneId);
		if (result == false)
			return BadRequest("Airplane is not found");
		return Ok();
	}
}