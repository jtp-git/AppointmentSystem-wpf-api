using AppointmentSystem.Application.DTO;
using AppointmentSystem.Application.Interfaces;
using AppointmentSystem.Application.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentSystem.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<AppointmentDto> CreateAsync(AppointmentDto appointmentDto, CancellationToken cancellationToken)
        {
            var appointment = AppointmentMapper.toEntity(appointmentDto);
            await _appointmentRepository.AddAsync(appointment, cancellationToken);
            return AppointmentMapper.toDto(appointment);
        }

        public async Task<List<AppointmentDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var appointments = await _appointmentRepository.GetAllAysnc(cancellationToken);
            return appointments.Select(AppointmentMapper.toDto).ToList();
        }
    }
}
