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
        public async Task<IActionResult> CreateAppointment(CreateAppointmentDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var createdAppointment = await _appointmentService.CreateAsync(dto, cancellationToken);
                return CreatedAtAction(nameof(GetAppointmentById),
                    new { id = createdAppointment.Id },
                    createdAppointment);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Appointment");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAppointments(CancellationToken cancellationToken)
        {
            var appointments = await _appointmentService.GetAllAsync(cancellationToken);
            return Ok(appointments);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentDto>> GetAppointmentById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var appointment = await _appointmentService.GetByIdAsync(id, cancellationToken);

                if (appointment == null)
                    return NotFound();

                return Ok(appointment);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Appointment not found");
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the appointment.");
                return StatusCode(500, "Internal server error");
            }
           
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, UpdateAppointmentDto dto, CancellationToken cancellationToken)
        {
            if (id != dto.Id)
            {
                return BadRequest(new { message = "Route ID mismatch"});
            }
            try
            {
                await _appointmentService.UpdateAsync(dto, cancellationToken);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Appointment not found");
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
                _logger.LogWarning(ex, "Appointment not found");
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
