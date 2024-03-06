using System.Threading.Tasks;
using FlightScoreboardData.DateBase;
using Microsoft.EntityFrameworkCore;

namespace FlightScoreboardData.Repositories;

public interface IAirlineWriteRepository
{
	Task<int> CreateAirlineAsync(Airline airline);
	Task<bool> UpdateArlineAsync(Airline airline);
	Task<bool> DeleteAirlineAsync(int id);
}

public class AirlineWriteRepository : IAirlineWriteRepository
{
	private readonly FlightScoreboardContext _context;

	public AirlineWriteRepository(FlightScoreboardContext context)
	{
		_context = context;
	}

	public async Task<int> CreateAirlineAsync(Airline airline)
	{
		var addAirline = await _context.Airlines.AddAsync(airline);
		await _context.SaveChangesAsync();
		return addAirline.Entity.Id;
	}

	public async Task<bool> UpdateArlineAsync(Airline airline)
	{
		_context.Entry(airline).State = EntityState.Modified;
		await _context.SaveChangesAsync();
		return true;
	}

	public async Task<bool> DeleteAirlineAsync(int id)
	{
		var airline = await _context.Airlines.FindAsync(id);
		if (airline == null) return false;

		_context.Airlines.Remove(airline);
		await _context.SaveChangesAsync();
		return true;
	}
}