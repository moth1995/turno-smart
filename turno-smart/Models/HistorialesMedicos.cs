using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace turno_smart.Models
{
    [Table("HistorialesMedicos")]
public class HistorialMedico
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [ForeignKey(nameof(Paciente))]
    public int IdPaciente { get; set; }
    public virtual Paciente Paciente { get; set; }

    [ForeignKey(nameof(Estudio))]
    public int IdEstudio { get; set; }
    public virtual Estudio Estudio { get; set; }

    [ForeignKey(nameof(Medico))]
    public int IdMedico { get; set; }
    public virtual Medico Medico { get; set; }
}

}
