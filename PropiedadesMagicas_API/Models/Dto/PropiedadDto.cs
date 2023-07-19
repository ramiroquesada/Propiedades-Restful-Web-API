using System.ComponentModel.DataAnnotations;

namespace PropiedadesMagicas_API.Models.Dto
{
    public class PropiedadDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Nombre { get; set; }
    }
}