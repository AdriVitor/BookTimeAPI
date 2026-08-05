using System.Text.Json.Serialization;

namespace BookingService_Application.DTOs
{
    public class ReservationCreateDTO
    {
        public int IdResource { get; set; }
        [JsonIgnore]
        public int IdCustomer { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Observation { get; set; }
    }
}
