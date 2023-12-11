using FlightScoreboard.Models;
using FlightScoreboard.Services;
using FlightScoreboard.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightScoreboard.Controllers;

public class CityNewController : Controller
{
    private readonly ICityService _cityService;

    public CityNewController(ICityService cityService)
    {
        _cityService = cityService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        var cities = await _cityService.GetAllCitiesAsync();
        return Json(cities);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CityCreateModel city)
    {
        var cityId= await _cityService.CreateCityAsync(city);
        return Json(cityId);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _cityService.DeleteCityAsync(id);
        if (result == false)
            return RedirectToAction("Index", "Error", new ErrorModel
            {
                ErrorMessage = "Удалить невозможно, возможно город используется в рейсе",
                ActionName = "Index",
                ControllerName = "City"
            });
        return RedirectToAction("Index");
    }
}