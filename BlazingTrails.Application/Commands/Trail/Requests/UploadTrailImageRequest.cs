using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazingTrails.Application.Commands.Trail.Requests
{
    public record UploadTrailImageRequest(int trailId, IBrowserFile? trailImg) : IRequest<UploadTrailImageResponse>
    {
        public const string route = "/api/trails/{trailId}/images";
    }

    public record UploadTrailImageResponse(string? imageName, string? errorMessage);
}
