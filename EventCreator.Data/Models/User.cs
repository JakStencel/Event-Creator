using System.Collections.Generic;

namespace EventCreator.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set;}
        public string Password { get; set; }

        public List<Ticket> PurchasedTickets { get; set; }
    }
}
