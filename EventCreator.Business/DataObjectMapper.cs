using EventCreator.Business.Models;
using EventCreator.Data.Models;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
[assembly: InternalsVisibleTo("EventCreator.Business.Tests")]
namespace EventCreator.Business
{
    internal interface IDataObjectMapper
    {
        List<Customer> MapCustomerBlListToCustomerList(List<CustomerBl> listOfCustomersBl);
        Customer MapCustomerBlToCustomer(CustomerBl customerBl);
        List<CustomerBl> MapCustomerListToCustomerBlList(List<Customer> listOfCustomers);
        CustomerBl MapCustomerToCustomerBl(Customer customer);
        Event MapEventBlToEvent(EventBl eventBl);
        List<Event> MapEventListBlToEventList(List<EventBl> eventlistBl);
        List<EventBl> MapEventListToEventListBl(List<Event> eventlist);
        EventBl MapEventToEventBl(Event @event);
        List<Ticket> MapTicketBlListToTicketList(List<TicketBl> ticketBlList);
        Ticket MapTicketBlToTicket(TicketBl ticketBl);
        List<TicketBl> MapTicketListToTicketBlList(List<Ticket> ticketList);
        TicketBl MapTicketToTicketBl(Ticket ticket);
        TicketType MapTicketTypeBlToTicketType(TicketTypeBl ticketTypeBl);
        List<TicketType> MapTicketTypeListBlToTicketTypeList(List<TicketTypeBl> ticketListBl);
        List<TicketTypeBl> MapTicketTypeListToTicketTypeListBl(List<TicketType> ticketList);
        TicketTypeBl MapTicketTypeToTicketTypeBl(TicketType ticketType);
        User MapUserBlToUser(UserBl userBl);
        UserBl MapUserToUserBl(User user);
    }

    internal class DataObjectMapper : IDataObjectMapper
    {
        public Event MapEventBlToEvent(EventBl eventBl)
        {
            var @event = new Event
            {
                Id = eventBl.Id,
                Name = eventBl.Name,
                Date = eventBl.Date,
                Description = eventBl.Description,
                Proceeds = eventBl.Proceeds,
                CustomerList = MapCustomerBlListToCustomerList(eventBl.CustomerList),
                TicketTypeList = MapTicketTypeListBlToTicketTypeList(eventBl.TicketTypeList),
            };

            return @event;
        }

        public EventBl MapEventToEventBl(Event @event)
        {
            var eventBl = new EventBl
            {
                Id = @event.Id,
                Name = @event.Name,
                Date = @event.Date,
                Description = @event.Description,
                Proceeds = @event.Proceeds,
                CustomerList = MapCustomerListToCustomerBlList(@event.CustomerList),
                TicketTypeList = MapTicketTypeListToTicketTypeListBl(@event.TicketTypeList),
            };

            return eventBl;
        }

        public List<Event> MapEventListBlToEventList(List<EventBl> eventlistBl)
        {
            var eventList = new List<Event>();
            foreach (var eventBl in eventlistBl)
            {
                eventList.Add(MapEventBlToEvent(eventBl));
            }

            return eventList;
        }

        public List<EventBl> MapEventListToEventListBl(List<Event> eventlist)
        {
            var eventListBl = new List<EventBl>();
            foreach (var @event in eventlist)
            {
                eventListBl.Add(MapEventToEventBl(@event));
            }

            return eventListBl;
        }

        public List<TicketType> MapTicketTypeListBlToTicketTypeList(List<TicketTypeBl> ticketListBl)
        {
            var ticketList = new List<TicketType>();
            foreach (var ticketTypeBl in ticketListBl)
            {
                ticketList.Add(MapTicketTypeBlToTicketType(ticketTypeBl));
            }

            return ticketList;
        }

        public List<TicketTypeBl> MapTicketTypeListToTicketTypeListBl(List<TicketType> ticketList)
        {
            var ticketListBl = new List<TicketTypeBl>();
            foreach (var ticketType in ticketList)
            {
                ticketListBl.Add(MapTicketTypeToTicketTypeBl(ticketType));
            }

            return ticketListBl;
        }

