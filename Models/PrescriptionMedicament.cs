using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cwiczenia9.Models;

[Table("Prescription_Medicament")]
public class PrescriptionMedicament
{
    
    public int IdMedicament { get; set; }
    
    public int IdPrescription { get; set; }

    public int? Dose { get; set; }

    [MaxLength(100)]
    public string? Details { get; set; }
    
    [ForeignKey(nameof(IdMedicament))]
    public virtual Medicament Medicament { get; set; } = null!;

    [ForeignKey(nameof(IdPrescription))]
    public virtual Prescription Prescription { get; set; } = null!;
}