using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCreator.Business.Models
{
    public class CustomerBl
    {
        public int Id;
        public string Name;
        public string Surname;
        public int Age;
        public TicketTypeBl UserTicket;
    }
}
