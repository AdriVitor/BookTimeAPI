using BookingService_API.Extensions;
using BookingService_Application.DTOs;
using BookingService_Application.DTOs.Reservation;
using BookingService_Application.Services.Interfaces;
using BookingService_Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions;

namespace BookingService_API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;
        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet("/{id}")]
        public async Task<ActionResult<Reservation>> GetById(int id)
        {
            try
            {
                var reservation = await _reservationService.GetByIdAsync(id);

                return Ok(reservation);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("/Resource/{resourceId}")]
        public async Task<ActionResult<IEnumerable<Reservation>>> Get(int resourceId)
        {
            try
            {
                var reservationsByPlaceId = await _reservationService.GetAllByResourceAsync(resourceId);
                return Ok(reservationsByPlaceId);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ReservationCreateDTO dto)
        {
            try
            {
                var tokenJwt = HttpContext.Request.Headers.Authorization.ToString();
                dto.IdCustomer = tokenJwt.GetCustomerId();

                dto.StartDate = dto.StartDate.SetSpecifyKind(DateTimeKind.Unspecified);
                dto.EndDate = dto.EndDate.SetSpecifyKind(DateTimeKind.Unspecified);

                await _reservationService.AddAsync(dto);

                return Ok("Reserva enviada para registro");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update")]
        public async Task<ActionResult> Put([FromBody] ReservationUpdateDTO dto)
        {
            try
            {
                var tokenJwt = HttpContext.Request.Headers.Authorization.ToString();
                var IdCustomer = tokenJwt.GetCustomerId();

                if (dto.IdCustomer != IdCustomer) throw new Exception("Operação não permitida");

                await _reservationService.UpdateAsync(dto);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var isDeleted = await _reservationService.DeleteAsync(id);
                if(!isDeleted)
                    return BadRequest();

                return Ok();
            }
            catch (Exception ex )
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
