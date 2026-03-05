using AppointmentSystem.Application.DTO;
using AppointmentSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentSystem.Application.Interfaces
{
    public interface IAppointmentRepository
    {
        Task AddAsync(Appointment appointment, CancellationToken cancellationToken);
        Task<List<Appointment>> GetAllAysnc(CancellationToken cancellationToken);

    }
}
