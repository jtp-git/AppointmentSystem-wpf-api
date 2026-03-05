using AppointmentSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace AppointmentSystem.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Appointment> Appointments { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
