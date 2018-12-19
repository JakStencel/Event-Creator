namespace EventCreator.Data.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public TicketType UserTicket { get; set; }
        public Event Event { get; set; }
    }
}
