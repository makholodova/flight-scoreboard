using System.Threading.Tasks;
using FlightScoreboardApi.Models;
using FlightScoreboardData.DateBase;
using FlightScoreboardData.Repositories;

namespace FlightScoreboardApi.Services;

public interface IFlightService
{
	Task<int> CreateFlightAsync(FlightCreateModel flight);
	Task<bool> UpdateFlightAsync(FlightUpdateModel flight);
	Task<bool> DeleteFlightAsync(int id);
}

public class FlightService : IFlightService
{
	private readonly IFlightReadRepository _flightReadRepository;
	private readonly IFlightWriteRepository _flightWriteRepository;

	public FlightService(IFlightReadRepository flightReadRepository, IFlightWriteRepository flightWriteRepository)
	{
		_flightReadRepository = flightReadRepository;
		_flightWriteRepository = flightWriteRepository;
	}

	public async Task<int> CreateFlightAsync(FlightCreateModel flight)
	{
		return await _flightWriteRepository.CreateFlightAsync(new Flight
		{
			DepartureTime = flight.DepartureTime, //DateTime.Now
			ArrivalTime = flight.ArrivalTime, //DateTime.Now
			ActualArrivalTime = flight.ActualArrivalTime,
			ActualDepartureTime = flight.ActualDepartureTime,
			CheckInStartTime = flight.CheckInStartTime,
			CheckInEndTime = flight.CheckInEndTime,
			BoardingStartTime = flight.BoardingStartTime,
			BoardingEndTime = flight.BoardingEndTime,
			NumberOfFlight = flight.NumberOfFlight,
			ToGate = flight.ToGate,
			ToTerminal = flight.ToTerminal,
			FromGate = flight.FromGate,
			FromTerminal = flight.FromTerminal,
			FromCityId = flight.FromCityId,
			ToCityId = flight.ToCityId,
			PilotId = flight.PilotId,
			AirlineId = flight.AirlineId,
			AirlineAirplaneId = flight.AirlineAirplaneId
		});
	}

	public async Task<bool> UpdateFlightAsync(FlightUpdateModel flight)
	{
		var flightNew = await _flightReadRepository.GetFlightAsync(flight.Id);
		if (flightNew == null) return false;

		flightNew.ArrivalTime = flight.ArrivalTime;
		flightNew.DepartureTime = flight.DepartureTime;
		flightNew.FromCityId = flight.FromCityId;
		flightNew.ToCityId = flight.ToCityId;
		flightNew.PilotId = flight.PilotId;
		flightNew.AirlineId = flight.AirlineId;
		flightNew.AirlineAirplaneId = flight.AirplaneId;
		flightNew.NumberOfFlight = flight.NumberOfFlight;
		flightNew.ToTerminal = flight.ToTerminal;
		flightNew.ToGate = flight.ToGate;
		flightNew.FromTerminal = flight.FromTerminal;
		flightNew.FromGate = flight.FromGate;
		flightNew.ActualArrivalTime = flight.ActualArrivalTime;
		flightNew.ActualDepartureTime = flight.ActualDepartureTime;
		flightNew.CheckInStartTime = flight.CheckInStartTime;
		flightNew.CheckInEndTime = flight.CheckInEndTime;
		flightNew.BoardingStartTime = flight.BoardingStartTime;
		flightNew.BoardingEndTime = flight.BoardingEndTime;

		await _flightWriteRepository.UpdateFlightAsync(flightNew);
		return true;
	}

	public async Task<bool> DeleteFlightAsync(int id)
	{
		return await _flightWriteRepository.DeleteFlightAsync(id);
	}
}