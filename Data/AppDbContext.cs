using Cwiczenia9.Models;
using Microsoft.EntityFrameworkCore;

namespace Cwiczenia9.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<PrescriptionMedicament>()
            .HasKey(pm => new { pm.IdPrescription, pm.IdMedicament });

        
        var doctors = new List<Doctor>
        {
            new Doctor
            {
                IdDoctor = 1,
                FirstName = "Marek",
                LastName = "Demo",
                Email = "demo@example.com"
            }
        };

        var patients = new List<Patient>
        {
            new Patient
            {
                IdPatient = 1,
                FirstName = "Jan",
                LastName = "Kowalski",
                BirthDate = new DateTime(1991, 9, 19)
            }
        };

        var medicaments = new List<Medicament>
        {
            new Medicament
            {
                IdMedicament = 1,
                Name = "AAA",
                Description = "AAA",
                Type = "Tabletka"
            }
        };
        var prescriptions = new List<Prescription>
        {
            new Prescription
            {
                IdPrescription = 1,
                Date = new DateTime(2025, 6, 5),
                DueDate = new DateTime(2029, 4, 4),
                IdDoctor = 1,
                IdPatient = 1
            }
        };

        var prescriptionMedicaments = new List<PrescriptionMedicament>
        {
            new PrescriptionMedicament
            {
                IdPrescription = 1,
                IdMedicament = 1,
                Dose = 3,
                Details = "AAA"
            }
        };

        modelBuilder.Entity<Prescription>().HasData(prescriptions);
        modelBuilder.Entity<PrescriptionMedicament>().HasData(prescriptionMedicaments);
        modelBuilder.Entity<Doctor>().HasData(doctors);
        modelBuilder.Entity<Patient>().HasData(patients);
        modelBuilder.Entity<Medicament>().HasData(medicaments);
    }
}