using FlightScoreboard.DateBase;
using FlightScoreboard.Services.Interfaces;
using FlightScoreboard.Services.Models;

namespace FlightScoreboard.Services;

public class CityService : ICityService
{
	private readonly FlightScoreboardContext _context;

	public CityService(FlightScoreboardContext context)
	{
		this._context = context;
	}

	public List<CityModel> GetAllCities()
	{
		return this._context.Cities.Select(p => new CityModel
		{
			Id = p.Id,
			Name = p.Name
		}).ToList();
	}

	public int CreateCity(CityCreateModel cityNew)
	{
		var addCity = this._context.Cities.Add(new City { Name = cityNew.Name });

		this._context.SaveChanges();

		return addCity.Entity.Id;
	}

	public bool DeleteCity(int id)
	{
		var city = this._context.Cities.FirstOrDefault(p => p.Id == id);
		if (city == null) return false;
		this._context.Cities.Remove(city);

		this._context.SaveChanges();

		return true;
	}
}