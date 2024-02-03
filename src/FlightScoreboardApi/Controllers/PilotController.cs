using System.Threading.Tasks;
using FlightScoreboardData.Services;
using FlightScoreboardData.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightScoreboardApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PilotController : ControllerBase
{
	private readonly IAirlineService _airlineService;
	private readonly IPilotService _pilotService;

	public PilotController(IPilotService pilotService, IAirlineService airlineService)
	{
		_pilotService = pilotService;
		_airlineService = airlineService;
	}

	[HttpGet]
	public async Task<IActionResult> All()
	{
		var pilots = await _pilotService.GetAllPilotsAsync();
		return Ok(pilots);
	}

	[Route("{id:int}")]
	[HttpGet]
	public async Task<IActionResult> PilotById(int id) //добавила  новый
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
	public async Task<IActionResult> Delete(int id)
	{
		var result = await _pilotService.DeletePilotAsync(id);
		if (result == false)
			return BadRequest("Pilot is not found");
		return Ok();
	}
}