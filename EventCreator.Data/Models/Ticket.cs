namespace EventCreator.Data.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public Event ChosenEvent { get; set; }
        public TicketType TypeOfTicket { get; set; }
        public Customer Customer { get; set; }
        public User User { get; set; }
    }
}
