using BookingService_Domain.Entities;
using BookingService_Domain.Entities.Enums;
using System.Reflection;
using Xunit.Sdk;

namespace BookingService.UnitTests.Domain
{
    public class ReservationTests
    {
        private Reservation CreateValidReservation()
        {
            return new Reservation(
                idResource: 1,
                idCustomer: 1,
                startDate: DateTime.UtcNow.Date,
                endDate: DateTime.UtcNow.Date.AddDays(2),
                observation: "Reserva válida",
                status: (int)StatusReservationEnum.Pending
            );
        }

        [Fact]
        public void CreateReservation_ShouldCreateWithValidData()
        {
            var reservation = CreateValidReservation();

            Assert.Equal(1, reservation.IdResource);
            Assert.Equal(1, reservation.IdCustomer);
            Assert.Equal("Reserva válida", reservation.Observation);
            Assert.Equal((int)StatusReservationEnum.Pending, reservation.Status);
            Assert.True(reservation.EndDate > reservation.StartDate);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void ValidateData_ShouldThrow_WhenIdResourceIsInvalid(int invalidId)
        {
            Action act = () => new Reservation(
                invalidId,
                1,
                DateTime.UtcNow,
                DateTime.UtcNow.AddDays(1),
                "Teste",
                (int)StatusReservationEnum.Pending
            );

            Assert.Throws<Exception>(act);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void ValidateData_ShouldThrow_WhenIdCustomerIsInvalid(int invalidId)
        {
            Action act = () => new Reservation(
                1,
                invalidId,
                DateTime.UtcNow,
                DateTime.UtcNow.AddDays(1),
                "Teste",
                (int)StatusReservationEnum.Pending
            );

            Assert.Throws<Exception>(act);
        }

        [Fact]
        public void ValidateData_ShouldThrow_WhenStartDateIsDefault()
        {
            Action act = () => new Reservation(
                1,
                1,
                default,
                DateTime.UtcNow.AddDays(1),
                "Teste",
                (int)StatusReservationEnum.Pending
            );

            Assert.Throws<Exception>(act);
        }

        [Fact]
        public void ValidateData_ShouldThrow_WhenEndDateIsDefault()
        {
            Action act = () => new Reservation(
                1,
                1,
                DateTime.UtcNow,
                default,
                "Teste",
                (int)StatusReservationEnum.Pending
            );

            Assert.Throws<Exception>(act);
        }

        [Fact]
        public void ValidateData_ShouldThrow_WhenEndDateIsBeforeStartDate()
        {
            Action act = () => new Reservation(
                1,
                1,
                DateTime.UtcNow.AddDays(2),
                DateTime.UtcNow.AddDays(1),
                "Teste",
                (int)StatusReservationEnum.Pending
            );

            Assert.Throws<Exception>(act);
        }

        [Fact]
        public void ValidateData_ShouldThrow_WhenReservationHasOneDayDuration()
        {
            var start = DateTime.UtcNow.Date;
            var end = start; // mesma data → deve falhar

            Action act = () => new Reservation(
                1,
                1,
                start,
                end,
                "Teste",
                (int)StatusReservationEnum.Pending
            );

            Assert.Throws<Exception>(act);
        }

        // ----------------------------------------------------------
        //  OBSERVATION
        // ----------------------------------------------------------
        [Fact]
        public void ValidateData_ShouldThrow_WhenObservationIsTooLong()
        {
            var obs = new string('A', 256);

            Action act = () => new Reservation(
                1,
                1,
                DateTime.UtcNow,
                DateTime.UtcNow.AddDays(2),
                obs,
                (int)StatusReservationEnum.Pending
            );

            Assert.Throws<Exception>(act);
        }

        // ----------------------------------------------------------
        //  STATUS ENUM
        // ----------------------------------------------------------
        [Fact]
        public void ValidateData_ShouldThrow_WhenStatusIsInvalid()
        {
            int invalidStatus = 999;

            Action act = () => new Reservation(
                1,
                1,
                DateTime.UtcNow,
                DateTime.UtcNow.AddDays(1),
                "Teste",
                invalidStatus
            );

            Assert.Throws<Exception>(act);
        }

        [Fact]
        public void SetUserValidation_ShouldSetCorrectValue()
        {
            var reservation = CreateValidReservation();

            reservation.SetUserValidation(true);

            Assert.True(reservation.UserValidated);
        }

        [Fact]
        public void SetResourceValidation_ShouldSetCorrectValue()
        {
            var reservation = CreateValidReservation();

            reservation.SetResourceValidation(false);

            Assert.False(reservation.ResourceValidated);
        }

        [Fact]
        public void MarkAsConfirmed_ShouldUpdateStatus()
        {
            var reservation = CreateValidReservation();

            reservation.MarkAsConfirmed();

            Assert.Equal((int)StatusReservationEnum.Confirmed, reservation.Status);
        }

        [Fact]
        public void MarkAsFailed_ShouldUpdateStatus()
        {
            var reservation = CreateValidReservation();

            reservation.MarkAsFailed();

            Assert.Equal((int)StatusReservationEnum.Failed, reservation.Status);
        }

        [Fact]
        public void ValidateData_WithoutParameters_ShouldValidateCurrentState()
        {
            var reservation = CreateValidReservation();

            reservation.ValidateData();

            Assert.True(true);
        }
    }
}
