using FluentValidation;
using PropiedadesMinimalApi.Models.DTO;

namespace PropiedadesMinimalApi.Validaciones
{
    public class ValidacionActualizarPropiedad : AbstractValidator<ActualizarPropiedadDTO>
    {
        public ValidacionActualizarPropiedad()
        {
            RuleFor(modelo => modelo.IdPropiedad).NotEmpty().GreaterThan(0);
            RuleFor(modelo => modelo.Nombre).NotEmpty();
            RuleFor(modelo => modelo.Descripcion).NotEmpty();
            RuleFor(modelo => modelo.Ubicacion).NotEmpty();
        }
    }
}
