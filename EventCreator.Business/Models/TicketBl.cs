using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCreator.Business.Models
{
    public class TicketBl
    {
        public int Id;
        public EventBl ChosenEvent;
        public TicketTypeBl TypeOfTicket { get; set; }
        public CustomerBl Customer { get; set; }

    }
}
