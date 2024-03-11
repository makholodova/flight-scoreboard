using System;
using FlightScoreboard.Services;
using FlightScoreboardData.Services.Models;
using NUnit.Framework;

namespace FlightScoreboard.Tests.Services;

public class StatusServiceTests
{
	[SetUp]
	public void Setup()
	{
	}

	[Test]
	public void CalculateStatus_BaseStatus()
	{
		var statusService = new StatusService();
		var model = new StatusCalculationTestModel
		{
			DepartureTime = DateTime.Now.AddDays(1),
			ArrivalTime = DateTime.Now.AddDays(1)
		};

		var result = statusService.CalculateStatus(model);

		Assert.That(result,
			Is.EqualTo("Начало регистрации " + model.DepartureTime.TimeOfDay.Add(new TimeSpan(-2, 0, 0))));
	}

	[Test]
	public void CalculateStatus_Delayed_WhenDepartureTimeIsLessThanNowTime()
	{
		var statusService = new StatusService();
		var model = new StatusCalculationTestModel
		{
			DepartureTime = DateTime.Now.AddHours(-2),
			ArrivalTime = DateTime.Now.AddHours(-1)
		};

		var result = statusService.CalculateStatus(model);

		Assert.That(result, Is.EqualTo("Задерживается, запланированное время вылета " + model.DepartureTime));
	}

	[Test]
	public void CalculateStatus_CheckInStarted_WhenCheckInStartTimeIsSet()
	{
		var statusService = new StatusService();
		var model = new StatusCalculationTestModel
		{
			DepartureTime = DateTime.Now,
			ArrivalTime = DateTime.Now,
			CheckInStartTime = DateTime.Now
		};

		var result = statusService.CalculateStatus(model);

		Assert.That(result, Is.EqualTo
			("Идет регистрация до " + model.CheckInStartTime.Value.TimeOfDay.Add(new TimeSpan(2, 0, 0))));
	}

	[Test]
	public void CalculateStatus_CheckInEnded_WhenCheckInEndTimeIsSet()
	{
		var statusService = new StatusService();
		var model = new StatusCalculationTestModel
		{
			DepartureTime = DateTime.Now,
			ArrivalTime = DateTime.Now,
			CheckInStartTime = DateTime.Now,
			CheckInEndTime = DateTime.Now
		};

		var result = statusService.CalculateStatus(model);

		Assert.That(result, Is.EqualTo("Регистрация окончена"));
	}


	[Test]
	public void CalculateStatus_BoardingStarted_WhenBoardingStartTimeIsSet()
	{
		var statusService = new StatusService();
		var model = new StatusCalculationTestModel
		{
			DepartureTime = DateTime.Now,
			ArrivalTime = DateTime.Now,
			CheckInStartTime = DateTime.Now,
			CheckInEndTime = DateTime.Now,
			BoardingStartTime = DateTime.Now
		};

		var result = statusService.CalculateStatus(model);

		Assert.That(result, Is.EqualTo("Идет посадка"));
	}

	[Test]
	public void CalculateStatus_BoardingFinished_WhenBoardingEndTimeIsSet()
	{
		// Arrange - настройка
		var statusService = new StatusService();
		var model = new StatusCalculationTestModel
		{
			DepartureTime = DateTime.Now,
			ArrivalTime = DateTime.Now,
			CheckInStartTime = DateTime.Now,
			CheckInEndTime = DateTime.Now,
			BoardingStartTime = DateTime.Now,
			BoardingEndTime = DateTime.Now
		};

		// Act - действие
		var result = statusService.CalculateStatus(model);

		// Assert - проверка
		Assert.That(result, Is.EqualTo("Посадка окончена"));
	}

	[Test]
	public void CalculateStatus_Departured_WhenActualDepartureTimeIsSet()
	{
		var statusService = new StatusService();
		var model = new StatusCalculationTestModel
		{
			DepartureTime = DateTime.Now.AddHours(-2),
			ArrivalTime = DateTime.Now.AddHours(-1),
			CheckInStartTime = DateTime.Now,
			CheckInEndTime = DateTime.Now,
			BoardingStartTime = DateTime.Now,
			BoardingEndTime = DateTime.Now,
			ActualDepartureTime = DateTime.Now.AddHours(-2).AddMinutes(4)
		};

		var result = statusService.CalculateStatus(model);

		Assert.That(result, Is.EqualTo("Вылетел " + model.ActualDepartureTime));
	}

	[Test]
	public void CalculateStatus_Arrived_WhenActualArrivalTimeIsSet()
	{
		var statusService = new StatusService();
		var model = new StatusCalculationTestModel
		{
			DepartureTime = DateTime.Now,
			ArrivalTime = DateTime.Now,
			CheckInStartTime = DateTime.Now,
			CheckInEndTime = DateTime.Now,
			BoardingStartTime = DateTime.Now,
			BoardingEndTime = DateTime.Now,
			ActualDepartureTime = DateTime.Now,
			ActualArrivalTime = DateTime.Now
		};

		var result = statusService.CalculateStatus(model);

		Assert.That(result, Is.EqualTo("Прибыл " + model.ActualArrivalTime));
	}

	[Test]
	public void CalculateStatus_Delayed_WhenDepartureTimeIsLessThanActualDepartureTime()
	{
		var statusService = new StatusService();
		var model = new StatusCalculationTestModel
		{
			DepartureTime = DateTime.Now.AddHours(-3),
			ArrivalTime = DateTime.Now.AddHours(-1),
			ActualDepartureTime = DateTime.Now.AddHours(-2),
			CheckInStartTime = DateTime.Now.AddHours(-1),
			CheckInEndTime = DateTime.Now.AddHours(-1),
			BoardingStartTime = DateTime.Now.AddHours(-1),
			BoardingEndTime = DateTime.Now.AddHours(-1)
		};

		var result = statusService.CalculateStatus(model);

		Assert.That(result, Is.EqualTo("Задерживается, самолет вылетел " + model.ActualDepartureTime));
	}

	private class StatusCalculationTestModel : IStatusCalculation
	{
		public DateTime DepartureTime { get; set; }
		public DateTime ArrivalTime { get; set; }
		public DateTime? ActualDepartureTime { get; set; }
		public DateTime? ActualArrivalTime { get; set; }
		public DateTime? CheckInStartTime { get; set; }
		public DateTime? CheckInEndTime { get; set; }
		public DateTime? BoardingStartTime { get; set; }
		public DateTime? BoardingEndTime { get; set; }
	}
}