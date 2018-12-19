using EventCreator.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace EventCreator.Data.Repositories
{
    public class UserRepository
    {
        public void AddUser(User userToAdd)
        {
            using (var dbContext = new EventCreatorDbContext())
            {
                if(dbContext.UserDbSet.Any(u => u.Login == userToAdd.Login))
                {
                    throw new Exception($"User with given login: '{userToAdd.Login}' already exist");
                }
                dbContext.UserDbSet.Add(userToAdd);
                dbContext.SaveChanges();
            }
        }

        public List<User> GetAll()
        {
            using (var dbContext = new EventCreatorDbContext())
            {
                var chosenUSer = dbContext.UserDbSet.Include(u => u.PurchasedTickets);
                return chosenUSer.ToList();
            }
        }

        public User GetUser(string login)
        {
            using (var dbContext = new EventCreatorDbContext())
            {
                if (dbContext.UserDbSet.Any(u => u.Login == login))
                {
                    var userToReturn = dbContext.UserDbSet
                        .Include(u => u.PurchasedTickets)
                        .Include(u => u.PurchasedTickets.Select(x => x.ChosenEvent))
                        .Include(u => u.PurchasedTickets.Select(x => x.Customer))
                        .Include(u => u.PurchasedTickets.Select(x => x.TypeOfTicket))
                        .Include(u => u.PurchasedTickets.Select(x => x.User))
                        .SingleOrDefault(u => u.Login == login);
                    return userToReturn;
                }
                throw new Exception("There is no user with provided login");
            }
        }

        public void Update(int indexEven, Customer customer, TicketType ticketType, User user, Ticket ticket)
        {
            using (var dbContext = new EventCreatorDbContext())
            {
                user.PurchasedTickets = null;
                dbContext.UserDbSet.Attach(user);
                dbContext.Entry(user).State = EntityState.Modified;

                dbContext.TicketTypeDbSet.Attach(ticketType);
                dbContext.Entry(ticketType).State = EntityState.Modified;

                var chosenEvent = dbContext.EventDbSet
                            .Include(e => e.TicketTypeList)
                            .Include(e => e.CustomerList)
                            .FirstOrDefault(e => e.Id == indexEven);

                chosenEvent.Proceeds += ticketType.Price;

                customer.Event = chosenEvent;
                customer.UserTicket = ticketType;
                dbContext.CustomerDbSet.Add(customer);

                ticket.Customer = customer;
                ticket.TypeOfTicket = ticketType;
                ticket.ChosenEvent = chosenEvent;
                ticket.User = user;

                dbContext.TicketDbSet.Add(ticket); 
                user.PurchasedTickets.Add(ticket); // the line above shouldn't be required becasue theoretically after adding ticket 
                                                   //to user (this line), the context that already track the user, starts tracking 
                                                   //ticket (ticket inhertits the state after user). But without these two lines errors
                                                   //occurred
                dbContext.SaveChanges();
            }
        }
    }
}
