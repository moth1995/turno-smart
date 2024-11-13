using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace turno_smart.Models
{
    [Table("Estudios")]
    public class Estudio
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string Descripcion { get; set; }
        public virtual List<HistorialMedico> HistorialMedico { get; set; }
    }
}