        public TicketType MapTicketTypeBlToTicketType(TicketTypeBl ticketTypeBl)
        {
            var ticketType = new TicketType
            {
                Id = ticketTypeBl.Id,
                Name = ticketTypeBl.Name,
                Price = ticketTypeBl.Price,
                NumberOfAvailable = ticketTypeBl.NumberOfAvailable,
            };

            return ticketType;
        }

        public TicketTypeBl MapTicketTypeToTicketTypeBl(TicketType ticketType)
        {
            var ticketTypeBl = new TicketTypeBl
            {
                Id = ticketType.Id,
                Name = ticketType.Name,
                Price = ticketType.Price,
                NumberOfAvailable = ticketType.NumberOfAvailable
            };

            return ticketTypeBl;
        }

        public Customer MapCustomerBlToCustomer(CustomerBl customerBl)
        {
            var customer = new Customer
            {
                Id = customerBl.Id,
                Name = customerBl.Name,
                Surname = customerBl.Surname,
                Age = customerBl.Age,
                UserTicket = MapTicketTypeBlToTicketType(customerBl.UserTicket)
            };

            return customer;
        }

        public CustomerBl MapCustomerToCustomerBl(Customer customer)
        {
            var customerBl = new CustomerBl
            {
                Id = customer.Id,
                Name = customer.Name,
                Surname = customer.Surname,
                Age = customer.Age,
                UserTicket = MapTicketTypeToTicketTypeBl(customer.UserTicket)
            };

            return customerBl;
        }

        public List<Customer> MapCustomerBlListToCustomerList(List<CustomerBl> listOfCustomersBl)
        {
            var customers = new List<Customer>();
            foreach (var customerBl in listOfCustomersBl)
            {
                customers.Add(MapCustomerBlToCustomer(customerBl));
            }

            return customers;
        }

        public List<CustomerBl> MapCustomerListToCustomerBlList(List<Customer> listOfCustomers)
        {
            var customersBl = new List<CustomerBl>();
            foreach (var customer in listOfCustomers)
            {
                customersBl.Add(MapCustomerToCustomerBl(customer));
            }

            return customersBl;
        }

        public User MapUserBlToUser(UserBl userBl)
        {
            var newUser = new User
            {
                Id = userBl.Id,
                Login = userBl.Login,
                Password = userBl.Password,
                PurchasedTickets = MapTicketBlListToTicketList(userBl.PurchasedTickets)
            };
            return newUser;
        }

        public UserBl MapUserToUserBl(User user)
        {
            var newUserBl = new UserBl
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password,
                PurchasedTickets = MapTicketListToTicketBlList(user.PurchasedTickets)
            };
            return newUserBl;
        }

        public Ticket MapTicketBlToTicket(TicketBl ticketBl)
        {
            var newTicket = new Ticket()
            {
                Id = ticketBl.Id,
                ChosenEvent = MapEventBlToEvent(ticketBl.ChosenEvent),
                Customer = MapCustomerBlToCustomer(ticketBl.Customer),
                TypeOfTicket = MapTicketTypeBlToTicketType(ticketBl.TypeOfTicket)
            };
            return newTicket;
        }

        public TicketBl MapTicketToTicketBl(Ticket ticket)
        {
            var newTicketBl = new TicketBl()
            {
                Id = ticket.Id,
                ChosenEvent = MapEventToEventBl(ticket.ChosenEvent),
                Customer = MapCustomerToCustomerBl(ticket.Customer),
                TypeOfTicket = MapTicketTypeToTicketTypeBl(ticket.TypeOfTicket)
            };
            return newTicketBl;
        }

        public List<Ticket> MapTicketBlListToTicketList(List<TicketBl> ticketBlList)
        {
            var ticketList = new List<Ticket>();
            foreach (var ticketBl in ticketBlList)
            {
                ticketList.Add(MapTicketBlToTicket(ticketBl));
            }
            return ticketList;
        }

        public List<TicketBl> MapTicketListToTicketBlList(List<Ticket> ticketList)
        {
            var ticketBlList = new List<TicketBl>();
            foreach (var ticket in ticketList)
            {
                ticketBlList.Add(MapTicketToTicketBl(ticket));
            }
            return ticketBlList;
        }
    }
}
