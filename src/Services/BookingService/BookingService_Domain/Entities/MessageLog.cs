namespace BookingService_Domain.Entities
{
    public class MessageLog
    {
        public Guid MessageId { get; set; }
        public DateTime ProcessedAt { get; set; }
    }
}
