using System;
using FlightScoreboardData.Services.Models;

namespace FlightScoreboardData.Services;

public interface IStatusService
{
	string CalculateStatus(IStatusCalculation scoreboardModel);
}

public class StatusService : IStatusService
{
	public string CalculateStatus(IStatusCalculation scoreboardDeparture)
	{
		if (scoreboardDeparture.ActualArrivalTime != null)
			return "Прибыл " + scoreboardDeparture.ActualArrivalTime;

		if (scoreboardDeparture.ActualDepartureTime != null)
		{
			if (scoreboardDeparture.DepartureTime.AddMinutes(5) < scoreboardDeparture.ActualDepartureTime)
				return "Задерживается, самолет вылетел " + scoreboardDeparture.ActualDepartureTime;
			return "Вылетел " + scoreboardDeparture.ActualDepartureTime;
		}

		if (scoreboardDeparture.BoardingEndTime != null)
			return "Посадка окончена";

		if (scoreboardDeparture.BoardingStartTime != null)
			return "Идет посадка";

		if (scoreboardDeparture.CheckInEndTime != null)
			return "Регистрация окончена";

		if (scoreboardDeparture.CheckInStartTime != null)
			return "Идет регистрация до " +
			       scoreboardDeparture.CheckInStartTime.Value.TimeOfDay.Add(new TimeSpan(2, 0, 0));

		if (scoreboardDeparture.CheckInStartTime != null)
			return "Идет регистрация до  " +
			       scoreboardDeparture.CheckInStartTime.Value.TimeOfDay.Add(new TimeSpan(2, 0, 0));

		if (scoreboardDeparture.DepartureTime < DateTime.Now)
			return "Задерживается, запланированное время вылета " + scoreboardDeparture.DepartureTime;

		return "Начало регистрации " + scoreboardDeparture.DepartureTime.TimeOfDay.Add(new TimeSpan(-2, 0, 0));


		// return string.Empty;
	}
}