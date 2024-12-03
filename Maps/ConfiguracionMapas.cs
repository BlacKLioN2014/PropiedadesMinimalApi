using AutoMapper;
using PropiedadesMinimalApi.Models;
using PropiedadesMinimalApi.Models.DTO;

namespace PropiedadesMinimalApi.Maps
{
    public class ConfiguracionMapas :Profile
    {
        public ConfiguracionMapas()
        {
            CreateMap<Propiedad, CrearPropiedadDTO>().ReverseMap();
            CreateMap<Propiedad, PropiedadDTO>().ReverseMap();
        }
    }
}
