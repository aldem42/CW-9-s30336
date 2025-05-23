using Cwiczenia9.Data;
using Cwiczenia9.DTOs;
using Cwiczenia9.Exceptions;
using Cwiczenia9.Models;
using Microsoft.EntityFrameworkCore; 

namespace Cwiczenia9.Services;

public interface IDbService
{
    public Task AddPrescriptionAsync(AddPrescriptionDto dto);
    public Task<GetPatientDto> GetPatientDetailsAsync(int id);
}

public class DbService(AppDbContext data) : IDbService
{
        public async Task AddPrescriptionAsync(AddPrescriptionDto dto)
    {
        if (dto.Medicaments.Count > 10)
            throw new NotFoundException("Recepta może obejmować maksymalnie 10 leków.");

        if (dto.DueDate < dto.Date)
            throw new NotFoundException("Błąd dat.");

        await using var transaction = await data.Database.BeginTransactionAsync();
        try
        {
            var doctor = await data.Doctors
                .FirstOrDefaultAsync(d => d.IdDoctor == dto.IdDoctor)
                ?? throw new NotFoundException($"Doktor o ID {dto.IdDoctor} nie został znaleziony.");

            Patient patient;
            if (dto.Patient.IdPatient != 0)
            {
                patient = await data.Patients
                    .FirstOrDefaultAsync(p => p.IdPatient == dto.Patient.IdPatient)
                    ?? throw new NotFoundException($"Pacjent o ID {dto.Patient.IdPatient} nie został znaleziony.");
            }
            else
            {
                patient = new Patient
                {
                    FirstName = dto.Patient.FirstName,
                    LastName = dto.Patient.LastName,
                    BirthDate = dto.Patient.BirthDate
                };

                await data.Patients.AddAsync(patient);
                await data.SaveChangesAsync();
            }

            var medicamentElement = new List<Medicament>();
            foreach (var medDto in dto.Medicaments)
            {
                var medicament = await data.Medicaments
                    .FirstOrDefaultAsync(m => m.IdMedicament == medDto.IdMedicament);

                if (medicament is null)
                    throw new NotFoundException($"Medicament o ID {medDto.IdMedicament} nie został znaleziony.");

                medicamentElement.Add(medicament);
            }

            var prescription = new Prescription
            {
                Date = dto.Date,
                DueDate = dto.DueDate,
                IdDoctor = doctor.IdDoctor,
                IdPatient = patient.IdPatient,
                PrescriptionMedicaments = dto.Medicaments.Select(medDto => new PrescriptionMedicament
                {
                    IdMedicament = medDto.IdMedicament,
                    Dose = medDto.Dose,
                    Details = medDto.Details
                }).ToList()
            };

            await data.Prescriptions.AddAsync(prescription);
            await data.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<GetPatientDto> GetPatientDetailsAsync(int id)
    {
        var patient = await data.Patients
            .Where(p => p.IdPatient == id)
            .Select(p => new GetPatientDto
            {
                IdPatient = p.IdPatient,
                FirstName = p.FirstName,
                LastName = p.LastName,
                BirthDate = p.BirthDate,
                Prescriptions = p.Prescriptions
                    .OrderBy(pr => pr.DueDate)
                    .Select(pr => new GetPrescriptionDto
                    {
                        IdPrescription = pr.IdPrescription,
                        Date = pr.Date,
                        DueDate = pr.DueDate,
                        Doctor = new DoctorDto
                        {
                            IdDoctor = pr.Doctor.IdDoctor,
                            FirstName = pr.Doctor.FirstName,
                            LastName = pr.Doctor.LastName
                        },
                        Medicaments = pr.PrescriptionMedicaments
                            .Select(pm => new MedicamentDto
                            {
                                IdMedicament = pm.Medicament.IdMedicament,
                                Name = pm.Medicament.Name,
                                Description = pm.Medicament.Description,
                                Dose = pm.Dose
                            }).ToList()
                    }).ToList()
            }).FirstOrDefaultAsync();

        return patient ?? throw new NotFoundException($"Patient o ID {id} nie został znaleziony");
    }
}
