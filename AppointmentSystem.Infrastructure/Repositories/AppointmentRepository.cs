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
            using var transaction = await _appDbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await _appDbContext.Appointments.AddAsync(appointment, cancellationToken);
                await _appDbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;

            }
          
        }
        public async Task<List<Appointment>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _appDbContext.Appointments.AsNoTracking().ToListAsync(cancellationToken);

        }
        public async Task<Appointment?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _appDbContext.Appointments.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }
        public async Task UpdateAsync(Appointment appointment, CancellationToken cancellationToken)
        {
            using var transaction = await _appDbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var existingAppointment = await _appDbContext.Appointments
                    .FirstOrDefaultAsync(a => a.Id == appointment.Id, cancellationToken);
                if (existingAppointment == null)
                    throw new KeyNotFoundException($"Appointment with ID {appointment.Id} not found");

                existingAppointment.Update(
                        appointment.Patient,
                        appointment.StartTime,
                        appointment.EndTime,
                        appointment.Notes
                    );
                await _appDbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;

            }
         

        }
        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            using var transaction = await _appDbContext.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                var appointment = await _appDbContext.Appointments
                   .FindAsync(new object[] { id }, cancellationToken);

                if (appointment == null)
                    throw new KeyNotFoundException($"Appointment with id {id} not found.");

                _appDbContext.Appointments.Remove(appointment);

                await _appDbContext.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }

        }
    }
}