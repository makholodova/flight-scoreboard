using System.Threading.Tasks;
using FlightScoreboardApi.Models;
using FlightScoreboardApi.Services;
using FlightScoreboardData.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace FlightScoreboardApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PilotController : ControllerBase
{
	private readonly IPilotReadRepository _pilotReadRepository;
	private readonly IPilotService _pilotService;

	public PilotController(IPilotService pilotService, IPilotReadRepository pilotReadRepository)
	{
		_pilotService = pilotService;
		_pilotReadRepository = pilotReadRepository;
	}

	[HttpGet]
	public async Task<IActionResult> All([FromQuery] int? airlineId = null)
	{
		var pilots = await _pilotReadRepository.GetPilotsWithDetailsAsync(airlineId);
		return Ok(pilots);
	}

	[Route("{id:int}")]
	[HttpGet]
	public async Task<IActionResult> Get([FromRoute] int id)
	{
		var pilot = await _pilotReadRepository.GetPilotWithDetailsAsync(id);
		return Ok(pilot);
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] PilotCreateModel pilotNew)
	{
		var pilotId = await _pilotService.CreatePilotAsync(pilotNew);
		return Ok(pilotId);
	}

	[HttpPut]
	[ProducesResponseType(200)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> Update([FromBody] PilotUpdateModel pilot)
	{
		var result = await _pilotService.UpdatePilotAsync(pilot);
		if (result == false)
			return BadRequest("Pilot is not found");
		return Ok();
	}

	[Route("{id:int}")]
	[HttpDelete]
	[ProducesResponseType(200)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> Delete([FromRoute] int id)
	{
		var result = await _pilotService.DeletePilotAsync(id);
		if (result == false)
			return BadRequest("Pilot is not found");
		return Ok();
	}
}