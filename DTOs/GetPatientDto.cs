namespace Cwiczenia9.DTOs;

public class GetPatientDto
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public List<GetPrescriptionDto> Prescriptions { get; set; } = new();
}

public class GetPrescriptionDto
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public DoctorDto Doctor { get; set; } = null!;
    public List<MedicamentDto> Medicaments { get; set; } = new();
}

public class DoctorDto
{
    public int IdDoctor { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}

public class MedicamentDto
{
    public int IdMedicament { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int? Dose { get; set; }
}