using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using FlightScoreboard.Models;
using FlightScoreboardData.DateBase;
using FlightScoreboardData.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightScoreboard.Services;

[SuppressMessage("ReSharper", "EntityFramework.NPlusOne.IncompleteDataQuery")]
[SuppressMessage("ReSharper", "EntityFramework.NPlusOne.IncompleteDataUsage")]
public interface ICityService
{
	Task<List<CityModel>> GetAllCitiesAsync();
	Task<CityModel> GetCityByIdAsync(int id);
	Task<int> CreateCityAsync(CityCreateModel city);
	Task<bool> UpdateCityAsync(CityUpdateModel city);
	Task<bool> DeleteCityAsync(int id);
}

public class CityService : ICityService
{
	private readonly FlightScoreboardContext _context;

	public CityService(FlightScoreboardContext context)
	{
		_context = context;
	}

	public async Task<List<CityModel>> GetAllCitiesAsync()
	{
		return await _context.Cities.Select(p => new CityModel
		{
			Id = p.Id,
			Name = p.Name
		}).ToListAsync();
	}

	public async Task<CityModel> GetCityByIdAsync(int id)
	{
		var city = await _context.Cities.FirstOrDefaultAsync(p => p.Id == id);
		if (city == null) return null;
		var cityModel = new CityModel
		{
			Id = city.Id,
			Name = city.Name
		};
		return cityModel;
	}

	public async Task<int> CreateCityAsync(CityCreateModel city)
	{
		var addCity = await _context.Cities.AddAsync(new City { Name = city.Name });
		await _context.SaveChangesAsync();

		return addCity.Entity.Id;
	}

	public async Task<bool> UpdateCityAsync(CityUpdateModel city)
	{
		var cityDb = await _context.Cities.FirstOrDefaultAsync(p => p.Id == city.Id);
		if (cityDb == null) return false;
		cityDb.Name = city.Name;
		await _context.SaveChangesAsync();
		return true;
	}

	public async Task<bool> DeleteCityAsync(int id)
	{
		var city = await _context.Cities.FirstOrDefaultAsync(p => p.Id == id);

		if (city == null || city.FromFlights.Any() || city.ToFlights.Any())
			return false;

		_context.Cities.Remove(city);

		await _context.SaveChangesAsync();

		return true;
	}
}