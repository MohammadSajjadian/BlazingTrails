using BlazingTrails.Application.DTOs;
using BlazingTrails.Application.DTOs.Validators;
using FluentValidation;
using MediatR;

namespace BlazingTrails.Application.Commands.Trail.Requests
{
    public record AddTrailRequest(TrailDto trailDto) : IRequest<AddTrailResponse>
    {
        public const string route = "/api/trails";
    }

    public class AddTrailRequestValidator : AbstractValidator<AddTrailRequest>
    {
        public AddTrailRequestValidator()
        {
            RuleFor(x => x.trailDto).SetValidator(new TrailValidator());
        }
    }

    public record AddTrailResponse(int trailId, string? errorMessage);
}
