using AutoMapper;
using PropiedadesMagicas_API.Models;
using PropiedadesMagicas_API.Models.Dto;

namespace PropiedadesMagicas_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Propiedad, PropiedadDto>();
            CreateMap<PropiedadDto, Propiedad>();

            CreateMap<Propiedad, PropiedadCreateDto>().ReverseMap();
            CreateMap<Propiedad, PropiedadUpdateDto>().ReverseMap();

            CreateMap<NumeroPropiedad, NumeroPropiedadDto>().ReverseMap();
            CreateMap<NumeroPropiedad, NumeroPropiedadCreateDto>().ReverseMap();
            CreateMap<NumeroPropiedad, NumeroPropiedadUpdateDto>().ReverseMap();
        }
    }
}