using BlazingTrails.Domain.Entities;
using FluentValidation;

namespace BlazingTrails.Application.DTOs.Validators
{
    public class RouteInstructionValidator : AbstractValidator<RouteInstruction>
    {
        public RouteInstructionValidator()
        {
            RuleFor(x => x.Stage).NotEmpty().WithMessage("Please enter a stage");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Please enter a description");
        }
    }
}
