using EventCreator.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCreator.Business.Report
{
    public class GeneralReport
    {
        public int AgeOfOldestParticipant { get; set; }
        public int AgeOfYoungestParticipant { get; set; }
        public double AverageAge { get; set; }
        public decimal TotalIncome { get; set; }
        public int NumberOfTicketsAvailable { get; set; }
        public EventBl Event { get; set; }


        public List<TicketTypePoolReport> TicketTypePoolReports = new List<TicketTypePoolReport>();
    }
}
