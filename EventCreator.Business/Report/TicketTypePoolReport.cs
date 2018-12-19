using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCreator.Business.Report
{
    public class TicketTypePoolReport
    {
        public int AgeOfOldestParticipantOfTicektTypes { get; set; }
        public int AgeOfYoungestParticipantOfTicektTypes { get; set; }
        public double AverageAgeOfParticipantsOFTicketTypes { get; set; }
        public decimal TotalIncomeForTicketTypes { get; set; }
    }
}
