using System.Threading.Tasks;
using FlightScoreboard.Models;
using FlightScoreboardData.Services;
using FlightScoreboardData.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightScoreboard.Controllers;

public class PilotController : Controller
{
	private readonly IAirlineService _airlineService;
	private readonly IPilotService _pilotService;

	public PilotController(IPilotService pilotService, IAirlineService airlineService)
	{
		_pilotService = pilotService;
		_airlineService = airlineService;
	}

	public async Task<IActionResult> Index()
	{
		var pilots = await _pilotService.GetAllPilotsAsync();
		return View(pilots);
	}

	[HttpGet]
	public async Task<IActionResult> Create()
	{
		var pilotModelGet = new PilotCreateModelGet();
		pilotModelGet.Airlines = await _airlineService.GetAvailableAirlinesAsync();
		return View(pilotModelGet);
	}

	[HttpPost]
	public async Task<IActionResult> Create(PilotCreateModel pilotNew)
	{
		await _pilotService.CreatePilotAsync(pilotNew);
		return RedirectToAction("Index");
	}

	[HttpGet]
	public async Task<IActionResult> Update(int id)
	{
		var pilotModel = new PilotUpdateModelGet();
		pilotModel.Pilot = await _pilotService.GetPilotByIdAsync(id);
		pilotModel.Airlines = await _airlineService.GetAvailableAirlinesAsync();
		// await Task.WhenAll(pilotTask, airlinesTask);

		return View(pilotModel);
	}

	[HttpPost]
	public async Task<IActionResult> Update(PilotUpdateModel pilot)
	{
		await _pilotService.UpdatePilotAsync(pilot);

		return RedirectToAction("Index");
	}

	public async Task<IActionResult> Delete(int id)
	{
		var result = await _pilotService.DeletePilotAsync(id);
		if (result == false)
			return RedirectToAction("Index", "Error", new ErrorModel
			{
				ErrorMessage = "Удалить невозможно, возможно пилот в рейсе",
				ActionName = "Index",
				ControllerName = "Pilot"
			});
		return RedirectToAction("Index");
	}
}