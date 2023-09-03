using FlightScoreboard.DateBase;
using FlightScoreboard.Services.Interfaces;
using FlightScoreboard.Services.Models;

namespace FlightScoreboard.Services;

public class AirplaneService : IAirplaneService
{
	private readonly FlightScoreboardContext _context;

	public AirplaneService(FlightScoreboardContext context)
	{
		this._context = context;
	}

	public List<AirplaneModel> GetAllAirplane()
	{
		return this._context.Airplane.Select(p => new AirplaneModel
		{
			Id = p.Id,
			Model = p.Model
		}).ToList();
	}

	public int CreateAirplane(AirplaneCreateModel airplane)
	{
		var addAirplane = this._context.Airplane.Add(new Airplane { Model = airplane.Model });
		this._context.SaveChanges();
		return addAirplane.Entity.Id;
	}

	public bool DeleteAirplane(int id)
	{
		var airplane = this._context.Airplane.FirstOrDefault(p => p.Id == id);
		if (airplane == null) return false;

		this._context.Remove(airplane);
		this._context.SaveChanges();

		return true;
	}
}