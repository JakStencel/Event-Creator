using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventCreator.Data;

namespace EventCreator.Business.Models
{
    public class EventBl
    {
        public EventBl()
        {
            TicketTypeList = new List<TicketTypeBl>();
            CustomerList = new List<CustomerBl>();
        }
        public int Id;
        public string Name;
        public DateTime Date;
        public string Description;
        public decimal Proceeds;

        public List<TicketTypeBl> TicketTypeList;
        public List<CustomerBl> CustomerList;
    }
}
