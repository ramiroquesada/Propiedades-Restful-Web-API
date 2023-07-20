using System.ComponentModel.DataAnnotations;

namespace PropiedadesMagicas_API.Models.Dto
{
    public class PropiedadUpdateDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Nombre { get; set; }

        public string Detalles { get; set; }

        [Required]
        public double Tarifa { get; set; }

        [Required]
        public int Ocupantes { get; set; }

        [Required]
        public int MetrosCuadrados { get; set; }

        [Required]
        public string ImagenUrl { get; set; }

        [Required]
        public string Amenidad { get; set; }
    }
}