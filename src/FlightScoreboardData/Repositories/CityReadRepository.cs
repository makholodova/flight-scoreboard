using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightScoreboardData.DateBase;
using Microsoft.EntityFrameworkCore;

namespace FlightScoreboardData.Repositories;

public interface ICityReadRepository
{
	Task<City> GetCityAsync(int id);
	Task<List<City>> GetCitiesAsync();
}

public class CityReadRepository : ICityReadRepository
{
	private readonly FlightScoreboardContext _context;

	public CityReadRepository(FlightScoreboardContext context)
	{
		_context = context;
	}

	public async Task<City> GetCityAsync(int id) 
	{
		return await _context.Cities.FindAsync(id);
	}

	public async Task<List<City>> GetCitiesAsync()
	{
		return await _context.Cities.Select(p => new City
		{
			Id = p.Id,
			Name = p.Name
		}).ToListAsync();
	}
}