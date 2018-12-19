using EventCreator.Business.Models;
using EventCreator.Business.Report;
using EventCreator.Business.Services;
using System;

namespace EventCreator.Utility.Display
{
    public class StatisticsDisplay
    {
        private TicketTypeService _ticketTypeService = new TicketTypeService();

        public void ShowStatsForEvent(GeneralReport report)
        {
            Console.WriteLine($"The Age of the oldest participant is: " +
                              $"{report.AgeOfOldestParticipant}{Environment.NewLine}" +
                              $"The Age of the youngest participant is: " +
                              $"{report.AgeOfYoungestParticipant}{Environment.NewLine}" +
                              $"The average Age of participant is: " +
                              $"{report.AverageAge:N2}{Environment.NewLine}" +
                              $"The income, that comes from chosen event is: " +
                              $"{report.TotalIncome}{Environment.NewLine}");
        }

        public void ShowStatsForSpecifiedTicketType(TicketTypePoolReport ticketTypeReport)
        {
            Console.WriteLine($"The age of the oldest customer of givent ticket type is: " +
                              $"{ticketTypeReport.AgeOfOldestParticipantOfTicektTypes}{Environment.NewLine}" +
                              $"The age of the youngest customer of the given ticket type is: " +
                              $"{ticketTypeReport.AgeOfYoungestParticipantOfTicektTypes}{Environment.NewLine}" +
                              $"The average age of customer of given ticket type is: " +
                              $"{ticketTypeReport.AverageAgeOfParticipantsOFTicketTypes}{Environment.NewLine}" +
                              $"The total income form given ticket type group is: " +
                              $"{ticketTypeReport.TotalIncomeForTicketTypes}{Environment.NewLine}");
        }

        public void ShowNumberOfAvailableTickets(EventBl chosenEvent)
        {
            Console.WriteLine($"Number of all available tickets: " +
                              $"{_ticketTypeService.GetNumberOfTicketsAvailable(chosenEvent)} {Environment.NewLine}");
        }
    }
}
