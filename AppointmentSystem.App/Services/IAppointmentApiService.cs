using AppointmentSystem.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
