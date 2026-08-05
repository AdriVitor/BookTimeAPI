namespace BookingService_Application.DTOs.Reservation
{
    public class ReservationUpdateDTO
    {
        public int Id { get; set; }
        public int IdCustomer { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Observation { get; set; }
    }
}
