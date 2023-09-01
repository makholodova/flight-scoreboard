using FlightScoreboard.DateBase;
using FlightScoreboard.Services.Interfaces;
using FlightScoreboard.Services.Models;

namespace FlightScoreboard.Services;

public class PilotService : IPilotService
{
	private readonly FlightScoreboardContext _context;

	public PilotService(FlightScoreboardContext context)
	{
		this._context = context;
	}


	public List<PilotIndexModel> GetAllPilot()
	{
		return this._context.Pilot.Select(p => new PilotIndexModel
		{
			Id = p.Id,
			Name = p.Name,
			SurName = p.SurName,
			Age = p.Age,
			AirlineId = p.AirlineId,
			AirlineName = p.Airline.Name
		}).ToList();
	}


	public PilotModel GetPilotById(int id)
	{
		var pilot = this._context.Pilot.FirstOrDefault(p => p.Id == id);
		if (pilot == null) return null;

		var pilotModel = new PilotModel
		{
			Id = pilot.Id,
			Name = pilot.Name,
			SurName = pilot.SurName,
			Age = pilot.Age,
			AirlineId = pilot.Id
		};
		return pilotModel;
	}

	public bool UpdatePilot(PilotUpdateModel pilot)
	{
		var pilotDb = this._context.Pilot.FirstOrDefault(p => p.Id == pilot.Id);
		if (pilotDb == null) return false;

		pilotDb.Name = pilot.Name;
		pilotDb.SurName = pilot.SurName;
		pilotDb.Age = pilot.Age;
		pilotDb.AirlineId = pilot.AirlineId;

		this._context.SaveChanges();
		return true;
	}

	public int CreatePilot(PilotCreateModel pilotNew)
	{
		var addPilot = this._context.Pilot.Add(new Pilot
			{
				Name = pilotNew.Name,
				SurName = pilotNew.SurName,
				Age = pilotNew.Age,
				AirlineId = pilotNew.AirlineId
			}
		);
		this._context.SaveChanges();
		return addPilot.Entity.Id;
	}

	public bool DeletePilot(int id)
	{
		var pilot = this._context.Pilot.FirstOrDefault(p => p.Id == id);
		if (pilot == null) return false;

		this._context.Pilot.Remove(pilot);
		this._context.SaveChanges();

		return true;
	}
}