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

	public List<FlightIndexModel> GetAllFlights()
	{
		return this._context.Flights.Select(p => new FlightIndexModel
		{
			Id = p.Id,
			ArrivalTime = p.ArrivalTime,
			DepartureTime = p.DepartureTime,
			FromCity = p.FromCity.Name,
			ToCity = p.ToCity.Name,
			PilotFullName = p.Pilot.Name+" "+ p.Pilot.SurName,
			PilotId = p.PilotId,
			AirlineName = p.Airline.Name,
			AirlineId = p.AirlineId,
			AirplaneModel = p.AirlineAirplane.Airplane.Model,
			AirplaneId = p.AirlineAirplaneId,
			AirplaneSerialNumber = p.AirlineAirplane.SerialNumber
		}).ToList();
	}

	public FlightModel GetFlightById(int id)
	{
		var flightDb = this._context.Flights.FirstOrDefault(p => p.Id == id);
		if (flightDb == null) return null;
		return new FlightModel
		{
			Id = flightDb.Id,
			ArrivalTime = flightDb.ArrivalTime,
			DepartureTime = flightDb.DepartureTime,
			FromCityId = flightDb.FromCityId,
			ToCityId = flightDb.ToCityId,
			PilotId = flightDb.PilotId,
			AirlineId = flightDb.AirlineId,
			AirlineAirplaneId = flightDb.AirlineAirplaneId
		};
	}

	public int CreateFlight(FlightCreateModel flight)
	{
		var addFlight = this._context.Flights.Add(new Flight
		{
			ArrivalTime = flight.ArrivalTime,
			DepartureTime = flight.DepartureTime,
			FromCityId = flight.FromCityId,
			ToCityId = flight.ToCityId,
			PilotId = flight.PilotId,
			AirlineId = flight.AirlineId,
			AirlineAirplaneId = flight.AirlineAirplaneId
		});
		this._context.SaveChanges();
		return addFlight.Entity.Id;
	}

	public bool UpdateFlight(FlightUpdateModel flight)
	{
		var flightNew = this._context.Flights.FirstOrDefault(p => p.Id == flight.Id);
		if (flightNew == null) return false;

		flightNew.ArrivalTime = flight.ArrivalTime;
		flightNew.DepartureTime = flight.DepartureTime;
		flightNew.FromCityId = flight.FromCityId;
		flightNew.ToCityId = flight.ToCityId;
		flightNew.PilotId = flight.PilotId;
		flightNew.AirlineId = flight.AirlineId;
		flightNew.AirlineAirplaneId = flight.AirlineAirplaneId;

		this._context.SaveChanges();
		return true;
	}

	public bool DeleteFlight(int id)
	{
		var flight = this._context.Flights.FirstOrDefault(p => p.Id == id);
		if (flight == null) return false;

		this._context.Flights.Remove(flight);
		this._context.SaveChanges();
		return true;
	}
}