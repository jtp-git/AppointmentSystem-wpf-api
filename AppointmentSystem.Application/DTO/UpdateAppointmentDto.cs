using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentSystem.Application.DTO
{
    public class UpdateAppointmentDto
    {
        public int Id { get; private set; }
        public string Patient { get; private set; } = null!;
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public string? Notes { get; private set; } = null!;
    }
}
