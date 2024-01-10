using BlazingTrails.Application.DTOs;
using BlazingTrails.Application.DTOs.Validators;
using FluentValidation;
using MediatR;

namespace BlazingTrails.Application.Commands.Trail.Requests
{
    public record EditTrailRequest(TrailDto trailDto) : IRequest<EditTrailResponse>
    {
        public const string route = "/api/trails";
    }

    public record EditTrailResponse(bool isSuccess, string? errorMessage);

    public class EditTrailRequestValidator : AbstractValidator<EditTrailRequest>
    {
        public EditTrailRequestValidator()
        {
            RuleFor(x => x.trailDto).SetValidator(new TrailValidator());
        }
    }
}
