using FlightScoreboard.DateBase;
using FlightScoreboard.Models;
using FlightScoreboard.Services.Interfaces;
using FlightScoreboard.Services.Models;

namespace FlightScoreboard.Services;

public class AirlineService : IAirlineService
{
	private readonly FlightScoreboardContext _context;

	public AirlineService(FlightScoreboardContext context)
	{
		this._context = context;
	}

	public List<AirlineModel> GetAllAirlines()
	{
		return this._context.Airlines.Select(p => new AirlineModel
		{
			Id = p.Id,
			Name = p.Name
		}).ToList();
	}

	public List<AirlineShortInfoModel> GetAvailableAirlines()
	{
		return this._context.Airlines.Select(p => new AirlineShortInfoModel
		{
			Id = p.Id,
			Name = p.Name
		}).ToList();
	}

	public int CreateAirline(AirlineCreateModel airline)
	{
		var addAirline = this._context.Airlines.Add(new Airline
		{
			Name = airline.Name
		});
		this._context.SaveChanges();
		return addAirline.Entity.Id;
	}

	public bool DeleteAirline(int id)
	{
		var airline = this._context.Airlines.FirstOrDefault(p => p.Id == id);
		if (airline == null) return false;

		this._context.Airlines.Remove(airline);

		this._context.SaveChanges();
		return true;
	}
}