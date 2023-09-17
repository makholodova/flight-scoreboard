using FlightScoreboard.DateBase;
using FlightScoreboard.Services.Interfaces;
using FlightScoreboard.Services.Models;

namespace FlightScoreboard.Services;

public class AirlineAirplaneService : IAirlineAirplaneService
{
	private readonly FlightScoreboardContext _context;

	public AirlineAirplaneService(FlightScoreboardContext context)
	{
		this._context = context;
	}

	public List<AirlineAirplaneShortModel> GetAllAAirlineAirplanes(int airlineId)
	{
		return this._context.AirlineAirplanes.Where(p => p.AirlineId == airlineId).Select(p =>
			new AirlineAirplaneShortModel
			{
				Id = p.Id,
				SerialNumber = p.SerialNumber,
				AirlineId = p.AirlineId,
				AirlineName = p.Airline.Name,
				AirplaneId = p.AirplaneId,
				AirplaneModel = p.Airplane.Model
			}).ToList();
	}

	public AirlineAirplaneModel GetAirplaneAirlineById(int id) // ?необходимость
	{
		var airplane = this._context.AirlineAirplanes.FirstOrDefault(p => p.Id == id);
		if (airplane == null) return null;

		return new AirlineAirplaneModel
		{
			Id = airplane.Id,
			SerialNumber = airplane.SerialNumber,
			AirlineId = airplane.AirlineId,
			AirplaneId = airplane.AirplaneId
		};
	}

	public int CreateAirplane(AirlineAirplaneCreateModel airplane)
	{
		var addAirplane = this._context.Add(new AirlineAirplane
		{
			SerialNumber = airplane.SerialNumber,
			AirlineId = airplane.AirlineId,
			AirplaneId = airplane.AirplaneId
		});

		this._context.SaveChanges();
		return addAirplane.Entity.Id;
	}

	public bool UpdateAirplane(AirlineAirplaneUpdateModel airplane)
	{
		var airplaneDb = this._context.AirlineAirplanes.FirstOrDefault(p => p.Id == airplane.Id);
		if (airplaneDb == null) return false;

		airplaneDb.SerialNumber = airplane.SerialNumber;
		airplaneDb.AirlineId = airplane.AirlineId;
		airplaneDb.AirplaneId = airplane.AirplaneId;

		this._context.SaveChanges();
		return true;
	}

	public bool DeleteAirplane(int id)
	{
		var airplane = this._context.AirlineAirplanes.FirstOrDefault(p => p.Id == id);
		if (airplane == null) return false;

		this._context.Remove(airplane);
		this._context.SaveChanges();
		return true;
	}
}