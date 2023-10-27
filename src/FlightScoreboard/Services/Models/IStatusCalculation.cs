namespace FlightScoreboard.Services.Models;

public interface IStatusCalculation
{
    DateTime DepartureTime { get; set; }
    DateTime ArrivalTime { get; set; }
    DateTime? ActualDepartureTime { get; set; }
    DateTime? ActualArrivalTime { get; set; }
    DateTime? CheckInStartTime { get; set; }
    DateTime? CheckInEndTime { get; set; }
    DateTime? BoardingStartTime { get; set; }
    DateTime? BoardingEndTime { get; set; }
}