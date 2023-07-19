﻿using System.ComponentModel.DataAnnotations;

namespace PropiedadesMagicas_API.Models
{
    public class Propiedad
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Detalles { get; set; }

        [Required]
        public double Tarifa { get; set; }

        public int Ocupantes { get; set; }

        public int MetrosCuadrados { get; set; }

        public string ImagenUrl { get; set; }

        public string Amenidad { get; set; }

        public DateTime FechaActualizacion { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}