using EventCreator.Business.Models;
using EventCreator.Business.Services;
using EventCreator.Utility.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCreator.Utility.Display
{
    public class DetailsDisplay
    {
        private EventService _eventService = new EventService();
        private TicketTypeService _ticketTypeService = new TicketTypeService();

        public void ShowAllEvents()
        {
            var listOfEventsBl = _eventService.GetAll();

            foreach (var @event in listOfEventsBl)
            {
                if (_ticketTypeService.CheckAvailbilityForEvent(@event))
                {
                    Console.WriteLine($"{Environment.NewLine} To choose this event kye in '{@event.Id}': {Environment.NewLine}");
                    ModelDisplay.DisplayEvent(@event);
                }
            }
        }

        public int GetNumerOfTickets(EventBl chosenEvent)
        {
            int numberOfTicketsFromUser = IoHelper.GetIntFromUser($"You have chosen an event: {chosenEvent.Name} {Environment.NewLine}" +
                                                $"Enter the number of tickets to buy: ");

            while (numberOfTicketsFromUser > _ticketTypeService.GetNumberOfTicketsAvailable(chosenEvent))
            {
                Console.WriteLine("There is not available declared amout of tickets");
                numberOfTicketsFromUser = IoHelper.GetIntFromUser("Enter the new number of tickets to buy");
            }
            return numberOfTicketsFromUser;
        }

        public CustomerBl CreateCustomer(int i)
        {
            var newCustomer = new CustomerBl
            {
                Name = IoHelper.GetStringFromUser($"Enter the Name of the client {i}: "),
                Surname = IoHelper.GetStringFromUser($"Enter the Surname of the client {i}: "),
                Age = IoHelper.GetIntFromUser($"Enter the Age of the client {i}: ")
            };
            return newCustomer;
        }

        public TicketTypeBl ChooseTicketType(EventBl chosenEvent)
        {
            var listOfTickets = _ticketTypeService.GetAll(chosenEvent);
            Console.WriteLine($"{Environment.NewLine}Choose ticket type from shown below:{Environment.NewLine}");

            foreach (var ticketType in listOfTickets)
            {
                if (_ticketTypeService.CheckAvailbilityOfSpecifiedTypeOfTicket(ticketType))
                {
                    Console.WriteLine($"- {ticketType.Name} " +
                                        $"(to choose press: {chosenEvent.TicketTypeList.IndexOf(ticketType)})");
                }
            }

            int indexOfTypeOfTicketFromUser = IoHelper.GetIntFromUser();

            return chosenEvent.TicketTypeList[indexOfTypeOfTicketFromUser];
        }
    }
}
