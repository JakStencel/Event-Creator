using EventCreator.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCreator.Data
{
    public class DataHolder
    {
        public static List<Event> EventList = new List<Event>
        {
            new Event
            {
                Name = "Open'er festiwal",
                Date = new DateTime(2019, 7, 23),
                Description = "The biggest music festival in Poland, which is taking place in the most beautiflu spot - Reagan's Park ",
                TicketTypeList = new List<TicketType>
                {
                    new TicketType {Name = "normal", Price = 300, NumberOfAvailable = 250},
                    new TicketType {Name = "Vip", Price = 800, NumberOfAvailable = 100},
                    new TicketType {Name = "Super Vip", Price = 1500, NumberOfAvailable = 10}
                }
            },
             new Event
             {
                Name = "Meskie granie orkiestra",
                Date = new DateTime(2019, 8, 11),
                Description = "The most expected event in the whole Poland. Best charismatic artist sing across " +
                              "their music genre. Podsiadło, Zalewski, Kortez ",
                TicketTypeList = new List<TicketType>
                {
                    new TicketType {Name = "normal", Price = 120, NumberOfAvailable = 20},
                    new TicketType {Name = "Vip", Price = 250, NumberOfAvailable = 10},
                }
             }
        };
        public static List<User> UserList = new List<User>
        {
            new User
            {
                Login = "UncleBob",
                Password = "password"
            }
        };
    }
}
