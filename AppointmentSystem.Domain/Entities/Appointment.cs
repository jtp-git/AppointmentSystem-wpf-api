
namespace AppointmentSystem.Domain.Entities
{
    public class Appointment
    {
        public int Id { get; private set; }
        public string Patient { get;private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public string Notes { get; private set; }
        public DateTime DateCreated { get; private set; }
        public DateTime? DateModified { get;private set; }

        public Appointment(){}

        public Appointment(string patient, DateTime startTime, DateTime endTime, string notes)
        {
            if (endTime <= startTime)
                throw new ArgumentException("End time must be after start time");
            Patient = patient;
            StartTime = startTime;
            EndTime = endTime;
            Notes = notes;
            DateCreated = DateTime.UtcNow;
        }

        public void Update(string patient, DateTime startTime, DateTime endTime, string notes)
        {
            if (endTime <= startTime)
                throw new ArgumentException("End time must be after start time");
            Patient = patient;
            StartTime = startTime;
            EndTime = endTime;
            Notes = notes;
            DateModified = DateTime.UtcNow;
        }

    }
}
