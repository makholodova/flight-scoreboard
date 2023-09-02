using FlightScoreboard.DateBase;
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

	public List<AirlineModel> GetAllAirline()
	{
		return this._context.Airline.Select(p => new AirlineModel
		{
			Id = p.Id,
			Name = p.Name
		}).ToList();
	}

	public int CreateAirline(AirlineCreateModel airline)
	{
		var addAirline = this._context.Airline.Add(new Airline
		{
			Name = airline.Name
		});
		this._context.SaveChanges();
		return addAirline.Entity.Id;
	}

	public bool DeleteAirline(int id)
	{
		var airline = this._context.Airline.FirstOrDefault(p => p.Id == id);
		if (airline == null) return false;

		this._context.Airline.Remove(airline);

		this._context.SaveChanges();
		return true;
	}
}