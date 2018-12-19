using EventCreator.Business.Models;
using EventCreator.Business.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCreator.Business.Services
{
    public class ReportService
    {
        TicketTypeService _ticketTypeService = new TicketTypeService();

        public GeneralReport GenerateReport(EventBl chosenEvent)
        {
            var newReport = new GeneralReport();

            newReport.AgeOfOldestParticipant = GetAgeOfOldestParticipant(chosenEvent);
            newReport.AgeOfYoungestParticipant = GetAgeOfYoungestParticipant(chosenEvent);
            newReport.AverageAge = GetAverageAge(chosenEvent);
            newReport.TotalIncome = chosenEvent.Proceeds;
            newReport.Event = chosenEvent;
            newReport.NumberOfTicketsAvailable = _ticketTypeService.GetNumberOfTicketsAvailable(chosenEvent);

            foreach (var ticketType in chosenEvent.TicketTypeList)
            {
                var newTicketTypePoolReport = new TicketTypePoolReport
                {
                    AgeOfOldestParticipantOfTicektTypes = GetAgeOfOldestParticipantOfTicektType(chosenEvent, ticketType),
                    AgeOfYoungestParticipantOfTicektTypes = GetAgeOfYoungestParticipantOfTicektType(chosenEvent, ticketType),
                    AverageAgeOfParticipantsOFTicketTypes = GetAverageAgeOfParticipantsOFTicketType(chosenEvent, ticketType),
                    TotalIncomeForTicketTypes = GetTotalIncomeForTicketType(chosenEvent, ticketType)
                };
                newReport.TicketTypePoolReports.Add(newTicketTypePoolReport);
            }

            return newReport;
        }

        public int GetAgeOfOldestParticipant(EventBl chosenEvent)
        {
            if (chosenEvent.CustomerList.Count != 0)
            {
                int ageOfOldestCustomer = 0;

                foreach (var customer in chosenEvent.CustomerList)
                {
                    if (customer.Age > ageOfOldestCustomer)
                    {
                        ageOfOldestCustomer = customer.Age;
                    }
                }

                return ageOfOldestCustomer;
            }
            return 0;
        }

        public int GetAgeOfYoungestParticipant(EventBl chosenEvent)
        {
            if (chosenEvent.CustomerList.Count != 0)
            {
                int ageOfYoungestCustomer = int.MaxValue;

                foreach (var customer in chosenEvent.CustomerList)
                {
                    ageOfYoungestCustomer = Math.Min(customer.Age, ageOfYoungestCustomer);
                }

                return ageOfYoungestCustomer;
            }
            return 0;
        }

        public double GetAverageAge(EventBl chosenEvent)
        {
            if (chosenEvent.CustomerList.Count != 0)
            {
                var sumOfAge = chosenEvent.CustomerList.Sum(c => c.Age);
                return (sumOfAge / chosenEvent.CustomerList.Count);
            }
            return 0;
        }

        public int GetAgeOfOldestParticipantOfTicektType(EventBl chosenEvent, TicketTypeBl ticketTypeBl)
        {
            int ageOfOldestCustomer = 0;
            var participantsOfSpecifiedTicketType = chosenEvent.CustomerList.Where(c => c.UserTicket.Name == ticketTypeBl.Name).ToList();

            foreach (var customer in participantsOfSpecifiedTicketType)
            {
                ageOfOldestCustomer = Math.Max(customer.Age, ageOfOldestCustomer);
            }

            return ageOfOldestCustomer;
        }

        public int GetAgeOfYoungestParticipantOfTicektType(EventBl chosenEvent, TicketTypeBl ticketTypeBl)
        {
            var participantsOfSpecifiedTicketType = chosenEvent.CustomerList.Where(c => c.UserTicket.Name == ticketTypeBl.Name).ToList();
            if (participantsOfSpecifiedTicketType.Count > 0)
            {
                int ageOfYoungestCustomer = int.MaxValue;

                foreach (var customer in participantsOfSpecifiedTicketType)
                {
                    ageOfYoungestCustomer = Math.Min(customer.Age, ageOfYoungestCustomer);
                }
                return ageOfYoungestCustomer;
            }
            return 0;
        }

        public double GetAverageAgeOfParticipantsOFTicketType(EventBl chosenEvent, TicketTypeBl ticketTypeBl)
        {
            var participantsOfSpecifiedTicketType = chosenEvent.CustomerList.Where(c => c.UserTicket.Name == ticketTypeBl.Name).ToList();
            var numberOfParticipantsOfGivenTicketType = chosenEvent.CustomerList.Count(c => c.UserTicket.Name == ticketTypeBl.Name);

            if (numberOfParticipantsOfGivenTicketType > 0)
            {
                double averageAge = 0;

                foreach (var customer in participantsOfSpecifiedTicketType)
                {
                    averageAge += customer.Age;
                }

                return averageAge / numberOfParticipantsOfGivenTicketType;
            }
            return 0;
        }

        public decimal GetTotalIncomeForTicketType(EventBl chosenEvent, TicketTypeBl ticketTypeBl)
        {
            var numberOfParticipantsOfGivenTicketType = chosenEvent.CustomerList.Count(c => c.UserTicket.Name == ticketTypeBl.Name);
            return ticketTypeBl.Price * numberOfParticipantsOfGivenTicketType;
        }
    }
}
