using AppointmentSystem.Application.DTO;


namespace AppointmentSystem.App.Services
{
    public interface IAppointmentApiService
    {
        Task<List<AppointmentDto>> GetAppointmentsAsync(CancellationToken cancellationToken);
        Task<AppointmentDto?> GetAppointmentByIdAsync(int id, CancellationToken cancellationToken);
        Task CreateAsync(CreateAppointmentDto dto, CancellationToken cancellationToken);
        Task UpdateAsync(UpdateAppointmentDto dto, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);

    }
}
