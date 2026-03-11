
using AppointmentSystem.Domain.Entities;


namespace AppointmentSystem.Application.Interfaces
{
    public interface IAppointmentRepository
    {
        Task AddAsync(Appointment appointment, CancellationToken cancellationToken);
        Task<List<Appointment>> GetAllAsync(CancellationToken cancellationToken);
        Task<Appointment> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task UpdateAsync(Appointment appointment, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
