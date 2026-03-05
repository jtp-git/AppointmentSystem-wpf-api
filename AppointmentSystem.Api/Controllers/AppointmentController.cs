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
    }
}
