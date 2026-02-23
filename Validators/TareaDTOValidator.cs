using FluentValidation;
using PrimerProyecto.DTOs;

namespace PrimerProyecto.Validators
{
    public class TareaDTOValidator : AbstractValidator<TareaDTO>
    {
        public TareaDTOValidator()
        {
            RuleFor(t => t.Titulo)
                .NotEmpty().WithMessage("El título es obligatorio.")
                .MinimumLength(5).WithMessage("El título debe tener al menos 5 caracteres.")
                .MaximumLength(100).WithMessage("El título no puede exceder los 100 caracteres.");

            RuleFor(t => t.Descripcion)
                .MaximumLength(500).WithMessage("La descripción no puede exceder los 500 caracteres.");
        }        
    }
}