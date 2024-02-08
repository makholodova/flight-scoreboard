using System.Threading.Tasks;
using FlightScoreboardData.Services;
using FlightScoreboardData.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightScoreboardApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CityController : ControllerBase
{
	private readonly ICityService _cityService;

	public CityController(ICityService cityService)
	{
		_cityService = cityService;
	}

	[HttpGet]
	public async Task<IActionResult> All()
	{
		var cities = await _cityService.GetAllCitiesAsync();
		return Ok(cities);
	}

	[Route("{id:int}")]
	[HttpGet]
	public async Task<IActionResult> Get([FromRoute] int id)
	{
		var city = await _cityService.GetCityByIdAsync(id);
		return Ok(city);
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] CityCreateModel model)
	{
		var cityId = await _cityService.CreateCityAsync(model);
		return Ok(cityId);
	}

	[HttpPut]
	public async Task<IActionResult> Update([FromBody] CityUpdateModel city)
	{
		var result = await _cityService.UpdateCityAsync(city);
		if (result == false)
			return BadRequest("City is not found");
		return Ok();
	}

	[Route("{id:int}")]
	[HttpDelete]
	[ProducesResponseType(200)]
	[ProducesResponseType(400)]
	public async Task<IActionResult> Delete(int id)
	{
		var result = await _cityService.DeleteCityAsync(id);
		if (result == false)
			return BadRequest("City is not found");
		return Ok();
	}
}