using PropiedadesMagicas_API.Models.Dto;

namespace PropiedadesMagicas_API.Datos
{
    public static class PropiedadStore
    {
        public static List<PropiedadDto> propiedadList = new List<PropiedadDto> {
        new PropiedadDto{ Id = 1, Nombre= "Vista a la piscina", Ocupantes= 2, MetrosCuadrados= 20},
        new PropiedadDto{Id = 2, Nombre= "vista a la playa", Ocupantes = 3, MetrosCuadrados = 30},
        new PropiedadDto{Id= 3, Nombre="vista al centro", Ocupantes = 5, MetrosCuadrados = 50}
        };
    }
}