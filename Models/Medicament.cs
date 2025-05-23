using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cwiczenia9.Models;

[Table("Medicament")]
public class Medicament
{
    [Key]
    public int IdMedicament { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = null!;

    [MaxLength(100)]
    public string? Description { get; set; }

    [MaxLength(100)]
    public string? Type { get; set; }

    public virtual ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = new List<PrescriptionMedicament>();
}