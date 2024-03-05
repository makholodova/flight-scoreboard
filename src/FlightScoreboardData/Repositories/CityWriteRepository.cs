using System.Threading.Tasks;
using FlightScoreboardData.DateBase;
using Microsoft.EntityFrameworkCore;

namespace FlightScoreboardData.Repositories;

public interface ICityWriteRepository
{
	Task<int> CreateCityAsync(City city);
	Task<bool> UpdateCityAsync(City city);
	Task<bool> DeleteCityAsync(int id);
}

public class CityWriteRepository : ICityWriteRepository
{
	private readonly FlightScoreboardContext _context;

	public CityWriteRepository(FlightScoreboardContext context)
	{
		_context = context;
	}

	public async Task<int> CreateCityAsync(City city)
	{
		var addCity = await _context.Cities.AddAsync(city);
		await _context.SaveChangesAsync();
		return addCity.Entity.Id;
	}

	public async Task<bool> UpdateCityAsync(City city)
	{
		_context.Entry(city).State = EntityState.Modified; //что это
		await _context.SaveChangesAsync();
		return true;
	}

	public async Task<bool> DeleteCityAsync(int id)
	{
		var city = await _context.Cities.FindAsync(id);
		if (city == null) return false;
		
		_context.Cities.Remove(city);
		await _context.SaveChangesAsync();
		return true;
	}
}