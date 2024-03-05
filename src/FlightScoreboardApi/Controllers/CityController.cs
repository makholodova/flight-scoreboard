using System.Threading.Tasks;
using FlightScoreboardApi.Models;
using FlightScoreboardApi.Services;
using FlightScoreboardData.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FlightScoreboardApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CityController : ControllerBase
{
	private readonly ICityReadRepository _cityReadRepository;
	private readonly ICityService _cityService;

	public CityController(ICityService cityService, ICityReadRepository cityReadRepository)
	{
		_cityService = cityService;
		_cityReadRepository = cityReadRepository;
	}

	[HttpGet]
	public async Task<IActionResult> All()
	{
		var cities = await _cityReadRepository.GetCitiesAsync();
		return Ok(cities);
	}

	[Route("{cityId:int}")]
	[HttpGet]
	public async Task<IActionResult> Get([FromRoute] int cityId)
	{
		var city = await _cityReadRepository.GetCityAsync(cityId);
		return Ok(city);
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] CityCreateModel city)
	{
		var cityId = await _cityService.CreateCityAsync(city);
		return Ok(cityId);
	}

	[HttpPut]
	[ProducesResponseType(200)]
	[ProducesResponseType(400)]
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
		if (result == false) return BadRequest("City is not found");
		return Ok();
	}
}