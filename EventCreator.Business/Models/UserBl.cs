using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCreator.Business.Models
{
    public class UserBl
    {
        public UserBl()
        {
            PurchasedTickets = new List<TicketBl>();
        }
        public int Id;
        public string Login;
        public string Password;

        public List<TicketBl> PurchasedTickets;
    }

}
