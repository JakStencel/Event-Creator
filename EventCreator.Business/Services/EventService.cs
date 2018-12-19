using EventCreator.Business.Models;
using EventCreator.Data;
using EventCreator.Data.Models;
using EventCreator.Data.Repositories;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Data.Entity;
using System.Linq;
using System;

[assembly: InternalsVisibleTo("EventCreator.Business.Tests")]
namespace EventCreator.Business.Services
{
    public class EventService
    {
        private IEventRepository _eventRepository;
        private IDataObjectMapper _dataObjectsMapper;

        public EventService()
        {
            _eventRepository = new EventRepository();
            _dataObjectsMapper = new DataObjectMapper();
        }

        //access modifier is internal cause of the accessibility of IDataObjectsMapper (internal)
        internal EventService(IEventRepository eventRepository, IDataObjectMapper dataObjectMapper)
        {
            _eventRepository = eventRepository;
            _dataObjectsMapper = dataObjectMapper;
        }

        public List<EventBl> GetAll()
        {
            //var eventList = _eventRepository.GetAll();
            List<Event> events;
            using (var dbContext = new EventCreatorDbContext())
            {
                events = dbContext.EventDbSet.Include(e => e.CustomerList).Include(e => e.TicketTypeList).ToList();
            }
            //----
            var eventsBl = new List<EventBl>();

            foreach (var @event in events)
            {
                eventsBl.Add(_dataObjectsMapper.MapEventToEventBl(@event));
            }

            return eventsBl;
        }

        public void AddEvent(EventBl eventBl)
        {
            var mappedEvent = _dataObjectsMapper.MapEventBlToEvent(eventBl);
            //---
            using (var dbContext = new EventCreatorDbContext())
            {
                dbContext.EventDbSet.Add(mappedEvent);
                dbContext.SaveChanges();
            }
            //_eventRepository.AddEvent(eventFromDataMapper);
        }

        public EventBl GetEvent(int indexOfEventFromUser)
        {
            Event @event;
            using (var dbContext = new EventCreatorDbContext())
            {
                if(!dbContext.EventDbSet.Any(e => e.Id == indexOfEventFromUser))
                {
                    throw new Exception("There is no event with provided Id");
                }

                @event = dbContext.EventDbSet.SingleOrDefault(e => e.Id == indexOfEventFromUser);
                dbContext.Entry(@event).Collection(e => e.CustomerList).Load();
                dbContext.Entry(@event).Collection(e => e.TicketTypeList).Load();
            }

            //return _dataObjectsMapper.MapEventToEventBl(_eventRepository.Get(indexOfEventFromUser));

            return _dataObjectsMapper.MapEventToEventBl(@event);
        }

        public void UpdateEvent(int indexOfEventFromUser, CustomerBl customerBl, TicketTypeBl ticketTypeBl)
        {
            var customer = _dataObjectsMapper.MapCustomerBlToCustomer(customerBl);
            var ticketType = _dataObjectsMapper.MapTicketTypeBlToTicketType(ticketTypeBl);
            //_eventRepository.Update(indexOfEventFromUser, customerFromDataMapper, ticketTypeFromDa);

            using (var dbContext = new EventCreatorDbContext())
            {
                var chosenEvent = dbContext.EventDbSet.SingleOrDefault(e => e.Id == indexOfEventFromUser);

                dbContext.TicketTypeDbSet.Attach(ticketType);
                dbContext.Entry(ticketType).State = EntityState.Modified;

                //updating proceeds of chosen event
                chosenEvent.Proceeds += ticketType.Price;

                customer.Event = chosenEvent;
                customer.UserTicket = ticketType;

                dbContext.CustomerDbSet.Add(customer);
                dbContext.SaveChanges();

            }
        }
    }
}
