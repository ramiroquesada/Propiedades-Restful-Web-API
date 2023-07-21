using System.ComponentModel.DataAnnotations;

namespace PropiedadesMagicas_API.Models.Dto
{
    public class NumeroPropiedadDto
    {
        [Required]
        public int PropiedadNum { get; set; }

        [Required]
        public int PropiedadId { get; set; }

        public string DetallesEspeciales { get; set; }
    }
}