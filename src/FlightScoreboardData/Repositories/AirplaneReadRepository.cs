using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightScoreboardData.DateBase;
using Microsoft.EntityFrameworkCore;

namespace FlightScoreboardData.Repositories;

public interface IAirplaneReadRepository
{
	Task<Airplane> GetAirplaneAsync(int id);
	Task<List<Airplane>> GetAirplanesAsync();
}

public class AirplaneReadRepository : IAirplaneReadRepository
{
	private readonly FlightScoreboardContext _context;

	public AirplaneReadRepository(FlightScoreboardContext context)
	{
		_context = context;
	}

	public async Task<Airplane> GetAirplaneAsync(int id)
	{
		return await _context.Airplanes.FindAsync(id);
	}

	public Task<List<Airplane>> GetAirplanesAsync()
	{
		return _context.Airplanes.Select(p => new Airplane
		{
			Id = p.Id,
			Model = p.Model
		}).ToListAsync();
	}
}