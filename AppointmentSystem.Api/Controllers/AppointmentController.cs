using AppointmentSystem.Application.DTO;
using AppointmentSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentSystem.Api.Controllers
{
    [ApiController]
    [Route("appointments")]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
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
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id, CancellationToken cancellationToken)
        {
            await _appointmentService.DeleteAsync(id, cancellationToken);
            return NoContent();
        }
    }
    
}
