using AppointmentSystem.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentSystem.Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<AppointmentDto> CreateAsync(AppointmentDto appointmentDto, CancellationToken cancellationToken);
        Task<List<AppointmentDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<AppointmentDto> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task UpdateAsync(AppointmentDto appointmentDto, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
