using FlightScoreboard.DateBase;
using FlightScoreboard.Services.Interfaces;
using FlightScoreboard.Services.Models;

namespace FlightScoreboard.Services;

public class AirplaneByAirlineService : IAirplaneByAirlineService
{
	private readonly FlightScoreboardContext _context;

	public AirplaneByAirlineService(FlightScoreboardContext context)
	{
		this._context = context;
	}

	public List<AirplaneByAirlineIndexModel> GetAllAirplaneByAirline()
	{
		return this._context.AirplaneByAirline.Select(p => new AirplaneByAirlineIndexModel
		{
			Id = p.Id,
			SerialNumber = p.SerialNumber,
			AirlineId = p.AirlineId,
			AirlineName = p.Airline.Name,
			AirplaneId = p.AirplaneId,
			AirplaneModel = p.Airplane.Model
		}).ToList();
	}

	public AirplaneByAirlineModel GetAirplaneByAirlineById(int id)
	{
		var airplaneByAirline = this._context.AirplaneByAirline.FirstOrDefault(p => p.Id == id);
		if (airplaneByAirline == null) return null;

		return new AirplaneByAirlineModel
		{
			Id = airplaneByAirline.Id,
			SerialNumber = airplaneByAirline.SerialNumber,
			AirlineId = airplaneByAirline.AirlineId,
			AirplaneId = airplaneByAirline.AirplaneId
		};
	}

	public int CreateAirplaneByAirline(AirplaneByAirlineCreateModel airplaneByAirline)
	{
		var addAirplaneByAirline = this._context.Add(new AirplaneByAirline
		{
			SerialNumber = airplaneByAirline.SerialNumber,
			AirlineId = airplaneByAirline.AirlineId,
			AirplaneId = airplaneByAirline.AirplaneId
		});

		this._context.SaveChanges();
		return addAirplaneByAirline.Entity.Id;
	}

	public bool UpdateAirplaneByAirline(AirplaneByAirlineUpdateModel airplaneByAirline)
	{
		var airplaneByAirlineDb = this._context.AirplaneByAirline.FirstOrDefault(p => p.Id == airplaneByAirline.Id);
		if (airplaneByAirlineDb == null) return false;

		airplaneByAirlineDb.SerialNumber = airplaneByAirline.SerialNumber;
		airplaneByAirlineDb.AirlineId = airplaneByAirline.AirlineId;
		airplaneByAirlineDb.AirplaneId = airplaneByAirline.AirplaneId;

		this._context.SaveChanges();
		return true;
	}

	public bool DeleteAirplaneByAirline(int id)
	{
		var airplaneByAirline = this._context.AirplaneByAirline.FirstOrDefault(p => p.Id == id);
		if (airplaneByAirline == null) return false;

		this._context.Remove(airplaneByAirline);
		this._context.SaveChanges();
		return true;
	}
}