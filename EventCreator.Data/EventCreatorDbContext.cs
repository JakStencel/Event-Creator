using EventCreator.Data.Models;
using System.Configuration;
using System.Data.Entity;

namespace EventCreator.Data
{
    public class EventCreatorDbContext : DbContext
    {
        public EventCreatorDbContext() : base(GetConnectionString())
        {

        }

        public DbSet<Customer> CustomerDbSet { get; set; }
        public DbSet<Event> EventDbSet { get; set; }
        public DbSet<Ticket> TicketDbSet { get; set; }
        public DbSet<TicketType> TicketTypeDbSet { get; set; }
        public DbSet<User> UserDbSet { get; set; }

        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["EventCreatorDbConnectionString"].ConnectionString;
        }
    }
}
