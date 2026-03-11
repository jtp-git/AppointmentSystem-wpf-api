using AppointmentSystem.Application.DTO;
using AppointmentSystem.Application.Interfaces;
using AppointmentSystem.Application.Mapper;


namespace AppointmentSystem.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<AppointmentDto> CreateAsync(CreateAppointmentDto appointmentDto, CancellationToken cancellationToken)
        {
            ValidateTime(appointmentDto.StartTime, appointmentDto.EndTime);

            var appointment = AppointmentMapper.ToDomain(appointmentDto);

            await _appointmentRepository.AddAsync(appointment, cancellationToken);

            return AppointmentMapper.ToDto(appointment);
        }

        public async Task<List<AppointmentDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var appointments = await _appointmentRepository.GetAllAsync(cancellationToken);

            return appointments.Select(AppointmentMapper.ToDto).ToList();
        }
        public async Task<AppointmentDto> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id, cancellationToken);
            if (appointment == null)
                throw new Exception($"Appointment with ID {id} not found.");
            return AppointmentMapper.ToDto(appointment);
        }
        public async Task UpdateAsync(UpdateAppointmentDto appointmentDto, CancellationToken cancellationToken)
        {
            ValidateTime(appointmentDto.StartTime, appointmentDto.EndTime);

            var existingAppointment = await _appointmentRepository.GetByIdAsync(appointmentDto.Id, cancellationToken);

            if (existingAppointment == null)
                throw new Exception($"Appointment with ID {appointmentDto.Id} not found.");

            AppointmentMapper.MapUpdate(appointmentDto, existingAppointment);

            await _appointmentRepository.UpdateAsync(existingAppointment, cancellationToken);
        }
        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var existingAppointment = await _appointmentRepository.GetByIdAsync(id, cancellationToken);
            if (existingAppointment == null)
                throw new Exception($"Appointment with ID {id} not found.");
            await _appointmentRepository.DeleteAsync(id, cancellationToken);
        }
        private static void ValidateTime(DateTime start, DateTime end)
        {
            if (end <= start)
                throw new ArgumentException("End time must be after start time.");
        }
    }
}
