namespace Cwiczenia9.DTOs;

public class AddPrescriptionDto
{
    public PatientDto Patient { get; set; } = null!;
    public List<AddMedicamentDto> Medicaments { get; set; } = new();
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public int IdDoctor { get; set; }
}

public class PatientDto
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime BirthDate { get; set; }
}

public class AddMedicamentDto
{
    public int IdMedicament { get; set; }
    public int? Dose { get; set; }
    public string Details { get; set; } = null!;
}