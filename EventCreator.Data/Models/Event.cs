using System;
using System.Collections.Generic;

namespace EventCreator.Data.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Proceeds { get; set; }

        public List<TicketType> TicketTypeList { get; set; } 
        public List<Customer> CustomerList { get; set; } 
    }
}
