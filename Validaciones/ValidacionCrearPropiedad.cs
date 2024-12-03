using FluentValidation;
using PropiedadesMinimalApi.Models.DTO;

namespace PropiedadesMinimalApi.Validaciones
{
    public class ValidacionCrearPropiedad : AbstractValidator<CrearPropiedadDTO>
    {
        public ValidacionCrearPropiedad()
        {
            RuleFor(modelo => modelo.Nombre).NotEmpty();
            RuleFor(modelo => modelo.Descripcion).NotEmpty();
            RuleFor(modelo => modelo.Ubicacion).NotEmpty();
        }
    }
}
