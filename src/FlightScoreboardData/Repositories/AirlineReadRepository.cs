using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightScoreboardData.DateBase;
using Microsoft.EntityFrameworkCore;

namespace FlightScoreboardData.Repositories;

public interface IAirlineReadRepository
{
	Task<List<Airline>> GetAirlinesAsync();
	Task<Airline> GetArlineAsync(int id);
}

public class AirlineReadRepository : IAirlineReadRepository
{
	private readonly FlightScoreboardContext _context;

	public AirlineReadRepository(FlightScoreboardContext context)
	{
		_context = context;
	}

	public async Task<Airline> GetArlineAsync(int id)
	{
		return await _context.Airlines.FindAsync(id);
	}

	public async Task<List<Airline>> GetAirlinesAsync()
	{
		return await _context.Airlines.Select(p => new Airline
		{
			Id = p.Id,
			Name = p.Name
		}).ToListAsync();
	}
}