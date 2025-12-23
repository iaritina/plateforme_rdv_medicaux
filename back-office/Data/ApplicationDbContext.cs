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

        public DbSet<Specialities> Specialities { get; set; }
        public DbSet<ConsultationType> ConsultationTypes { get; set; }
        public DbSet<DoctorAvailability> DoctorAvailabilities { get; set; }
        public DbSet<DoctorSpeciality> DoctorSpecialities { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<DoctorSpeciality>(entity =>
            {
                entity.ToTable("DOCTOR_SPECIALITY");

                entity.HasKey(ds => new { ds.DoctorId, ds.SpecialityId });

                entity.Property(ds => ds.DoctorId)
                    .HasColumnName("ID_DOC");

                entity.Property(ds => ds.SpecialityId)
                    .HasColumnName("ID_SPEC");

                entity.HasOne(ds => ds.Doctor)
                    .WithMany(d => d.DoctorSpecialities)
                    .HasForeignKey(ds => ds.DoctorId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(ds => ds.Speciality)
                    .WithMany(s => s.DoctorSpecialities)
                    .HasForeignKey(ds => ds.SpecialityId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            builder.Entity<Appointment>(entity =>
            {
                entity.ToTable("APPOINTMENT");

                entity.HasKey(a => a.ID_APT);

                entity.HasOne(a => a.Patient)
                    .WithMany()
                    .HasForeignKey(a => a.ID_PAT)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(a => a.ConsultationType)
                    .WithMany()
                    .HasForeignKey(a => a.ID_TYPE_CONSUL)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(a => a.Doctor)
                    .WithMany()
                    .HasForeignKey(a => a.ID_DOC)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}