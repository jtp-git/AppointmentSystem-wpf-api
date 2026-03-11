using AppointmentSystem.Application.DTO;

namespace AppointmentSystem.Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<AppointmentDto> CreateAsync(CreateAppointmentDto appointmentDto, CancellationToken cancellationToken);
        Task<List<AppointmentDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<AppointmentDto> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task UpdateAsync(UpdateAppointmentDto appointmentDto, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
