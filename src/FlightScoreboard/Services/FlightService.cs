using FlightScoreboard.DateBase;
using FlightScoreboard.Services.Interfaces;
using FlightScoreboard.Services.Models;

namespace FlightScoreboard.Services;

public class FlightService : IFlightService
{
	private readonly FlightScoreboardContext _context;

	public FlightService(FlightScoreboardContext context)
	{
		this._context = context;
	}

	public List<FlightIndexModel> GetAllFlight()
	{
		return this._context.Flight.Select(p => new FlightIndexModel
		{
			Id = p.Id,
			Time = p.Time,
			FromCity = p.FromCity,
			ToCity = p.ToCity,
			PilotName = p.Pilot.Name,
			AirlineName = p.Airline.Name,
			AirplaneByAirlineModel = p.AirplaneByAirline.Airplane.Model,
			AirplaneByAirlineSerialNumber = p.AirplaneByAirline.SerialNumber
		}).ToList();
	}

	public FlightModel GetFlightById(int id)
	{
		var flightDb = this._context.Flight.FirstOrDefault(p => p.Id == id);
		if (flightDb == null) return null;
		return new FlightModel
		{
			Id = flightDb.Id,
			Time = flightDb.Time,
			FromCityId = flightDb.FromCityId,
			ToCityId = flightDb.ToCityId,
			PilotId = flightDb.PilotId,
			AirlineId = flightDb.AirlineId,
			AirplaneByAirlineId = flightDb.AirplaneByAirlineId
		};
	}

	public int CreateFlight(FlightCreateModel flight)
	{
		var addFlight = this._context.Flight.Add(new Flight
		{
			Time = flight.Time,
			FromCityId = flight.FromCityId,
			ToCityId = flight.ToCityId,
			PilotId = flight.PilotId,
			AirlineId = flight.AirlineId,
			AirplaneByAirlineId = flight.AirplaneByAirlineId
		});
		this._context.SaveChanges();
		return addFlight.Entity.Id;
	}

	public bool UpdateFlight(FlightUpdateModel flight)
	{
		var flightNew = this._context.Flight.FirstOrDefault(p => p.Id == flight.Id);
		if (flightNew == null) return false;

		flightNew.Id = flight.Id;
		flightNew.Time = flight.Time;
		flightNew.FromCityId = flight.FromCityId;
		flightNew.ToCityId = flight.ToCityId;
		flightNew.PilotId = flight.PilotId;
		flightNew.AirlineId = flight.AirlineId;
		flightNew.AirplaneByAirlineId = flight.AirplaneByAirlineId;

		this._context.SaveChanges();
		return true;
	}

	public bool DeleteFlight(int id)
	{
		var flight = this._context.Flight.FirstOrDefault(p => p.Id == id);
		if (flight == null) return false;

		this._context.Flight.Remove(flight);
		this._context.SaveChanges();
		return true;
	}
}