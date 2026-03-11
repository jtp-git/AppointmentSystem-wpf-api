

namespace AppointmentSystem.Application.DTO
{
    public class UpdateAppointmentDto
    {
        public int Id { get;  set; }
        public string Patient { get;  set; } = null!;
        public DateTime StartTime { get;  set; }
        public DateTime EndTime { get;  set; }
        public string? Notes { get;  set; } = null!;
    }
}
