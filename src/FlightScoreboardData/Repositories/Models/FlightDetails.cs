﻿using System;

namespace FlightScoreboardData.Repositories.Models;

public class FlightDetails
{
	public int Id { get; set; }
	public DateTime DepartureTime { get; set; }
	public DateTime ArrivalTime { get; set; }

	public DateTime? ActualDepartureTime { get; set; }
	public DateTime? ActualArrivalTime { get; set; }
	public DateTime? CheckInStartTime { get; set; }
	public DateTime? CheckInEndTime { get; set; }
	public DateTime? BoardingStartTime { get; set; }
	public DateTime? BoardingEndTime { get; set; }
	public string NumberOfFlight { get; set; }
	public string ToGate { get; set; }
	public string ToTerminal { get; set; }
	public string FromGate { get; set; }
	public string FromTerminal { get; set; }

	public int FromCityId { get; set; }
	public string FromCity { get; set; }
	public int ToCityId { get; set; }
	public string ToCity { get; set; }
	public int PilotId { get; set; }
	public string PilotFullName { get; set; }

	public int AirlineId { get; set; }
	public string AirlineName { get; set; }
	public string AirplaneModel { get; set; }
	public int AirplaneId { get; set; }
	public int AirplaneSerialNumber { get; set; }
}