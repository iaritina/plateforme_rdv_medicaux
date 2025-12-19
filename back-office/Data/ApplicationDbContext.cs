using back_office.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace back_office.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Doctor> Doctors { get; set; } 
        public DbSet<Patient> Patients { get; set; }
        public DbSet<User> Users { get; set; }
        
        public DbSet<Specialities> Specialities { get; set; }
        public DbSet<ConsultationType> ConsultationTypes { get; set; }
        public DbSet<DoctorAvailability> DoctorAvailabilities { get; set; }
    }
}
