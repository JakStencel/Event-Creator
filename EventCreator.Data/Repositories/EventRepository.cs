using EventCreator.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace EventCreator.Data.Repositories
{
    public interface IEventRepository
    {
        void AddEvent(Event eventToAdd);
        Event Get(int indexFromUser);
        List<Event> GetAll();
        void Update(int indexOfEventToUpdate, Customer customer, TicketType ticketType);
    }

    public class EventRepository : IEventRepository
    {
        public List<Event> GetAll()
        {
            using(var dbContext = new EventCreatorDbContext())
            {
                var events = dbContext.EventDbSet.Include(e => e.TicketTypeList).Include(e => e.CustomerList);
                return events.ToList();
            }
        }

        public void AddEvent(Event eventToAdd)
        {
            using (var dbContext = new EventCreatorDbContext())
            {
                if(dbContext.EventDbSet.Any(e => e.Name == eventToAdd.Name))
                {
                    throw new Exception($"Event with given name: '{eventToAdd.Name}' already exist!");
                }
                dbContext.EventDbSet.Add(eventToAdd);
                dbContext.SaveChanges();
            }
        }

        public Event Get(int indexFromUser)
        {
            using (var dbContext = new EventCreatorDbContext())
            {
                if(indexFromUser > dbContext.EventDbSet.Count() || indexFromUser <= 0)
                {
                    throw new Exception("You have entered wrong index of event");
                }
                var chosenEvent = dbContext.EventDbSet.SingleOrDefault(e => e.Id == indexFromUser);
                dbContext.Entry(chosenEvent).Collection(e => e.CustomerList).Load();
                dbContext.Entry(chosenEvent).Collection(e => e.TicketTypeList).Load();
                return chosenEvent; 
            }
        }

        public void Update(int indexOfEventToUpdate, Customer customer, TicketType ticketType)
        {
            using (var dbContext = new EventCreatorDbContext())
            {
                var chosenEvent = dbContext.EventDbSet.SingleOrDefault(e => e.Id == indexOfEventToUpdate);
                chosenEvent.Proceeds += ticketType.Price;

                dbContext.TicketTypeDbSet.Attach(ticketType);
                dbContext.Entry(ticketType).State = EntityState.Modified;

                customer.Event = chosenEvent;
                customer.UserTicket = ticketType;
                dbContext.CustomerDbSet.Add(customer);

                dbContext.SaveChanges();
            }
        }
    }
}
