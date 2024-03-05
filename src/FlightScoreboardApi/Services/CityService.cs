using System.Threading.Tasks;
using FlightScoreboardApi.Models;
using FlightScoreboardData.DateBase;
using FlightScoreboardData.Repositories;

namespace FlightScoreboardApi.Services;

public interface ICityService
{
	Task<int> CreateCityAsync(CityCreateModel city);
	Task<bool> UpdateCityAsync(CityUpdateModel city);
	Task<bool> DeleteCityAsync(int id);
}

public class CityService : ICityService
{
	private readonly ICityReadRepository _cityReadRepository;
	private readonly ICityWriteRepository _cityWriteRepository;
	private readonly IFlightReadRepository _flightReadRepository;

	public CityService(ICityReadRepository cityReadRepository, ICityWriteRepository cityWriteRepository, IFlightReadRepository flightReadRepository)
	{
		_cityReadRepository = cityReadRepository;
		_cityWriteRepository = cityWriteRepository;
		_flightReadRepository = flightReadRepository;
	}


	public async Task<int> CreateCityAsync(CityCreateModel city)
	{
		return await _cityWriteRepository.CreateCityAsync(new City
		{
			Name = city.Name
		});
	}

	public async Task<bool> UpdateCityAsync(CityUpdateModel city)
	{
		var cityNew = await _cityReadRepository.GetCityAsync(city.Id);
		if (cityNew == null) return false;
		cityNew.Name = city.Name;
		await _cityWriteRepository.UpdateCityAsync(cityNew);
		return true;
	}

	public async Task<bool> DeleteCityAsync(int id)
	{
		if (await _flightReadRepository.DoesCityUsed(id))
			return false;
		return await _cityWriteRepository.DeleteCityAsync(id);
	}
}