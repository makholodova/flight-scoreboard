﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using FlightScoreboardData.DateBase;
using FlightScoreboardData.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightScoreboardData.Services;

[SuppressMessage("ReSharper", "EntityFramework.NPlusOne.IncompleteDataUsage")]
[SuppressMessage("ReSharper", "EntityFramework.NPlusOne.IncompleteDataQuery")]
public interface IAirplaneService
{
	Task<List<AirplaneModel>> GetAllAirplanesAsync();
	Task<int> CreateAirplaneAsync(AirplaneCreateModel airplane);
	Task<bool> DeleteAirplaneAsync(int id);
}

public class AirplaneService : IAirplaneService
{
	private readonly FlightScoreboardContext _context;

	public AirplaneService(FlightScoreboardContext context)
	{
		_context = context;
	}

	public Task<List<AirplaneModel>> GetAllAirplanesAsync()
	{
		return _context.Airplanes.Select(p => new AirplaneModel
		{
			Id = p.Id,
			Model = p.Model
		}).ToListAsync();
	}


	public async Task<int> CreateAirplaneAsync(AirplaneCreateModel airplane)
	{
		var addAirplane = await _context.Airplanes.AddAsync(new Airplane { Model = airplane.Model });
		await _context.SaveChangesAsync();
		return addAirplane.Entity.Id;
	}

	public async Task<bool> DeleteAirplaneAsync(int id)
	{
		var airplane = await _context.Airplanes.FirstOrDefaultAsync(p => p.Id == id);
		if (airplane == null || airplane.AirlineAirplanes.Any()) return false;

		_context.Remove(airplane);
		await _context.SaveChangesAsync();

		return true;
	}
}