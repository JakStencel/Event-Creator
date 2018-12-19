using EventCreator.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCreator.Utility.Display
{
    public class ModelDisplay
    {
        public static void DisplayEvent(EventBl @event)
        {
            Console.WriteLine($"Name of the event: {@event.Name} {Environment.NewLine}" +
                              $"Date of the event: {@event.Date.ToShortDateString()} {Environment.NewLine}" +
                              $"Description of the event: {@event.Description} {Environment.NewLine}");
        }

        public static void DisplayTicketType(TicketTypeBl ticket)
        {
            Console.WriteLine($"Ticket type called: {ticket.Name} {Environment.NewLine}" +
                              $"Price of specified ticket type: {ticket.Price} {Environment.NewLine}" +
                              $"Availbility of specified ticket type: {ticket.NumberOfAvailable} {Environment.NewLine}");
        }

        public static void DisplayCustomer(CustomerBl customer)
        {
            Console.WriteLine($"The Name of the customer: {customer.Name} {Environment.NewLine}" +
                              $"The Surname of the customer: {customer.Surname} {Environment.NewLine}" +
                              $"The type of chosen ticket: {customer.UserTicket.Name} {Environment.NewLine}");
        }
    }
}
