using System.Threading.Tasks;
using FlightScoreboardData.DateBase;
using Microsoft.EntityFrameworkCore;

namespace FlightScoreboardData.Repositories;

public interface IFlightWriteRepository
{
	Task<int> CreateFlightAsync(Flight flight);
	Task<bool> UpdateFlightAsync(Flight flight);
	Task<bool> DeleteFlightAsync(int id);
}

public class FlightWriteRepository : IFlightWriteRepository
{
	private readonly FlightScoreboardContext _context;

	public FlightWriteRepository(FlightScoreboardContext context)
	{
		_context = context;
	}

	public async Task<int> CreateFlightAsync(Flight flight)
	{
		var addFlight = await _context.Flights.AddAsync(flight);
		await _context.SaveChangesAsync();
		return addFlight.Entity.Id;
	}

	public async Task<bool> UpdateFlightAsync(Flight flight)
	{
		_context.Entry(flight).State = EntityState.Modified;
		await _context.SaveChangesAsync();
		return true;
	}

	public async Task<bool> DeleteFlightAsync(int id)
	{
		var flight = await _context.Flights.FindAsync(id);
		if (flight == null) return false;

		_context.Flights.Remove(flight);
		await _context.SaveChangesAsync();
		return true;
	}
}