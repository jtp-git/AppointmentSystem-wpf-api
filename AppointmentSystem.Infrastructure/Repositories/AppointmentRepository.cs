using AppointmentSystem.Application.Interfaces;
using AppointmentSystem.Domain.Entities;
using AppointmentSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentSystem.Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppDbContext _appDbContext;

        public AppointmentRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(Appointment appointment, CancellationToken cancellationToken)
        {
            _appDbContext.Appointments.Add(appointment);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
        public async Task<List<Appointment>> GetAllAysnc(CancellationToken cancellationToken)
        {
            return await _appDbContext.Appointments.ToListAsync(cancellationToken);

        }
    }
}