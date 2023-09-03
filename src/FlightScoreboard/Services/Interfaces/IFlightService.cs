using FlightScoreboard.Services.Models;

namespace FlightScoreboard.Services.Interfaces;

public interface IFlightService
{
	List<FlightIndexModel> GetAllFlights();
	FlightModel GetFlightById(int id);
	int CreateFlight(FlightCreateModel flight);
	bool UpdateFlight(FlightUpdateModel flight);
	bool DeleteFlight(int id);
}