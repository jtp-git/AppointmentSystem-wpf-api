

namespace AppointmentSystem.Application.DTO
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public string Patient { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public string?  Notes { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
