using BlazingTrails.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingTrails.Application.DTOs.Validators
{
    public class TrailValidator : AbstractValidator<TrailDto>
    {
        public TrailValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Please enter a name");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Please enter a description");
            RuleFor(x => x.Location).NotEmpty().WithMessage("Please enter a location");
            RuleFor(x => x.Length).GreaterThan(0).WithMessage("Please enter a length");
            RuleFor(x => x.TimeInMinutes).GreaterThan(0).WithMessage("Please enter a time");
            RuleFor(x => x.Routes).NotEmpty().WithMessage("Please add a route instruction");
            RuleForEach(x => x.Routes).SetValidator(new RouteInstructionValidator());
        }
    }
}
