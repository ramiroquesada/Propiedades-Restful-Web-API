using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropiedadesMagicas_API.Models
{
    public class NumeroPropiedad
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PropiedadNum { get; set; }

        [Required]
        public int PropiedadId { get; set; }

        [ForeignKey("PropiedadId")]
        public Propiedad Propiedad { get; set; }

        public string DetallesEspeciales { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaActualizacion { get; set; }
    }
}