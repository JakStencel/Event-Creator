namespace EventCreator.Data.Models
{
    public class TicketType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int NumberOfAvailable { get; set; }
        public Event Event { get; set; } 
    }
}
