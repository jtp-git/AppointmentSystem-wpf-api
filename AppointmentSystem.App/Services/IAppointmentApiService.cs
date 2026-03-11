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
   
    }
}
