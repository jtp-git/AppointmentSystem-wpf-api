using AppointmentSystem.Application.DTO;
using AppointmentSystem.Domain.Entities;


namespace AppointmentSystem.Application.Mapper
{
    public static class AppointmentMapper
    {

        public static AppointmentDto ToDto(Appointment entity)
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

        public static void MapUpdate(UpdateAppointmentDto dto, Appointment entity)
        {
            entity.Update(
                dto.Patient, 
                dto.StartTime, 
                dto.EndTime, 
                dto.Notes);
        }
        public static Appointment ToDomain(CreateAppointmentDto dto)
        {
            return new Appointment(
                dto.Patient,
                dto.StartTime,
                dto.EndTime,
                dto.Notes
            );

        }
    }
}
