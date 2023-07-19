using PropiedadesMagicas_API.Models.Dto;

namespace PropiedadesMagicas_API.Datos
{
    public static class PropiedadStore
    {
        public static List<PropiedadDto> propiedadList = new List<PropiedadDto> {
        new PropiedadDto{ Id = 1, Nombre= "Vista a la piscina"},
        new PropiedadDto{Id = 2, Nombre= "vista a la playa"},
        new PropiedadDto{Id= 3, Nombre="vista al centro"}
        };
    }
}
