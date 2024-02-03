﻿namespace FlightScoreboardData.Services.Models;

public class PilotModel
{
	public int Id { get; set; }
	public string Name { get; set; }
	public string SurName { get; set; }
	public int Age { get; set; }
	public int AirlineId { get; set; }
	public string AirlineName { get; set; }
}