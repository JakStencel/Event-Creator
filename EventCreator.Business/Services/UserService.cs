using EventCreator.Business.Models;
using EventCreator.Data;
using EventCreator.Data.Models;
using EventCreator.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("EventCreator.Business.Tests")]
namespace EventCreator.Business.Services
{
    public class UserService
    {
        private UserRepository _userRepository;
        private DataObjectMapper _dataObjectsMapper;

        public UserService()
        {
            _userRepository = new UserRepository();
            _dataObjectsMapper = new DataObjectMapper();
        }

        public void AddUserToUserList(UserBl userBl)
        {
            var user = _dataObjectsMapper.MapUserBlToUser(userBl);

            using (var dbContext = new EventCreatorDbContext())
            {
                dbContext.UserDbSet.Add(user);
                dbContext.SaveChanges();
            }

            //_userRepository.AddUser(user);
        }

        public List<UserBl> GetAllUsers()
        {
            //var listOfUsers = _userRepository.GetAll();
            List<User> users;

            using (var dbContext = new EventCreatorDbContext())
            {
                users = dbContext.UserDbSet.Include(u => u.PurchasedTickets).ToList();
            }

            var listOfUsersBl = new List<UserBl>();

            foreach (var user in users)
            {
                listOfUsersBl.Add(_dataObjectsMapper.MapUserToUserBl(user));
            }
            return listOfUsersBl;
        }

        public UserBl GetUser(string login)
        {
            //return _dataObjectsMapper.MapUserToUserBl(_userRepository.GetUser(login));
            User user;
            using (var dbContext = new EventCreatorDbContext())
            {
                if(!dbContext.UserDbSet.Any(u => u.Login == login))
                {
                    throw new Exception("There is no user with provided data");
                }

                user = dbContext.UserDbSet
                    .Include(u => u.PurchasedTickets)
                    .Include(u => u.PurchasedTickets.Select(t => t.ChosenEvent))
                    .Include(u => u.PurchasedTickets.Select(t => t.Customer))
                    .Include(u => u.PurchasedTickets.Select(t => t.TypeOfTicket))
                    .Include(u => u.PurchasedTickets.Select(t => t.User))
                    .SingleOrDefault(u => u.Login == login);
            }

            return _dataObjectsMapper.MapUserToUserBl(user);
        }

        public void UpdateUser(int indexOfEventFromUser, CustomerBl customerBl, TicketTypeBl ticketTypeBl, UserBl userBl, TicketBl ticketBl)
        {
            var ticket = _dataObjectsMapper.MapTicketBlToTicket(ticketBl);
            var user = _dataObjectsMapper.MapUserBlToUser(userBl);
            var customer = _dataObjectsMapper.MapCustomerBlToCustomer(customerBl);
            var ticketType = _dataObjectsMapper.MapTicketTypeBlToTicketType(ticketTypeBl);

            using (var dbContext = new EventCreatorDbContext())
            {
                user.PurchasedTickets = null;

                dbContext.TicketTypeDbSet.Attach(ticketType);
                dbContext.Entry(ticketType).State = EntityState.Modified;

                dbContext.UserDbSet.Attach(user);
                dbContext.Entry(user).State = EntityState.Modified;

                var chosenEvent = dbContext.EventDbSet
                    .Include(e => e.CustomerList)
                    .Include(e => e.TicketTypeList)
                    .SingleOrDefault(e => e.Id == indexOfEventFromUser);

                chosenEvent.Proceeds += ticketType.Price;

                customer.Event = chosenEvent;
                customer.UserTicket = ticketType;
                dbContext.CustomerDbSet.Add(customer);

                ticket.ChosenEvent = chosenEvent;
                ticket.Customer = customer;
                ticket.TypeOfTicket = ticketType;
                ticket.User = user;
                dbContext.TicketDbSet.Add(ticket);

                user.PurchasedTickets.Add(ticket);

                dbContext.SaveChanges();
            }
                //_userRepository.Update(indexOfEventFromUser, customerFromDataMapper, ticketTypeFromDataMapper, userFromDataMapper, ticketFromDataMapper);
        }

        public void AddSoldTicket(UserBl user, TicketBl soldTicket)
        {
            user.PurchasedTickets.Add(soldTicket);
        }

        public bool CheckPassword(UserBl userBl, string passwordFromUser)
        {
            if (userBl.Password == passwordFromUser)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
