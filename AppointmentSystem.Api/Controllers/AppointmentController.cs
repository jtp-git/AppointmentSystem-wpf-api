using AppointmentSystem.Application.DTO;
using AppointmentSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentSystem.Api.Controllers
{
    [ApiController]
    [Route("api/v1/appointments")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly ILogger<AppointmentController> _logger;
        public AppointmentController(IAppointmentService appointmentService, ILogger<AppointmentController> logger)
        {
            _appointmentService = appointmentService;
            _logger = logger;

        }
        [HttpPost]
        public async Task<IActionResult> CreateAppointment(AppointmentDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var createdAppointment = await _appointmentService.CreateAsync(dto, cancellationToken);
                return CreatedAtAction(nameof(CreateAppointment),
                    new { id = createdAppointment.Id },
                    createdAppointment);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAppointments(CancellationToken cancellationToken)
        {
            var appointments = await _appointmentService.GetAllAsync(cancellationToken);
            return Ok(appointments);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, AppointmentDto dto, CancellationToken cancellationToken)
        {
            if (id != dto.Id)
            {
                return BadRequest("ID mismatch");
            }
            try
            {
                await _appointmentService.UpdateAsync(dto, cancellationToken);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating the appointment.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id, CancellationToken cancellationToken)
        {
            try
            {
                await _appointmentService.DeleteAsync(id, cancellationToken);
                return NoContent();

            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while deleting the appointment.");
                return StatusCode(500, "Internal server error");

            }
        }

    }
}
