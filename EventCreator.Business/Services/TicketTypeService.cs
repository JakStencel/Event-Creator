using EventCreator.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventCreator.Business.Services
{
    public class TicketTypeService
    {
        public void AddTicketType(EventBl eventBl, TicketTypeBl ticketTypeBl)
        {
            if (eventBl.TicketTypeList.Any(t => t.Name == ticketTypeBl.Name))
            {
                throw new Exception($"Ticket with given name: '{ticketTypeBl.Name}' already exist");
            }
            eventBl.TicketTypeList.Add(ticketTypeBl);
        }

        public List<TicketTypeBl> GetAll(EventBl eventBl)
        {
            return eventBl.TicketTypeList;
        }

        public int GetNumberOfTicketsAvailable(EventBl eventBl)
        {
            return eventBl.TicketTypeList.Sum(t => t.NumberOfAvailable);
        }

        public int GetNumberOfUnavailableTypesOfTickets(EventBl eventBl)
        {
            return eventBl.TicketTypeList.Count(t => t.NumberOfAvailable == 0);
        }

        public bool CheckAvailbilityForEvent(EventBl eventBl)
        {
            if (eventBl.TicketTypeList.Count(t => t.NumberOfAvailable == 0) == eventBl.TicketTypeList.Count)
            {
                return false;
            }
            return true;
        }

        public bool CheckAvailbilityOfSpecifiedTypeOfTicket(TicketTypeBl ticketTypeBl)
        {
            if (ticketTypeBl.NumberOfAvailable > 0)
            {
                return true;
            }
            return false;
        }

        public int DecreaseNumberOfAvailableTickets(TicketTypeBl typeOfTicket)
        {
            return typeOfTicket.NumberOfAvailable--;
        }
    }
}
