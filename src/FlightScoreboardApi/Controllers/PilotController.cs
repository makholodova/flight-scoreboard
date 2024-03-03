using System.Threading.Tasks;
using FlightScoreboardData.Services;
using FlightScoreboardData.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightScoreboardApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PilotController : ControllerBase
{
	private readonly IPilotService _pilotService;

	public PilotController(IPilotService pilotService)
	{
		_pilotService = pilotService;
	}

	[HttpGet]
	public async Task<IActionResult> All([FromQuery] int? airlineId = null)
	{
		var pilots = await _pilotService.GetAllPilotsAsync(airlineId);
		return Ok(pilots);
	}

	[Route("{id:int}")]
	[HttpGet]
	public async Task<IActionResult> Get([FromRoute] int id)
	{
		var pilot = await _pilotService.GetPilotByIdAsync(id);
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