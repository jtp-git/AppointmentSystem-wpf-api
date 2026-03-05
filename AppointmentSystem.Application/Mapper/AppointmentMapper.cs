using AppointmentSystem.Application.DTO;
using AppointmentSystem.Domain.Entities;


namespace AppointmentSystem.Application.Mapper
{
    public class AppointmentMapper
    {

        public static AppointmentDto toDto(Appointment entity)
        {
            return new AppointmentDto
            {
                Id = entity.Id,
                Patient = entity.Patient,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                Notes = entity.Notes,
                DateCreated = entity.DateCreated,
                DateModified = entity.DateModified
            };
        }

        public static Appointment toEntity(AppointmentDto dto)
        {
            return new Appointment
            (dto.Patient,
                dto.StartTime,
                dto.EndTime,
                dto.Notes);

        }
    }
}
