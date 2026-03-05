using AppointmentSystem.Application.Interfaces;
using AppointmentSystem.Domain.Entities;
using AppointmentSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


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
        public async Task<Appointment> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _appDbContext.Appointments.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }
        public async Task UpdateAsync(Appointment appointment, CancellationToken cancellationToken)
        {
            _appDbContext.Appointments.Update(appointment);
            await _appDbContext.SaveChangesAsync(cancellationToken);

        }
        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var appointment = await _appDbContext.Appointments.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
            if (appointment == null)
                return;

            _appDbContext.Appointments.Remove(appointment);
            await _appDbContext.SaveChangesAsync(cancellationToken);

        }
    }
}